using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int enemySpeed = 3;
  
    void Update()
    {
        float moveBy = enemySpeed * Time.deltaTime;
        transform.Translate(Vector3.down * moveBy);

        if (transform.position.y <= -4.8)
        {
            Vector3 newPos = new Vector3(transform.position.x, 7, transform.position.z);
            newPos.x = Random.Range(-8.5f, 8.5f);
            transform.position = newPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.playerLives--;

            if (GameManager.playerLives == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                // Reset enemy position
                Vector3 enemyPosition = transform.position;
                Vector3 newPos = new Vector3(Random.Range(-8.5f, 8.5f), 7, enemyPosition.z);
                transform.position = newPos;
            }
        }

        if (other.CompareTag("Bullet"))
        {
            GameManager.playerScore += 100;
            Destroy(other.gameObject);
            
            {
                // Reset enemy position
                Vector3 enemyPosition = transform.position;
                Vector3 newPos = new Vector3(Random.Range(-8.5f, 8.5f), 7, enemyPosition.z);
                transform.position = newPos;
            }
        }
    }
}
