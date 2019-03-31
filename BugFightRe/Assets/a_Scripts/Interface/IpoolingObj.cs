using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void EnqueueMySelfDele(GameObject mySelf);

public interface IpoolingObj
{
    EnqueueMySelfDele OnEnqueueMySelfDele { get; set; }
    void OnDequeue();
    void OnEnqueue();
}
