using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarSuspensions : MonoBehaviour
{
    #region Old
    //[SerializeField] Transform carBody;
    //[SerializeField] GameObject suspensionsParent;
    //[SerializeField] float carBodyYValue;
    //[SerializeField] float firstSuspensionOffset;

    //List<Transform> suspensions;

    //private int suspensionIndex = 0;
    //private float yValue;

    //#region Subscribe and Unsubscribe to collision events

    //private void OnEnable()
    //{
    //    EventBroker.OnPickUpSuspension += GainSuspension;
    //    EventBroker.OnHitToBlock += LoseSuspension;
    //}

    //private void OnDisable()
    //{
    //    EventBroker.OnPickUpSuspension -= GainSuspension;
    //    EventBroker.OnHitToBlock -= LoseSuspension;
    //}

    //#endregion

    //private void Awake()
    //{
    //    AddAllSuspensionsToList();
    //}

    //private void AddAllSuspensionsToList()
    //{
    //    suspensions = new List<Transform>();

    //    foreach (Transform suspension in suspensionsParent.transform)
    //    {
    //        suspensions.Add(suspension.transform);
    //    }
    //}

    //private void GainSuspension()
    //{
    //    if (suspensionIndex == 0)
    //    {
    //        yValue = carBodyYValue + firstSuspensionOffset;
    //    }

    //    AdjustCarBodyYPosition(yValue);

    //    suspensions[suspensionIndex].gameObject.SetActive(true);

    //    suspensionIndex++;
    //    yValue = carBodyYValue;
    //}

    //private void LoseSuspension()
    //{
    //    if (suspensionIndex != 0)
    //    {
    //        if (suspensionIndex == 1)
    //        {
    //            yValue = carBodyYValue + firstSuspensionOffset;
    //        }

    //        AdjustCarBodyYPosition(-yValue);

    //        suspensionIndex--;

    //        suspensions[suspensionIndex].gameObject.SetActive(false);
    //        yValue = carBodyYValue;
    //    }
    //    else
    //    {
    //        Debug.Log("Game Over");
    //    }
    //}

    //private void AdjustCarBodyYPosition(float _yValue)
    //{
    //    carBody.localPosition += new Vector3(0f, _yValue, 0f);
    //}
    #endregion

    [SerializeField] private GameObject suspensionPrefab;
    [SerializeField] private GameObject carBody;
    [SerializeField] private Transform suspensionsParent;
    [SerializeField] private Transform wheelsParent;

    [SerializeField] float carBodyYIncreaseAmount;
    [SerializeField] float suspensionYIncreaseAmount;

    private Queue<GameObject> suspensions;

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
        suspensions = new Queue<GameObject>();
    }

    private void GainSuspension()
    {
        // Adjust Y position value of carBody and suspensions on the car

        if (suspensions.Count == 0)
        {
            carBody.transform.position += new Vector3(0f, carBodyYIncreaseAmount + 0.1f, 0f);

            GameObject newSuspension = Instantiate(suspensionPrefab, suspensionsParent);

            suspensions.Enqueue(newSuspension);
        }
        else
        {
            carBody.transform.position += new Vector3(0f, carBodyYIncreaseAmount, 0f);

            GameObject newSuspension = Instantiate(suspensionPrefab, suspensionsParent);

            newSuspension.transform.localPosition += new Vector3(0f, suspensions.Last().transform.localPosition.y + 0.625f, 0f);

            suspensions.Enqueue(newSuspension);
        }
    }

    private void LoseSuspension()
    {
        GameObject suspensionToDelete = suspensions.Peek();

        suspensions.Dequeue();

        Destroy(suspensionToDelete);

        wheelsParent.transform.position = suspensions.Peek().transform.position - new Vector3(0f, 0.26f, 0f);
    }
}
