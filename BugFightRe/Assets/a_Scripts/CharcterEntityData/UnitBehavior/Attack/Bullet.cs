using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class Bullet : Projectile
{
    public Transform myTargetTrans { get; private set; }
    public float myBulletDamage { get => _BulletDamage; set => _BulletDamage = value; }
    public string myTargetTag { get => _TargetTag; set => _TargetTag = value; }
    public DamageAble myTheBulletCaster { get => _theBulletCaster; set => _theBulletCaster = value; }

    float _BulletDamage;
    string _TargetTag;
    DamageAble _theBulletCaster;

    public UnityAction _backToQue;

    public override void AddTickToManager()
    {
        base.AddTickToManager();
    }

    public override void FixedTickFloat(float _tick)
    {

        myTrans.position = Vector3.MoveTowards(myTrans.position, myTargetTrans.position, myMoveSpeed * _tick);
    }

    public override void RemoveTickFromManager()
    {
        base.RemoveTickFromManager();
    }


    public void SetInstance(string _tag, UnityAction _onBack)
    {
        myTargetTag = _tag;
        _backToQue += _onBack;
        _backToQue += OnEnqueue;
    }
    public  void OnDequeue(DamageAble _myTheBulletCaster, Transform _targetTrans,float _damage)
    {
        myTheBulletCaster = _myTheBulletCaster;
        myBulletDamage = _damage;
        myTargetTrans = _targetTrans;
        AddTickToManager();

        //n초후에 총알 날아간거 돌아오게
        //이후 다른풀링도 배열로 바꿔볼것
    }
    public  void OnEnqueue()
    {
        RemoveTickFromManager();
    }

    protected override void OnTriggerEnter2D(Collider2D coll)
    {
       if(coll.CompareTag(myTargetTag))
        {
            DealDamageToTarget(coll);
            _backToQue?.Invoke();
        }
    }

   

    protected void DealDamageToTarget(Collider2D _coll)
    {
        ColliderDicSingletone.myInstance.myColliderDamageAble[_coll.GetInstanceID()].GetPhysicalDamage(myBulletDamage, myTheBulletCaster);
    }
    

}
