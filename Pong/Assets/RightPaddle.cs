using UnityEngine;
using UnityEngine.InputSystem;

public class RightPaddle : PaddleController
{
    protected override float GetMovementInput()
    {
        if (Keyboard.current == null) return 0f;

        float move = 0f;
        if (Keyboard.current.upArrowKey.isPressed) move += 1f;
        if (Keyboard.current.downArrowKey.isPressed) move -= 1f;

        return move;
    }
}