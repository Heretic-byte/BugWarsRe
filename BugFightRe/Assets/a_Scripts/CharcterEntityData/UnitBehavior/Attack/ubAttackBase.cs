using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class ubAttackBase : myUnitBehavior
{
    public float myAttackSpeed { get; set; }
    public float myAttackRange { get; set; }

    protected Vector3 _attackDir = Vector3.right;
    protected ColliderDicSingletone myManagerCollDic;
    protected float _attackTimer = 0f;

    protected RaycastHit2D targetHitten;
    protected Vector3 rayOrigin;

 
    public override void SetInstance()
    {
        myUnit = GetComponent<Unit>();
        myTrans = myUnit.myTrans;
        SetTranslateDir();

        myAttackSpeed = myUnit.myStat.m_BaseAttackSpeed;
        myAttackRange = myUnit.myStat.m_BaseAttackRange;

        myManagerCollDic = ColliderDicSingletone.myInstance;


        myUnit.myOnDequeueAction += AddTickToManager;
        myUnit.myOnEnqueueAction += RemoveTickFromManager;
     
    }
   protected virtual void SetTranslateDir()
    {
        if (!myUnit.myIsFacingRight)
        {
            _attackDir = Vector3.left;
        }
        else
        {
            _attackDir = Vector3.right;
        }
    }
 
 
    public override void FixedTickFloat(float _tick)
    {
        _attackTimer += _tick;
        TryAttack();
    }
    protected abstract void TryAttack();

    public abstract void Attack();

}
