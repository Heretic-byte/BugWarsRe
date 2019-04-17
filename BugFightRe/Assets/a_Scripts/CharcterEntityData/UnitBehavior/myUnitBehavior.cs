using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]

public abstract class myUnitBehavior : MonoBehaviour, IfixedTickFloat
{
    
    public Unit myUnit { get; protected set; }
    public GameManager myManagerGame { get; set ; }
    protected Transform myTrans { get; set; }

    public abstract void AddTickToManager();
  

    public abstract void FixedTickFloat(float _tick);

    public abstract void RemoveTickFromManager();
  

    public abstract void SetInstance();

}
