using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
        myManagerGame = GameManager.GetInstance;
        myTrans = transform;
        myObj = gameObject;

    }
  

    public virtual void AddTickToManager()
    {
        GameManager.GetInstance.AddScaledTickToManager(FixedTickFloat);
    }
    public virtual void RemoveTickFromManager()
    {
        GameManager.GetInstance.RemoveScaledTickFromManager(FixedTickFloat);
    }

    public abstract void FixedTickFloat(float _tick);

    public abstract void SetInstance(Unit _myCaster, UnityAction _onBulletTriggered);
    public abstract void ShootProjectile(DamageAble _targetUnit);


}
