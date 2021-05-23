using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static Action<float> OnTouchDragEvent;

    private float xMovementFactor;
    private float touchStartXPosition;

    private bool isTouching;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnTouchStart();
        }

        if (isTouching)
        {
            OnTouchDrag();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            OnTouchEnd();
        }
    }

    public void OnTouchStart()
    {
        isTouching = true;

        touchStartXPosition = Input.GetTouch(0).position.x;
    }

    public void OnTouchDrag()
    {
        xMovementFactor = Input.GetTouch(0).position.x - touchStartXPosition;

        OnTouchDragEvent(xMovementFactor);
    }

    public void OnTouchEnd()
    {
        isTouching = false;
        xMovementFactor = 0f;
    }
}
