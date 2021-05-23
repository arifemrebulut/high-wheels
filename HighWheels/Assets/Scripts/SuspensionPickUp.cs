using UnityEngine;

public class SuspensionPickUp : MonoBehaviour, ICollideable
{
    public void ExicuteCollisionActions()
    {
        EventBroker.CallOnPickUpSuspension();
        Destroy(gameObject);
    }
}
