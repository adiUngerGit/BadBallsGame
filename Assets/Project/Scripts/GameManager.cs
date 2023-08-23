using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public static int playerScore = 0;
    public static int playerLives = 3;
    private bool isGameOver;

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            playerScore = 0;
            isGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }

        if (playerLives == 0)
        {
            isGameOver = true;
            canvas.SetActive(true);
            playerLives = 3;

        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 50), " Score: " + playerScore);
        GUI.Label(new Rect(10, 30, 200, 50), " Lives: " + playerLives);
    }
}
