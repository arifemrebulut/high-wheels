using UnityEngine;
using DG.Tweening;

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

    #region Subscribe and Unsubscribe to animation events

    private void OnEnable()
    {
        EventBroker.OnEndGamePoint += PlayEndGameAnimations;
    }

    private void OnDisable()
    {
        EventBroker.OnEndGamePoint -= PlayEndGameAnimations;
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
}
