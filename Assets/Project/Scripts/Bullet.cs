using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    [SerializeField] Transform pfCovidDestruction;
    private CovidSpawner _covidSpawner;

    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        GameObject spawnerObject = GameObject.Find("CovidSpawner");
        if (spawnerObject != null)
        {
            _covidSpawner = spawnerObject.GetComponent<CovidSpawner>();
        }

        float speed = 10f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    void Update()
    {
        Destroy(gameObject, 5);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Covid")
        {
            Destroy(gameObject);
            Instantiate(pfCovidDestruction, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _covidSpawner.decreaseCovidsCount();
        }
    }
}
