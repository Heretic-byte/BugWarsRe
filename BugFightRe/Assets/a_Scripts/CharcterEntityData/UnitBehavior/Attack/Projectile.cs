using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour,IfixedTickFloat
{
    public GameManager myManagerGame { get; set; }
    public Transform myTrans { get;private set; }
    public GameObject myObj { get; private set; }
    public float myMoveSpeed { get => _MoveSpeed; set => _MoveSpeed = value; }

    [SerializeField]
    float _MoveSpeed;
    protected virtual void Awake()
    {
        myManagerGame = GameManager.myInstance;
        myTrans = transform;
        myObj = gameObject;

    }
  

    public virtual void AddTickToManager()
    {
        GameManager.myInstance.AddScaledTickToManager(FixedTickFloat);
    }
    public virtual void RemoveTickFromManager()
    {
        GameManager.myInstance.RemoveScaledTickFromManager(FixedTickFloat);
    }

    public abstract void FixedTickFloat(float _tick);

  

    protected abstract void OnTriggerEnter2D(Collider2D coll);

   
}
