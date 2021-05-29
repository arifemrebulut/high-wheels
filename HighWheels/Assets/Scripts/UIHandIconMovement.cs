using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIHandIconMovement : MonoBehaviour
{
    [SerializeField] float xMovementAmount;
    [SerializeField] float xMovementDuration;

    Image image;
    private float startXPosition;

    private void Start()
    {
        image = GetComponent<Image>();
        startXPosition = image.rectTransform.localPosition.x;
        MoveLeft();
    }

    private void MoveLeft()
    {
        image.rectTransform.DOLocalMoveX(startXPosition - xMovementAmount, xMovementDuration).SetLoops(2, LoopType.Yoyo)
            .OnComplete(MoveRight);
    }

    private void MoveRight()
    {
        image.rectTransform.DOLocalMoveX(startXPosition + xMovementAmount, xMovementDuration).SetLoops(2, LoopType.Yoyo)
            .OnComplete(MoveLeft);
    }
}
