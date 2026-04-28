using Unity.Netcode;
using UnityEngine;

public abstract class PaddleController : NetworkBehaviour
{
    public float speed = 8f;
    public float yLimit = 4.2f;

    protected NetworkVariable<float> syncedY =
        new NetworkVariable<float>(
            0f,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner
        );

    protected abstract float GetMovementInput();

    void Update()
    {
        // ONLY OWNER moves
        if (!IsOwner) return;

        float move = GetMovementInput();

        Vector3 pos = transform.position;
        pos.y += move * speed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);

        transform.position = pos;

        // Sync Y only
        syncedY.Value = pos.y;
    }

    void LateUpdate()
    {
        // NON-OWNERS follow Y ONLY (do not touch X)
        if (IsOwner) return;

        Vector3 pos = transform.position;
        pos.y = syncedY.Value;
        transform.position = pos;
    }
}