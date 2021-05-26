using UnityEngine;

public class EndGamePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventBroker.CallOnEndGamePoint();
        }
    }
}
