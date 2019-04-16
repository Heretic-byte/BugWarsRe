using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class HealthBar : myUnitBehavior
{
    [SerializeField]
    private Image _ForwardBar;
    public Image myForwardBar { get => _ForwardBar; set => _ForwardBar = value; }

    Tween _BarUpdateTween;
    public Tween myBarUpdateTween { get => _BarUpdateTween; set => _BarUpdateTween = value; }

    Vector3 _FollowOffset;
    public Vector3 myFollowOffset { get => _FollowOffset; set => _FollowOffset = value; }


    public override void SetInstance()
    {
        myTrans = transform;
        

        myUnit = GetComponentInParent<Unit>();
        SetDir();
        SetBarFillAmountMax();
        myUnit.OnDequeueAction += AddTickToManager;
        myUnit.OnDequeueAction += SetBarFillAmount;
        myUnit.OnEnqueueAction += RemoveTickFromManager;
        myUnit.myOnDamageAction += BarUpdate;

        myFollowOffset = this.myTrans.position - myUnit.myTrans.position;

    }
    void SetDir()
    {
        if (!myUnit.myIsFacingRight)
        {
           var ScaleTemp= myTrans.localScale;
            ScaleTemp.x *= -1;
            myTrans.localScale = ScaleTemp;

            myForwardBar.fillOrigin = 1;
        }
       
    }
    void SetBarFillAmountMax()
    {
        myForwardBar.fillAmount = 1f;
    }
    void SetBarFillAmount()
    {
        myForwardBar.fillAmount = myUnit.GetCurrentHealth/ myUnit.GetMaxHealth();
    }
    void BarUpdate()
    { 
        myBarUpdateTween= myForwardBar.DOFillAmount(myUnit.GetCurrentHealth / myUnit.GetMaxHealth(), 0.15f);
    }
 
    public override void AddTickToManager()
    {
        GameManager.myInstance.AddUnScaledTickToManager(FixedTickFloat);
    }
    public override void RemoveTickFromManager()
    {
        GameManager.myInstance.RemoveUnScaledTickFromManager(FixedTickFloat);
    }
    public override void FixedTickFloat(float _tick)
    {

        FollowMyUnit(_tick);
    }

    private void FollowMyUnit(float tick)
    {

        myTrans.position =myUnit.myTrans.position+ myFollowOffset;
    }
}
