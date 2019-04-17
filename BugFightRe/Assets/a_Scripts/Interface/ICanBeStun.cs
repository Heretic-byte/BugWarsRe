using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface ICanBeStun 
{
 

    UnityAction myOnStuned { get; set; }
    void GetStunDur(float _dur);

}
