using Unity.Netcode;
using UnityEngine;

public class BallMovement : NetworkBehaviour, ICollidable
{
    public float speed = 6f;
    public float launchDelay = 1f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody2D>(); // ensure reference

        // Only SERVER controls ball
        if (IsServer)
        {
            RestartRound();
        }
    }

    void Launch()
    {
        if (!IsServer) return;

        float x = UnityEngine.Random.value < 0.5f ? -1f : 1f;
        float y = UnityEngine.Random.Range(-0.6f, 0.6f);

        rb.linearVelocity = new Vector2(x, y).normalized * speed;
    }

    public void ResetBall()
    {
        if (!IsServer) return;
        if (rb == null) return;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = Vector2.zero;
    }

    public void RestartRound()
    {
        if (!IsServer) return;
        if (rb == null) return;

        ResetBall();
        CancelInvoke(nameof(Launch));
        Invoke(nameof(Launch), launchDelay);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsServer) return;

        ICollidable collidable = collision.gameObject.GetComponent<ICollidable>();
        if (collidable != null)
        {
            collidable.OnHit(collision);
        }

        rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }

    public void OnHit(Collision2D collision)
    {
        if (!IsServer) return;

        rb.linearVelocity = -rb.linearVelocity;
    }
}