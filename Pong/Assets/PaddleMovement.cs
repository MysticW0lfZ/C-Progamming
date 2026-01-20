using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 8f;
    public float yLimit = 4.2f;

    public Key upKey = Key.W;
    public Key downKey = Key.S;

    void Update()
    {
        float move = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current[upKey].isPressed) move += 1f;
            if (Keyboard.current[downKey].isPressed) move -= 1f;
        }

        Vector3 pos = transform.position;
        pos.y += move * speed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);
        transform.position = pos;
    }
}
