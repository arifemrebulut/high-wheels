using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block") && CarSuspensions.currentSuspensionCount == 0)
        {
            Queue<GameObject> currentSuspensions = CarSuspensions.suspensions;

            EventBroker.CallOnGameOver();
        }
        else
        {
            ICollideable collideable = other.gameObject.GetComponent<ICollideable>();

            if (collideable != null)
            {
                collideable.ExicuteCollisionActions();
            }
        }       
    }
}
