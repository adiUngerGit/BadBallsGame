using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Madhur.InfoPopup;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _livesText;
    [SerializeField] private Text _remainingCovids;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Image _crosshairImage;

    public void setLives(int lives)
    {
        _livesText.text = "Lives: " + lives;

        if (lives == 1)
        {
            _livesText.color = Color.red;
        }
    }

    public void setRemainingCovids(int num)
    {
        _remainingCovids.text = "Bad Balls: " + num;

        if (num == 0)
        {
            SceneManager.LoadScene(4);
        }
    }

    public void showInstructionText(string text, bool isPositive)
    {
        _gameOverText.text = text;
        if (isPositive)
        {
            _gameOverText.color = Color.green;
        }
        else
        {
            _gameOverText.color = Color.red;
        }
        _gameOverText.gameObject.SetActive(true);
    }

    public void showCrosshair(bool show)
    {
        _crosshairImage.gameObject.SetActive(show);
    }
}
