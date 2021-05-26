using System;
using UnityEngine;

public class EventBroker
{
    public static Action OnPickUpSuspension;
    public static Action OnPickUpDiamond;
    public static Action<int> OnHitToBlock;

    public static Action OnGameWin;
    public static Action OnGameOver;
    public static Action OnEndGamePoint;
    public static Action OnScoreCountPoint;

    public static void CallOnPickUpSuspension()
    {
        OnPickUpSuspension?.Invoke();
    }

    public static void CallOnPickUpDiamond()
    {
        OnPickUpDiamond?.Invoke();
    }

    public static void CallOnHitToBlock(int _blockHeight)
    {
        OnHitToBlock?.Invoke(_blockHeight);
    }

    public static void CallOnGameWin()
    {
        OnGameWin?.Invoke();
    }

    public static void CallOnGameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void CallOnEndGamePoint()
    {
        OnEndGamePoint?.Invoke();
    }

    public static void CallOnScoreCountPoint()
    {
        Debug.Log("OnScoreCountPoint");
        OnScoreCountPoint?.Invoke();
    }
}
