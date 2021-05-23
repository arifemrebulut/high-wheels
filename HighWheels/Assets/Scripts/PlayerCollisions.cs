using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ICollideable collideable = collision.gameObject.GetComponent<ICollideable>();

        if (collideable != null)
        {
            collideable.ExicuteCollisionActions();
        }
    }
}
