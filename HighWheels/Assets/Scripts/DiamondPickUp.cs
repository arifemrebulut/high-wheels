using UnityEngine;

public class DiamondPickUp : MonoBehaviour, ICollideable
{
    public void ExicuteCollisionActions()
    {
        EventBroker.CallOnPickUpDiamond();
        Destroy(gameObject);
    }
}
