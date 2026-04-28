using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class LocalPaddleController : PaddleController
{
    private Key upKey;
    private Key downKey;

    public override void OnNetworkSpawn()
    {
        if (OwnerClientId == 0)
        {
            upKey = Key.W;
            downKey = Key.S;
        }
        else
        {
            upKey = Key.UpArrow;
            downKey = Key.DownArrow;
        }
    }

    protected override float GetMovementInput()
    {
        float move = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current[upKey].isPressed) move += 1f;
            if (Keyboard.current[downKey].isPressed) move -= 1f;
        }

        return move;
    }
}