using UnityEngine;

public class EndGamePoint : MonoBehaviour
{
    private void OnEnable()
    {
        EventBroker.CallOnEndGame();
    }
}
