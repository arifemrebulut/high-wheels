using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class CarSuspensions : MonoBehaviour
{
    [Header("Suspension Animation Durations")]
    [SerializeField] private float gainSuspensionTime;
    [SerializeField] private float loseSuspensionTime;
    
    [Space]
    [SerializeField] private float carBodyOffset;
    [SerializeField] float yIncreaseAmount;

    [SerializeField] private GameObject suspensionPrefab;
    [SerializeField] private GameObject carBody;  
    [SerializeField] private Transform suspensionsParent;
    [SerializeField] private Transform wheelsParent;

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
            carBody.transform.DOMoveY((transform.position.y + yIncreaseAmount + carBodyOffset), gainSuspensionTime).SetEase(Ease.OutBack);

            GameObject newSuspension = Instantiate(suspensionPrefab, suspensionsParent);

            newSuspension.AddComponent<SuspensionRayCheck>();

            suspensions.Enqueue(newSuspension);
        }
        else
        {
            carBody.transform.position += new Vector3(0f, yIncreaseAmount, 0f);

            GameObject newSuspension = Instantiate(suspensionPrefab, suspensionsParent);

            newSuspension.transform.localPosition += new Vector3(0f, suspensions.Last().transform.localPosition.y + yIncreaseAmount, 0f);

            suspensions.Enqueue(newSuspension);
        }
    }

    private void LoseSuspension(int _loseAmount)
    {
        for (int i = 0; i < _loseAmount; i++)
        {
            GameObject suspensionToDelete = suspensions.Peek();

            suspensions.Dequeue();

            Destroy(suspensionToDelete);           

            if (suspensions.Count > 0)
            {
                carBody.transform.position -= new Vector3(0f, yIncreaseAmount, 0f);
            }
            else
            {
                carBody.transform.position -= new Vector3(0f, yIncreaseAmount + carBodyOffset, 0f);
            }

            foreach (var suspension in suspensions)
            {
                suspension.transform.localPosition -= new Vector3(0f, suspensions.Last().transform.localPosition.y + yIncreaseAmount, 0f);
            }
        }
    }
}
