using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UnitStatement { Walk,Idle, Attack, Death, Stun }
public abstract class Unit : DamageAble
{
    [SerializeField]
    Vector3 _rayCastOffset =Vector3.up;


    UnitStatement _unitStatement = UnitStatement.Walk;
    public UnitStatement myUnitStatement { get => _unitStatement; set => _unitStatement = value; }


    myUnitBehavior[] _unitBehaviors;
    public myUnitBehavior[] myUnitBehaviors { get => _unitBehaviors; set => _unitBehaviors = value; }

    public LayerMask myTargetLayers { get => _targetLayers; set => _targetLayers = value; }
    [SerializeField]
    protected LayerMask _targetLayers;

    public LayerMask myAllyLayers { get => _allyLayers; set => _allyLayers = value; }
    [SerializeField]
    protected LayerMask _allyLayers;
    [SerializeField]
    private float _deathDelay = 3f;
    public float myDeathDelay { get => _deathDelay; }

    public DamageAble myBlockingAlly { get;set; }
    public DamageAble myAttackTarget { get; set; }
  
    public UnityAction myOnAttack { get; set ; }
    public UnityAction myOnWalking { get ; set; }
    public UnityAction myOnNotWalking { get; set; }
    public UnityAction myOnAttackTargetDead { get; set; }
    public UnityAction myOnRushBattleField { get; set; }
   
    public Vector3 myRayCastOffset { get => _rayCastOffset; set => _rayCastOffset = value; }

    protected override void MainSetInstance()
    {
        base.MainSetInstance();

        myUnitBehaviors = GetComponentsInChildren<myUnitBehavior>();
        
        foreach(var myB in myUnitBehaviors)
        {
            myB.SetInstance();
        }
    }
    public void AddBehavTick()
    {
        foreach (var behav in myUnitBehaviors)
        {
            behav.AddTickToManager();
        }
    }
    public void RemoveBehavTick()
    {
        foreach (var behav in myUnitBehaviors)
        {
            behav.RemoveTickFromManager();
        }
    }
    public virtual void SetAttackTargetNull()
    {
        myAttackTarget = null;
    }
    public virtual void AttackTargetDead()
    {
        SetAttackTargetNull();
        myOnAttackTargetDead?.Invoke();
    }

    public override void SetHpToMax()
    {
        base.SetHpToMax();
        myOnHealAction?.Invoke();
    }
    public void SetPosition(Vector3 pos)
    {
        myTrans.position = pos;
    }
    public virtual void GoRushBattleField(Vector3 _pos)
    {
        Debug.Log("RUSH2");
        SetPosition(_pos);
        myOnRushBattleField?.Invoke();
        AddBehavTick();
    }

}

