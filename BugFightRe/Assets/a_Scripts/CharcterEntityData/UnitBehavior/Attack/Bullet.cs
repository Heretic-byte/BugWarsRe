using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;



public class Bullet : Projectile
{
    public DamageAble myTargetUnit { get => _targetUnit; private set => _targetUnit = value; }
    DamageAble _targetUnit;

  
    Unit _theBulletCaster;
    public Unit myTheBulletCaster { get => _theBulletCaster; set => _theBulletCaster = value; }

   
    public event UnityAction _OnBulletTrigger;

    Sequence _timerCounter = null;
    public Sequence myTimerCounter { get => _timerCounter; set => _timerCounter = value; }

    [SerializeField]
    private float _bulletMaxLifeDur = 2f;
    public float myBulletMaxLifeDur { get => _bulletMaxLifeDur; set => _bulletMaxLifeDur = value; }

    public override void AddTickToManager()
    {
        base.AddTickToManager();
    }

    public override void FixedTickFloat(float _tick)
    {

        myTrans.position = Vector3.MoveTowards(myTrans.position, myTargetUnit.myTrans.position, myMoveSpeed * _tick);

        if(Vector2.SqrMagnitude(myTargetUnit.myTrans.position - myTrans.position) < Mathf.Pow(0.1f,2))
        {
            _OnBulletTrigger?.Invoke();

            OnEnqueue();
        }
    }

    public override void RemoveTickFromManager()
    {
        base.RemoveTickFromManager();
    }


    public override void SetInstance(Unit _myCaster,UnityAction _onBulletTriggered)
    {
        myTheBulletCaster = _myCaster;
        myTheBulletCaster.myOnAttackTargetDead += OnEnqueue;
        _OnBulletTrigger += _onBulletTriggered;
       
    }
    public override void ShootProjectile( DamageAble _targetUnit)
    {
        if(myTimerCounter!=null)
        {
            myTimerCounter.Kill();
        }

        myTargetUnit = _targetUnit;
       
        AddTickToManager();
        StartCountAutoEnque();
    }

    private void StartCountAutoEnque()
    {
        myTimerCounter = DOTween.Sequence();
        myTimerCounter.SetDelay(myBulletMaxLifeDur).OnComplete(OnEnqueue);
    }

      void OnEnqueue()
    {
        myObj.SetActive(false);
        RemoveTickFromManager();
        myTargetUnit = null;


    }

   
}
