using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private GameEvent _gameEvent;

    public UnityEvent m_Response;

    public GameEvent m_GameEvent { get => _gameEvent; }

    public void RegisterEvent()
    {
        m_GameEvent.RegisterListeners(this);
    }
    public void UnregisterEvent()
    {
        m_GameEvent.UnregisterListeners(this);
    }
    public void OnEventRaised()
    {
        m_Response?.Invoke();
    }

    
}
