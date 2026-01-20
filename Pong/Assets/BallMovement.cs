
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 6f;
    public float launchDelay = 1f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        RestartRound();
    }

    void Launch()
    {
        float x = UnityEngine.Random.value < 0.5f ? -1f : 1f;
        float y = UnityEngine.Random.Range(-0.6f, 0.6f);

        rb.linearVelocity = new Vector2(x, y).normalized * speed;
    }

 
    public void ResetBall()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = Vector2.zero;
    }

    public void RestartRound()
    {
        ResetBall();
        CancelInvoke(nameof(Launch));
        Invoke(nameof(Launch), launchDelay);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }
}
