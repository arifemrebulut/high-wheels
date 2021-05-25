using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI diamondCount;

    private int totalDiamonds;

    #region Subscribe and unsubscribe to events

    private void OnEnable()
    {
        EventBroker.OnPickUpDiamond += IncreaseDiamond;
    }

    private void OnDisable()
    {
        EventBroker.OnPickUpDiamond -= IncreaseDiamond;
    }

    #endregion

    private void IncreaseDiamond()
    {
        diamondCount.text = (totalDiamonds + 1).ToString();
    }

    private void DecreaseDiamond(int decreaseAmount)
    {
        diamondCount.text = (totalDiamonds - decreaseAmount).ToString();
    }
}
