using UnityEngine;
using Cinemachine;
using StarterAssets;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private int _lives = 4;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform pfBullet;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform shieldMesh;

    private StarterAssetsInputs _starterAssetsInput;
    private ThirdPersonController _thirdPersonController;
    private UIManager _uiManager;
    private CovidSpawner _covidSpawner;
    private Animator _animator;

    void Awake()
    {
        _starterAssetsInput = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            _uiManager = canvas.GetComponent<UIManager>();
        }

        GameObject spawnerObject = GameObject.Find("CovidSpawner");
        if (spawnerObject != null)
        {
            _covidSpawner = spawnerObject.GetComponent<CovidSpawner>();
        }

        _uiManager.setLives(_lives);
    }

    void Update()
    {
        setAimInputBehavior();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Covid")
        {
            if (!shieldMesh.gameObject.activeSelf) 
            {
                dammage();
            }
            Destroy(other.gameObject);
            _covidSpawner.decreaseCovidsCount();
            shieldMesh.gameObject.SetActive(false);
        }

        if (other.tag == "Shield")
        {
            Destroy(other.gameObject);
            shieldMesh.gameObject.SetActive(true);
        }
    }

    void setAimInputBehavior()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        if (_starterAssetsInput.aim)
        {
            if (Input.GetMouseButtonDown(0)) {
                StartCoroutine(showShootAnimation());
            }

            // Camera switching
            _uiManager.showCrosshair(true);
            aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.setRotationSensitivity(aimSensitivity);
            _thirdPersonController.setRotateOnMove(true);

            // Shooting raycast
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)) {
                mouseWorldPosition = raycastHit.point;
            }

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            //transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            if (_starterAssetsInput.shoot)
            {
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(pfBullet, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                _starterAssetsInput.shoot = false;
            }
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.setRotationSensitivity(normalSensitivity);
            _thirdPersonController.setRotateOnMove(true);
            _uiManager.showCrosshair(false);
        }
    }

    public void dammage()
    {
        _lives--;
        if (_lives == 0) {
            SceneManager.LoadScene(2);
        }
        else
        {
            _uiManager.setLives(_lives);
        }
    }

    IEnumerator showShootAnimation() {
        _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
        yield return new WaitForSeconds(0.7f);
        _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
    }
}
