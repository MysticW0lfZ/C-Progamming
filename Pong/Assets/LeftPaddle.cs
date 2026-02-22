using UnityEngine;
using UnityEngine.InputSystem;

public class LeftPaddle : PaddleController
{
    protected override float GetMovementInput()
    {
        if (Keyboard.current == null) return 0f;

        float move = 0f;
        if (Keyboard.current.wKey.isPressed) move += 1f;
        if (Keyboard.current.sKey.isPressed) move -= 1f;

        return move;
    }
}