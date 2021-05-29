using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class PlayerAnimations : MonoBehaviour
{
    [Header("End Game Animations")]  
    [SerializeField] Transform stopPosition;
    [SerializeField] Transform endPosition;
    [SerializeField] float zMoveForwardDuration;
    [SerializeField] float zMoveBackwardDuration;
    [SerializeField] float yAngleDuration;
    [SerializeField] float driftDelay;
    [SerializeField] GameObject driftParticle;

    [Header("Score Count Animations")]
    [SerializeField] float goingDownDuration;
    [SerializeField] float suspensionBreakDuration;
    [SerializeField] float carBodyDownPosition;
    [SerializeField] float carBodyDownDuration;
    [SerializeField] GameObject carBody;
    [SerializeField] List<GameObject> rearWheels;
    [SerializeField] GameObject suspensionsParent;
    [SerializeField] float suspensiondestroyduration = 3f;


    private Stack<GameObject> frontSuspensions;

    #region Subscribe and Unsubscribe to animation events

    private void OnEnable()
    {
        EventBroker.OnEndGamePoint += PlayEndGameAnimations;
        EventBroker.OnScoreCountPoint += PlayScoreCountAnimations;
    }

    private void OnDisable()
    {
        EventBroker.OnEndGamePoint -= PlayEndGameAnimations;
        EventBroker.OnScoreCountPoint -= PlayScoreCountAnimations;
    }

    #endregion

    private void PlayEndGameAnimations()
    {
        Sequence endGameSequence = DOTween.Sequence();

        endGameSequence.Append(transform.DOMove(stopPosition.position, zMoveForwardDuration)).SetEase(Ease.Linear);
        endGameSequence.Join(transform.DORotate(transform.localEulerAngles + new Vector3(0f, 180f, 0f), yAngleDuration).SetDelay(driftDelay).SetEase(Ease.Linear));
        endGameSequence.Append(transform.DOMove(endPosition.position, zMoveBackwardDuration).SetEase(Ease.Linear)
            .OnComplete(EventBroker.CallOnScoreCountPoint));
    }

    private void PlayScoreCountAnimations()
    {
        StartCoroutine(GetSuspensions());
    }

    private IEnumerator GetSuspensions()
    {
        yield return new WaitForSeconds(1.5f);

        frontSuspensions = new Stack<GameObject>();

        foreach (var suspension in CarSuspensions.suspensions)
        {
            frontSuspensions.Push(suspension);
            suspension.GetComponent<SuspensionRagdoll>().rearSuspensions.ForEach(x => x.transform.parent = carBody.transform);
        }

        InvokeRepeating("BreakSuspensions", 0f, suspensiondestroyduration);
        Invoke("MoveCarBodyDown", 0.6f);
    }

    private void MoveCarBodyDown()
    {
        carBody.transform.DOMoveY(carBody.transform.position.y - carBodyDownPosition * frontSuspensions.Count, carBodyDownDuration * frontSuspensions.Count)
            .SetEase(Ease.Linear);
    }
    
    private void  BreakSuspensions()
    {
        if (frontSuspensions.Count <= 0)
        {
            return;
        }

        GameObject currentSuspensionToBreak = frontSuspensions.Peek();

        foreach (var wheel in rearWheels)
        {
            wheel.transform.parent = carBody.transform;
        }

        Destroy(currentSuspensionToBreak); 

        frontSuspensions.Pop();
    }
}
