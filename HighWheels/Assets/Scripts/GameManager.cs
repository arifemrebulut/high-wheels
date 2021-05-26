using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI diamondCount;

    private int totalDiamonds;

    #region Subscribe and unsubscribe to events

    private void OnEnable()
    {
        EventBroker.OnPickUpDiamond += TakeDiamond;
    }

    private void OnDisable()
    {
        EventBroker.OnPickUpDiamond -= TakeDiamond;
    }

    #endregion

    private void TakeDiamond()
    {
        totalDiamonds++;
        diamondCount.text = totalDiamonds.ToString();
    }

    private void IncreaseDiamond(int increaseAmount)
    {
        totalDiamonds += increaseAmount;
        diamondCount.text = totalDiamonds.ToString();
    }

    private void DecreaseDiamond(int decreaseAmount)
    {
        totalDiamonds -= decreaseAmount;
        diamondCount.text = totalDiamonds.ToString();
    }
}
