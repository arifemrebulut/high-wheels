using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform endGamePosition;
    [SerializeField] float endGamePositionDuration;

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

        playerFollowCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 0;

        playerFollowCamera.m_Follow = null;

        sequence.Append(transform.DOMove(endGamePosition.position, endGamePositionDuration));
        sequence.Join(transform.DORotate(new Vector3(transform.rotation.x, 0f, transform.rotation.z), endGamePositionDuration));

    
    }
}
