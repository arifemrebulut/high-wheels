using UnityEngine;

public class Block : MonoBehaviour, ICollideable
{
    Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    public void ExicuteCollisionActions()
    {
        collider.enabled = false;

        EventBroker.CallOnHitToBlock();
    }
}
