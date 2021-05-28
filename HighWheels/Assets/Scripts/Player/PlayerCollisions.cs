using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private bool canCollide = true;

    private void Update()
    {
        Debug.Log(canCollide);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            if (CarSuspensions.currentSuspensionCount == 0 && canCollide)
            {
                EventBroker.CallOnGameOver();
            }
            else if (CarSuspensions.currentSuspensionCount > 0 && canCollide)
            {
                ICollideable collideable = other.gameObject.GetComponent<ICollideable>();

                collideable.ExicuteCollisionActions();
                canCollide = false;
                Invoke("ChangeToCanCollide", 1f);
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            canCollide = true;
        }
    }

    private void ChangeToCanCollide()
    {
        canCollide = true;
    }
}
