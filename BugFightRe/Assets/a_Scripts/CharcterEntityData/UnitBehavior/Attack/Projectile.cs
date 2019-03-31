using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour,IfixedTickFloat
{
    public GameManager myManagerGame { get; set; }
    public Transform myTrans { get;private set; }
    public GameObject myObj { get; private set; }
    public Transform myTargetTrans { get; private set; }
   
   protected virtual void Awake()
    {
        myManagerGame = GameManager.myInstance;
        myTrans = transform;
        myObj = gameObject;
    }

    public virtual void SetTargetToMove(Transform _targetTrans)
    {

        myTargetTrans = _targetTrans;
    }
   

    public abstract void AddTickToManager();

    public abstract void FixedTickFloat(float _tick);

    public abstract void RemoveTickFromManager();


    protected abstract void OnTriggerEnter2D(Collider2D coll);
 
}
