using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ICollideable collideable = other.gameObject.GetComponent<ICollideable>();

        if (collideable != null && other.gameObject.CompareTag("SuspensionPickUp"))
        {
            collideable.ExicuteCollisionActions();
        }
    }
}
