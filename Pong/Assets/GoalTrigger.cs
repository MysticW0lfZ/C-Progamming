using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public bool isLeftGoal;
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;

        if (isLeftGoal)
        {
            gameManager.ScoreLeftPlayer();   // Left goal gives left player point
        }
        else
        {
            gameManager.ScoreRightPlayer();  // Right goal gives right player point
        }
    }
}
