using System;

public interface UT_IEventService
{
    public delegate void EventHandler<T>(T Event);

    public void SendEvent<T>(T Event = default) where T : UT_Event;

    public void Subscribe<T>(EventHandler<T> Handler);

    public void Unsubscribe<T>(EventHandler<T> Handler);
}
