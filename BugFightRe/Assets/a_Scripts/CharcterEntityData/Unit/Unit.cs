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

    public DamageAble myBlockingAlly { get => _BlockingAlly; set => _BlockingAlly = value; }
    public DamageAble myAttackTarget { get => _AttackTarget; set => _AttackTarget = value; }
    DamageAble _AttackTarget;
    DamageAble _BlockingAlly;

    public UnityAction myOnAttack { get => _onAttack; set => _onAttack = value; }
    UnityAction _onAttack;
    public UnityAction myOnWalking { get => _onWalking; set => _onWalking = value; }
    public UnityAction myOnNotWalking { get => _onNotWalking; set => _onNotWalking = value; }
    public Vector3 myRayCastOffset { get => _rayCastOffset; set => _rayCastOffset = value; }

    UnityAction _onWalking;
    UnityAction _onNotWalking;
    public UnityAction OnDequeueAction { get; set; }
    public UnityAction OnEnqueueAction { get; set; }
    public UnityAction myOnAttackTargetDead { get; set; }

    protected override void MainSetInstance()
    {
        base.MainSetInstance();

        myUnitBehaviors = GetComponents<myUnitBehavior>();
        
        foreach(var myB in myUnitBehaviors)
        {
            myB.SetInstance();
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
  
}

