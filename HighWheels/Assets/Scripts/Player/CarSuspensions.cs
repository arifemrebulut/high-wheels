using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class CarSuspensions : MonoBehaviour
{
    [Header("Suspension Animation Durations")]
    [SerializeField] private float gainSuspensionTime;
    [SerializeField] private float loseSuspensionTime;
    [SerializeField] private float loseSuspensionDelay;
    
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
        if (suspensions.Count == 0)
        {
            carBody.transform.DOMoveY((carBody.transform.position.y + yIncreaseAmount + carBodyOffset), gainSuspensionTime).SetEase(Ease.OutBack);

            GameObject newSuspension = Instantiate(suspensionPrefab, suspensionsParent);

            suspensions.Enqueue(newSuspension);

            AddComponentToFistSuspension();
        }
        else
        {
            carBody.transform.DOMoveY(carBody.transform.position.y + yIncreaseAmount, gainSuspensionTime).SetEase(Ease.OutBack);

            GameObject newSuspension = Instantiate(suspensionPrefab, suspensionsParent);

            newSuspension.transform.localPosition += new Vector3(0f, suspensions.Last().transform.localPosition.y + yIncreaseAmount, 0f);

            suspensions.Enqueue(newSuspension);
        }
    }

    private void LoseSuspension(int _loseAmount)
    {

        if (suspensions.Count - _loseAmount <= 0)
        {
            EventBroker.CallOnGameOver();
        }
        else
        {
            for (int i = 0; i < _loseAmount; i++)
            {
                GameObject suspensionToDelete = suspensions.Peek();

                suspensions.Dequeue();

                Destroy(suspensionToDelete);
            }

            float yDecreaseAmount = yIncreaseAmount * _loseAmount;

            if (suspensions.Count > 1)
            {
                carBody.transform.DOMoveY(carBody.transform.position.y - yDecreaseAmount, loseSuspensionTime)
                    .SetEase(Ease.InBack).SetDelay(loseSuspensionDelay);
            }
            else
            {
                carBody.transform.DOMoveY(carBody.transform.position.y - (yDecreaseAmount + carBodyOffset), loseSuspensionTime)
                    .SetEase(Ease.OutBack).SetDelay(loseSuspensionDelay);
            }

            foreach (var suspension in suspensions)
            {

                suspension.transform.DOLocalMoveY(suspension.transform.localPosition.y - yDecreaseAmount, loseSuspensionTime)
                    .SetEase(Ease.OutBack).SetDelay(loseSuspensionDelay)
                    .OnComplete(AddComponentToFistSuspension);
            }
        }     
    }

    private void AddComponentToFistSuspension()
    {
        if (suspensions.Count > 0)
        {
            suspensions.Peek().AddComponent<SuspensionRayCheck>();
        }
        else
        {
            Debug.Log("Queue is empty!");
        }
    }   
}
