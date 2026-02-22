using UnityEngine;

public abstract class PaddleController : MonoBehaviour, ICollidable
{
    public float speed = 8f;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        float input = GetMovementInput();
        rb.linearVelocity = new Vector2(0, input * speed);
    }

    protected abstract float GetMovementInput();

    // REQUIRED by ICollidable
    public void OnHit(Collision2D collision)
    {
        // Optional feedback
    }
}