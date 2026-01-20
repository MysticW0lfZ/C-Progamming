using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int maxScore = 5;

    public TMP_Text leftScoreText;
    public TMP_Text rightScoreText;

    public GameObject winPanel;
    public TMP_Text winText;

    public BallMovement ball;

    private int leftScore;
    private int rightScore;
    private bool gameOver;

    void Start()
    {
        gameOver = false;

        if (winPanel != null) winPanel.SetActive(false);
        if (winText != null) winText.text = "";

        if (ball == null)
        {
            GameObject ballObj = GameObject.FindGameObjectWithTag("Ball");
            if (ballObj != null) ball = ballObj.GetComponent<BallMovement>();
        }

        UpdateUI();

        if (ball != null) ball.RestartRound();
    }

    public void ScoreLeftPlayer()
    {
        if (gameOver) return;

        leftScore += 1;
        UpdateUI();

        if (leftScore >= maxScore)
        {
            EndGame("Player 2 (right side) wins");
            return;
        }

        if (ball != null) ball.RestartRound();
    }

    public void ScoreRightPlayer()
    {
        if (gameOver) return;

        rightScore += 1;
        UpdateUI();

        if (rightScore >= maxScore)
        {
            EndGame("Player 1 (left side) wins");
            return;
        }

        if (ball != null) ball.RestartRound();
    }

    void EndGame(string message)
    {
        gameOver = true;

        if (winText != null) winText.text = message;
        if (winPanel != null) winPanel.SetActive(true);

        if (ball != null) ball.ResetBall();

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        leftScore = 0;
        rightScore = 0;
        gameOver = false;

        if (winPanel != null) winPanel.SetActive(false);
        if (winText != null) winText.text = "";

        UpdateUI();

        if (ball != null) ball.RestartRound();
    }

    void UpdateUI()
    {
        if (leftScoreText != null) leftScoreText.text = leftScore.ToString();
        if (rightScoreText != null) rightScoreText.text = rightScore.ToString();
    }
}
