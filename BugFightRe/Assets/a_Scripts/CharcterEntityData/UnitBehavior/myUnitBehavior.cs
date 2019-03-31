using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]

public abstract class myUnitBehavior : MonoBehaviour, IfixedTickFloat
{
    protected Unit _unit;
    public Unit myUnit { get => _unit; set => _unit = value; }
    public GameManager myManagerGame { get; set ; }
    protected Transform myTrans { get; set; }

    public abstract void AddTickToManager();
  

    public abstract void FixedTickFloat(float _tick);

    public abstract void RemoveTickFromManager();
  

    public abstract void SetInstance();

}
