using Unity.Netcode;
using UnityEngine;
using TMPro;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    public int maxScore = 5;

    public TMP_Text leftScoreText;
    public TMP_Text rightScoreText;

    public GameObject winPanel;
    public TMP_Text winText;

    public BallMovement ball;

    private bool gameOver;

    public NetworkVariable<int> leftScore = new NetworkVariable<int>(
        0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    public NetworkVariable<int> rightScore = new NetworkVariable<int>(
        0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    void Awake()
    {
        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        leftScore.OnValueChanged += OnScoreChanged;
        rightScore.OnValueChanged += OnScoreChanged;

        if (winPanel != null) winPanel.SetActive(false);
        if (winText != null) winText.text = "";

        FindBall();
        UpdateUI();

        if (IsServer && ball != null)
        {
            ball.RestartRound();
        }
    }

    public override void OnNetworkDespawn()
    {
        leftScore.OnValueChanged -= OnScoreChanged;
        rightScore.OnValueChanged -= OnScoreChanged;
    }

    void OnScoreChanged(int oldValue, int newValue)
    {
        UpdateUI();
    }

    void FindBall()
    {
        if (ball != null) return;

        GameObject ballObj = GameObject.FindGameObjectWithTag("Ball");

        if (ballObj != null)
        {
            ball = ballObj.GetComponent<BallMovement>();
        }
    }

    public void ScoreLeftPlayer()
    {
        if (!IsServer || gameOver) return;

        leftScore.Value++;
        UpdateUI();

        if (leftScore.Value >= maxScore)
        {
            EndGameClientRpc(true);
            return;
        }

        FindBall();
        if (ball != null) ball.RestartRound();
    }

    public void ScoreRightPlayer()
    {
        if (!IsServer || gameOver) return;

        rightScore.Value++;
        UpdateUI();

        if (rightScore.Value >= maxScore)
        {
            EndGameClientRpc(false);
            return;
        }

        FindBall();
        if (ball != null) ball.RestartRound();
    }

    [ClientRpc]
    void EndGameClientRpc(bool leftPlayerWon)
    {
        gameOver = true;

        if (leftPlayerWon)
        {
            ShowWin("Player 1 (left side) wins");
        }
        else
        {
            ShowWin("Player 2 (right side) wins");
        }
    }

    void ShowWin(string message)
    {
        if (winText != null) winText.text = message;
        if (winPanel != null) winPanel.SetActive(true);

        if (IsServer)
        {
            FindBall();
            if (ball != null) ball.ResetBall();
        }
    }

    public void RestartGame()
    {
        if (!IsServer) return;

        leftScore.Value = 0;
        rightScore.Value = 0;
        gameOver = false;

        RestartClientRpc();

        FindBall();
        if (ball != null) ball.RestartRound();
    }

    [ClientRpc]
    void RestartClientRpc()
    {
        gameOver = false;

        if (winPanel != null) winPanel.SetActive(false);
        if (winText != null) winText.text = "";

        UpdateUI();
    }

    void UpdateUI()
    {
        if (leftScoreText != null)
            leftScoreText.text = leftScore.Value.ToString();

        if (rightScoreText != null)
            rightScoreText.text = rightScore.Value.ToString();
    }
}