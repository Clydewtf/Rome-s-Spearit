using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnToMenu()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}

