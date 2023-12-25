using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float minYPosition = -10f;

    public GameOverUIManager gameOverUIManager;

    void Update()
    {
        CheckPlayersPosition();
    }

    void CheckPlayersPosition()
    {
        if (player1.position.y < minYPosition || player2.position.y < minYPosition)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over! One of the players fell off the screen.");

        if (gameOverUIManager != null)
        {
            gameOverUIManager.ShowGameOverPanel();
        }
    }
}
