using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CovidSpawner : MonoBehaviour
{
    [SerializeField] private Transform _pfCovid;
    private UIManager _uiManager;

    [Range(0, 100)]
    [SerializeField] private int numberOfEnemies = 25;
    private float range = 80f;

    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            _uiManager = canvas.GetComponent<UIManager>();
        }

        _uiManager.setRemainingCovids(numberOfEnemies);

        for (int index = 1; index < numberOfEnemies; index++)
        {
            Instantiate(
                _pfCovid,
                RandomNavmeshLocation(range),
                Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
            finalPosition.y = 2f;
        }
        return finalPosition;
    }

    public void decreaseCovidsCount()
    {
        _uiManager.setRemainingCovids(--numberOfEnemies);
    }
}
