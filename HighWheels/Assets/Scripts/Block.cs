using UnityEngine;

public class Block : MonoBehaviour, ICollideable
{
    [Range(1, 5)]
    [SerializeField] int blockHeight;

    Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    public void ExicuteCollisionActions()
    {
        collider.enabled = false;

        EventBroker.CallOnHitToBlock(blockHeight);
    }
}
