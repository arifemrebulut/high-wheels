using System;

public class EventBroker
{
    public static Action OnPickUpSuspension;
    public static Action OnPickUpDiamond;
    public static Action OnHitToBlock;

    public static void CallOnPickUpSuspension()
    {
        OnPickUpSuspension?.Invoke();
    }

    public static void CallOnPickUpDiamond()
    {
        OnPickUpDiamond?.Invoke();
    }

    public static void CallOnHitToBlock()
    {
        OnHitToBlock?.Invoke();
    }
}
