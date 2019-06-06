using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface ICanBeStun 
{
 

    UnityAction<int> myOnCancel { get; set; }
    void GetStunDur(float _dur);

}
