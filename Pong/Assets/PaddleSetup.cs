using Unity.Netcode;
using UnityEngine;

public class PaddleSetup : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        // ONLY SERVER sets position
        if (!IsServer) return;

        if (OwnerClientId == 0)
        {
            transform.position = new Vector3(-7f, 0f, 0f); // LEFT
        }
        else
        {
            transform.position = new Vector3(7f, 0f, 0f); // RIGHT
        }
    }
}