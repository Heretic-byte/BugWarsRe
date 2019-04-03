using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;



public class Bullet : Projectile
{
    public DamageAble myTargetUnit { get; private set; }

    float _BulletDamage;
    public float myBulletDamage { get => _BulletDamage; set => _BulletDamage = value; }

    int _TargetHash;
    public int myTargetHash { get => _TargetHash; set => _TargetHash = value; }

    Unit _theBulletCaster;
    public Unit myTheBulletCaster { get => _theBulletCaster; set => _theBulletCaster = value; }

    public event UnityAction _backToQue;
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
    }

    public override void RemoveTickFromManager()
    {
        base.RemoveTickFromManager();
    }


    public void SetInstance(UnityAction _onBulletTriggered, UnityAction _onBack)
    {
        _OnBulletTrigger += _OnBulletTrigger;
        _backToQue += _onBack;
       
    }
    public  void BulletShooting( DamageAble _targetUnit)
    {
        if(myTimerCounter!=null)
        {
            myTimerCounter.Kill();
        }

        myTargetUnit = _targetUnit;
        myTargetHash = myTargetUnit.myCollider2D.GetInstanceID();
        AddTickToManager();
        StartCountAutoEnque();
    }

    private void StartCountAutoEnque()
    {
        myTimerCounter = DOTween.Sequence();
        myTimerCounter.SetDelay(myBulletMaxLifeDur).OnComplete(OnEnqueue);
    }

    public  void OnEnqueue()
    {
        _backToQue?.Invoke();
        RemoveTickFromManager();
       
    }

    protected override void OnTriggerEnter2D(Collider2D coll)
    {
       if(coll.GetInstanceID()==myTargetHash)//다른타겟은 맞으면 안됨
        {
            _OnBulletTrigger?.Invoke();
             
            OnEnqueue();
        }
    }
}
