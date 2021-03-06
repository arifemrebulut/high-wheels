using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float deltaXMultiplier;

    private float touchStartXPosition;
    public float deltaX { get; private set; }

    public static Action<float> OnTouchMovedEvent;
    public static Action OnTouchBeganEvent;

    private bool isGameStart;

    CanvasManager canvasManager;

    private void Update()
    {
        if (!isGameStart && Input.touchCount > 0)
        {
            isGameStart = true;
            EventBroker.CallOnGameStart();
        }

        if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchStartXPosition = Input.GetTouch(0).position.x;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                deltaX = (Input.GetTouch(0).position.x - touchStartXPosition) * deltaXMultiplier;
                touchStartXPosition = Input.GetTouch(0).position.x;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                deltaX = 0f;
            }
        }
    }
}