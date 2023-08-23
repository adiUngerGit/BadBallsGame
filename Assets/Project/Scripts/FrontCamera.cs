using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject frontCamera;
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            frontCamera.SetActive(true);
        }
        else
        {   
            frontCamera.SetActive(false);
        }
    }
}
