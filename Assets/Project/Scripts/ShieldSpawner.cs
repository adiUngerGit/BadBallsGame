using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShieldSpawner : MonoBehaviour
{
    [SerializeField] private Transform _pfShield;
    private UIManager _uiManager;
    
    [Range (0, 30)]
    [SerializeField] private int numberOfShields = 5;
    private float range = 80f;
    
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            _uiManager = canvas.GetComponent<UIManager>();
        }

        for (int index = 0; index < numberOfShields; index++)
        {
            Vector3 randomPos = RandomNavmeshLocation(range);
            while (randomPos.x == 0)
            {
                randomPos = RandomNavmeshLocation(range);
            }

            Instantiate(
                _pfShield,
                randomPos,
                Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
    }
    
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = new Vector3(-74, 12.2f, -23);
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
            finalPosition.y = 2f;
        }
        return finalPosition;
    }
}
