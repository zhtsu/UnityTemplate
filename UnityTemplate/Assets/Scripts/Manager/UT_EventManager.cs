using System;
using System.Collections.Generic;

public class UT_EventManager : UT_Manager
{
    override public string ManagerName => "Event Manager";

    private readonly object _lock = new object();
    private readonly Dictionary<Type, Delegate> _EventHandlers = new Dictionary<Type, Delegate>();

    override public void Initialize()
    {
        _EventHandlers.Clear();
    }

    override public void Destroy()
    {
        _EventHandlers.Clear();
    }

    public void SendEvent<T>(T Event = default)
        where T : UT_Event
    {
        Type EventType = typeof(T);
        Delegate Handlers = null;

        lock (_lock)
        {
            _EventHandlers.TryGetValue(EventType, out Handlers);
        }

        if (Handlers is UT_IEventService.EventHandler<T> TypedHandler)
        {
            TypedHandler?.Invoke(Event);
        }
    }

    public void Subscribe<T>(UT_IEventService.EventHandler<T> Handler)
    {
        lock (_lock)
        {
            Type EventType = typeof(T);

            if (_EventHandlers.TryGetValue(EventType, out Delegate ExistingDelegate))
            {
                _EventHandlers[EventType] = Delegate.Combine(ExistingDelegate, Handler);
            }
            else
            {
                _EventHandlers[EventType] = Handler;
            }
        }
    }

    public void Unsubscribe<T>(UT_IEventService.EventHandler<T> Handler)
    {
        lock (_lock)
        {
            Type EventType = typeof(T);

            if (_EventHandlers.TryGetValue(EventType, out Delegate ExistingDelegate))
            {
                Delegate NewDelegate = Delegate.Remove(ExistingDelegate, Handler);

                if (NewDelegate == null)
                {
                    _EventHandlers.Remove(EventType);
                }
                else
                {
                    _EventHandlers[EventType] = NewDelegate;
                }
            }
        }
    }
}