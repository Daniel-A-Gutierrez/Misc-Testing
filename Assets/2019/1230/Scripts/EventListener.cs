using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventListener : MonoBehaviour
{
    public GameEvent ListenTo;
    public UnityEvent response;
    public void Response()
    {
        response.Invoke();
    }
    void OnEnable()
    {
        ListenTo.Register(this);
    }
    void OnDisable()
    {
        ListenTo.Deregister(this);
    }

}
