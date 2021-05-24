using System.Collections.Generic;
using UnityEngine;

public class CarSuspensions : MonoBehaviour
{
    [SerializeField] Transform carBody;
    [SerializeField] GameObject suspensionsParent;
    [SerializeField] float carBodyYValue;
    [SerializeField] float firstSuspensionOffset;

    List<Transform> suspensions;

    private int suspensionIndex = 0;
    private float yValue;

    #region Subscribe and Unsubscribe to collision events

    private void OnEnable()
    {
        EventBroker.OnPickUpSuspension += GainSuspension;
        EventBroker.OnHitToBlock += LoseSuspension;
    }

    private void OnDisable()
    {
        EventBroker.OnPickUpSuspension -= GainSuspension;
        EventBroker.OnHitToBlock -= LoseSuspension;
    }

    #endregion

    private void Awake()
    {
        AddAllSuspensionsToList();
    }

    private void AddAllSuspensionsToList()
    {
        suspensions = new List<Transform>();

        foreach (Transform suspension in suspensionsParent.transform)
        {
            suspensions.Add(suspension.transform);
        }
    }

    private void GainSuspension()
    {
        if (suspensionIndex == 0)
        {
            yValue = carBodyYValue + firstSuspensionOffset;
        }

        AdjustCarBodyYPosition(yValue);

        suspensions[suspensionIndex].gameObject.SetActive(true);

        suspensionIndex++;
        yValue = carBodyYValue;
    }

    private void LoseSuspension()
    {
        if (suspensionIndex != 0)
        {
            if (suspensionIndex == 1)
            {
                yValue = carBodyYValue + firstSuspensionOffset;
            }

            AdjustCarBodyYPosition(-yValue);

            suspensionIndex--;

            suspensions[suspensionIndex].gameObject.SetActive(false);
            yValue = carBodyYValue;
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

    private void AdjustCarBodyYPosition(float _yValue)
    {
        carBody.localPosition += new Vector3(0f, _yValue, 0f);
    }
}
