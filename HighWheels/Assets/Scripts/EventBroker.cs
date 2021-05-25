using System;

public class EventBroker
{
    public static Action OnPickUpSuspension;
    public static Action OnPickUpDiamond;
    public static Action<int> OnHitToBlock;

    public static Action OnGameOver;

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

    public static void CallOnGameOver()
    {
        OnGameOver?.Invoke();
    }
}
