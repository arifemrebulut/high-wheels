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
        Debug.Log("Crash");

        collider.enabled = false;

        if (CarSuspensions.currentSuspensionCount == 0 || (CarSuspensions.currentSuspensionCount - blockHeight) < 0)
        {
            EventBroker.CallOnGameOver();
        }
        else
        {
            EventBroker.CallOnHitToBlock(blockHeight);
        }
    }
}
