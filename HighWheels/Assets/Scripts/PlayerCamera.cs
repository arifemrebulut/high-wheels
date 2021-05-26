using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform endGamePosition;
    [SerializeField] float endGamePositionDuration;

    [Header("Score Scene Rotation")]
    [SerializeField] 

    CinemachineVirtualCamera playerFollowCamera;

    private void Awake()
    {
        playerFollowCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        EventBroker.OnEndGamePoint += EndGameAnimations;
    }

    private void OnDisable()
    {
        EventBroker.OnEndGamePoint -= EndGameAnimations;
    }

    private void EndGameAnimations()
    {
        Sequence sequence = DOTween.Sequence();

        playerFollowCamera.m_Follow = null;

        sequence.Append(transform.DOMove(endGamePosition.position, endGamePositionDuration));
    }
}
