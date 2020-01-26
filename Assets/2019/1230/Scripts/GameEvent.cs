using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data" , menuName ="ScriptableObjects/GameEvent" , order = 2)]
public class GameEvent : ScriptableObject
{
    private List<EventListener> listeners;
    public void Raise()
    {
        foreach(EventListener el in listeners)
        {
            el.Response();
        }
    }

    public bool Register(EventListener newListener)
    {
        foreach(EventListener el in listeners)
        {
            if(el == newListener)
                return false;
        }
        listeners.Add(newListener);
        return true;
    }

    public bool Deregister(EventListener listener)
    {
       for(int i = 0 ; i  < listeners.Count ; i++)
        {
            if(listeners[i] == listener)
                listeners.RemoveAt(i);
            return true;
        } 
        return false;
    }
}
