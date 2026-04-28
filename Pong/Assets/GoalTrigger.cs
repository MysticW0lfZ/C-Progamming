using UnityEngine;
using Unity.Netcode;

public class GoalTrigger : MonoBehaviour
{
    public bool isLeftGoal;
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;

        if (!NetworkManager.Singleton.IsServer) return;

        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        //  FIXED LOGIC
        if (isLeftGoal)
        {
            // Ball hit LEFT → RIGHT player scores
            gameManager.ScoreRightPlayer();
        }
        else
        {
            // Ball hit RIGHT → LEFT player scores
            gameManager.ScoreLeftPlayer();
        }
    }
}