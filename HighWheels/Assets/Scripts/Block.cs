using UnityEngine;

public class Block : MonoBehaviour, ICollideable
{
    public void ExicuteCollisionActions()
    {
        EventBroker.CallOnHitToBlock();
    }
}
