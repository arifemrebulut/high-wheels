using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private bool canCollide = true;

    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Block"))
    //    {
    //        if (CarSuspensions.currentSuspensionCount == 0 && canCollide)
    //        {
    //            EventBroker.CallOnGameOver();
    //        }
    //        else if (CarSuspensions.currentSuspensionCount > 0 && canCollide)
    //        {
    //            ICollideable collideable = other.gameObject.GetComponent<ICollideable>();

    //            collideable.ExicuteCollisionActions();
    //            canCollide = false;
    //        }
    //    }
    //    else if (other.gameObject.CompareTag("SuspensionPickUp"))
    //    {
    //        Debug.Log("PickSuspension");
    //        other.gameObject.GetComponent<ICollideable>().ExicuteCollisionActions();
    //    }
    //    else if (other.gameObject.CompareTag("DiamondPickUp"))
    //    {
    //        other.gameObject.GetComponent<ICollideable>().ExicuteCollisionActions();
    //    }      
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            if (CarSuspensions.currentSuspensionCount == 0)
            {
                EventBroker.CallOnGameOver();
            }
            else if (CarSuspensions.currentSuspensionCount > 0)
            {
                DeactivateCollider();
                ICollideable collideable = other.gameObject.GetComponent<ICollideable>();

                collideable.ExicuteCollisionActions();

                Invoke("ActivateCollider", 0.8f);
                
            }
        }
        else if (other.gameObject.CompareTag("SuspensionPickUp"))
        {
            Debug.Log("PickSuspension");
            other.gameObject.GetComponent<ICollideable>().ExicuteCollisionActions();
        }
        else if (other.gameObject.CompareTag("DiamondPickUp"))
        {
            other.gameObject.GetComponent<ICollideable>().ExicuteCollisionActions();
        }
    }

    private void ActivateCollider()
    {
        boxCollider.enabled = true;
    }

    private void DeactivateCollider()
    {
        boxCollider.enabled = false;
    }

    private void ChangeToCanCollide()
    {
        canCollide = true;
    }
}
