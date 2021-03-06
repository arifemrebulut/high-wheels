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
    [SerializeField] private float yIncreaseAmount;

    [SerializeField] private GameObject suspensionPrefab;
    [SerializeField] private GameObject carBody;  
    [SerializeField] private Transform suspensionsParent;
    [SerializeField] private Transform wheelsParent;

    private Vector3 carBodyDefaultPosition;

    public static int currentSuspensionCount { get; private set; }
    public static Queue<GameObject> suspensions { get; private set; }

    DG.Tweening.Tween gainTween;

    #region Subscribe and Unsubscribe to collision events

    private void OnEnable()
    {
        EventBroker.OnPickUpSuspension += GainSuspension;
        EventBroker.OnHitToBlock += LoseSuspension;
        EventBroker.OnGameOver += ResetCarBodyPosition;
    }

    private void OnDisable()
    {
        EventBroker.OnPickUpSuspension -= GainSuspension;
        EventBroker.OnHitToBlock -= LoseSuspension;
        EventBroker.OnGameOver -= ResetCarBodyPosition;
    }

    #endregion

    private void Awake()
    {
        suspensions = new Queue<GameObject>();
        carBodyDefaultPosition = carBody.transform.position;
    }

    private void Update()
    {
        currentSuspensionCount = suspensions.Count;
    }

    private void GainSuspension()
    {
        if (suspensions.Count == 0)
        {
            gainTween = carBody.transform.DOMoveY((carBody.transform.position.y + yIncreaseAmount + carBodyOffset), gainSuspensionTime).SetEase(Ease.OutBack);

            GameObject newSuspension = Instantiate(suspensionPrefab, suspensionsParent);

            suspensions.Enqueue(newSuspension);
        }
        else
        {
            if (gainTween.IsPlaying())
            {
                gainTween.Complete();
                gainTween = carBody.transform.DOMoveY(carBody.transform.position.y + yIncreaseAmount, gainSuspensionTime).SetEase(Ease.OutBack);
            }
            else
            {
                gainTween = carBody.transform.DOMoveY(carBody.transform.position.y + yIncreaseAmount, gainSuspensionTime).SetEase(Ease.OutBack);
            }

            GameObject newSuspension = Instantiate(suspensionPrefab, suspensionsParent);

            newSuspension.transform.localPosition += new Vector3(0f, suspensions.Last().transform.localPosition.y + yIncreaseAmount, 0f);

            suspensions.Enqueue(newSuspension);
        }
    }

    private void LoseSuspension(int _loseAmount)
    {
        Debug.Log(_loseAmount);

        for (int i = 0; i < _loseAmount; i++)
        {
            GameObject suspensionToDelete = suspensions.Peek();

            suspensions.Dequeue();

            suspensionToDelete.GetComponent<SuspensionRagdoll>().ActivateRagdoll();
        }

        float yDecreaseAmount = yIncreaseAmount * _loseAmount;

        if (suspensions.Count > 1)
        {

            carBody.transform.DOMoveY(carBody.transform.position.y - yDecreaseAmount, loseSuspensionTime)
                .SetEase(Ease.OutBack).SetDelay(loseSuspensionDelay);
        }
        else
        {
            carBody.transform.DOMoveY(carBody.transform.position.y - (yDecreaseAmount + carBodyOffset), loseSuspensionTime)
                .SetEase(Ease.OutBack).SetDelay(loseSuspensionDelay);
        }

        foreach (var suspension in suspensions)
        {
            suspension.transform.DOLocalMoveY(suspension.transform.localPosition.y - yDecreaseAmount, loseSuspensionTime)
                .SetEase(Ease.OutBack).SetDelay(loseSuspensionDelay);
        }
    }
    
    private void ResetCarBodyPosition()
    {
        carBody.AddComponent<BoxCollider>();
        carBody.AddComponent<Rigidbody>();
    }
}
