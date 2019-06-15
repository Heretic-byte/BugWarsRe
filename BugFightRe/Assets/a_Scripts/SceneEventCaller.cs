using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SceneEventCaller : MonoBehaviour
{

    [SerializeField]
    UnityEvent _OnAwake;
    public UnityEvent m_OnAwake { get => _OnAwake; }
    [SerializeField]
    UnityEvent _OnStart;
    public UnityEvent m_Start { get => _OnStart; }

    private void Awake()
    {
        m_OnAwake?.Invoke();
    }
    void Start()
    {
        m_Start?.Invoke();
    }
    
}
