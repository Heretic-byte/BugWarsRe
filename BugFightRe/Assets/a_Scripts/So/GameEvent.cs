using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{

    LinkedList<GameEventListener> listenersList = new LinkedList<GameEventListener>();
    public void Raise()
    {
        
        foreach (var ll in listenersList)
        {
            ll.OnEventRaised();
        }
    }
    public void RegisterListeners(GameEventListener listener )
    {
        listenersList.AddLast(listener);
    }
    public void UnregisterListeners(GameEventListener listener)
    {
        listenersList.Remove(listener);
    }

}
