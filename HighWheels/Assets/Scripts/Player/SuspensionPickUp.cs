using UnityEngine;

public class SuspensionPickUp : MonoBehaviour, ICollideable
{
    public void ExicuteCollisionActions()
    {
        Destroy(gameObject);
        EventBroker.CallOnPickUpSuspension();
    }
}
