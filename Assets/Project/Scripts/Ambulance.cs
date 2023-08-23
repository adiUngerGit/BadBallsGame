using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambulance : MonoBehaviour
{   
    public Transform[] waypoints;

    public float speed = 10;
    int currentWayPoint;
    Vector3 target, moveDirection;
    bool isWaiting;
    bool justStarted = true;
    int lastWaitPoint = -1;
    bool isCollide = false;

    void Update()
    {
        if (!isCollide)
        {
            target = waypoints[currentWayPoint].position;
            moveDirection = target - transform.position; 
            
            if (moveDirection.magnitude < 1)
            {
                if (lastWaitPoint != currentWayPoint)
                {
                    lastWaitPoint = currentWayPoint;
                    StartCoroutine(waitFor(5));
                }

                if (!isWaiting)
                {
                    if (!justStarted)
                    {
                        transform.Rotate(new Vector3(0, 90, 0));
                    }
                    justStarted = false;
                    currentWayPoint = ++currentWayPoint % waypoints.Length;
                }
            }

            if (!isWaiting)
            {
                GetComponent<Rigidbody>().velocity = moveDirection.normalized * speed;
            }
            else
            {
                GetComponent<Rigidbody>().velocity = moveDirection.normalized * 0;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCollide = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCollide = false;
        }
    }

    private IEnumerator waitFor(int seconds)
    {
        isWaiting = true;
        yield return new WaitForSeconds(seconds);
        isWaiting = false;
    }
}
