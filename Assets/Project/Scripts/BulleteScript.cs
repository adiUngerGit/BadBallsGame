using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulleteScript : MonoBehaviour
{
    public int bulletSpeed = 6;

    void Update()
    {
        float moveBy = bulletSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * moveBy);

        if (transform.position.y > 6)
        {
            Destroy(gameObject);
        }
    }
}
