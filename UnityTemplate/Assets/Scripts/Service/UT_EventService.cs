using System;
using System.Collections.Generic;
using UnityEngine;



public class UT_EventService : MonoBehaviour, UT_IEventService
{
    private UT_EventManager _EventManager;

    public UT_EventService(UT_EventManager EventManager)
    {
        _EventManager = EventManager;
    }

    public void SendEvent<T>(T Event = null) where T : UT_Event
    {
        if (_EventManager == null)
            return;

        _EventManager.SendEvent(Event);
    }

    public void Subscribe<T>(UT_IEventService.EventHandler<T> Handler)
    {
        if (_EventManager == null)
            return;

        _EventManager.Subscribe(Handler);
    }

    public void Unsubscribe<T>(UT_IEventService.EventHandler<T> Handler)
    {
        if (_EventManager == null)
            return;

        _EventManager.Unsubscribe(Handler);
    }
}
