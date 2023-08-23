using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    [SerializeField] private Transform _exitCollider;
    float seconds = 60;

    void Start()
    {
        _exitCollider.gameObject.SetActive(false);
    }

    void Update()
    {
        _timerText.text = (int)seconds + "s";
        seconds -= Time.deltaTime;

        if ((int)seconds == 0)
        {
            _exitCollider.gameObject.SetActive(true);
            _timerText.gameObject.SetActive(false);
        }
    }
}
