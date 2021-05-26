using UnityEngine;
using DG.Tweening;

public class PlayerAnimations : MonoBehaviour
{
    [Header("End Game Animations")]
    [SerializeField] float forwardZPosition;
    [SerializeField] float backwardZposition;
    [SerializeField] float zMoveForwardDuration;
    [SerializeField] float zMoveBackwardDuration;
    [SerializeField] float yAngleDuration;
    [SerializeField] float driftDelay;
    [SerializeField] GameObject driftParticle;

    public void PlayEndGameAnimations()
    {
        Sequence endGameSequence = DOTween.Sequence();

        endGameSequence.Append(transform.DOMoveZ(transform.position.z + forwardZPosition, zMoveForwardDuration));
        endGameSequence.Join(transform.DORotate(transform.localEulerAngles + new Vector3(0f, 180f, 0f), yAngleDuration).SetDelay(driftDelay));
        endGameSequence.Append(transform.DOMoveZ(transform.position.z - backwardZposition, zMoveBackwardDuration));
    }
}
