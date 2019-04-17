using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class ubHealthBar : myUnitBehavior
{
    [SerializeField]
    private Image _ForwardBar;
    public Image myForwardBar { get => _ForwardBar; set => _ForwardBar = value; }

    Tween _BarUpdateTween;
    public Tween myBarUpdateTween { get => _BarUpdateTween; set => _BarUpdateTween = value; }

    Vector3 _FollowOffset;
    public Vector3 myFollowOffset { get => _FollowOffset; set => _FollowOffset = value; }

    GameObject _myObj { get; set; }
    public override void SetInstance()
    {
        myTrans = transform;
        _myObj = gameObject;

         myUnit = GetComponentInParent<Unit>();
        SetDir();
        SetBarFillAmountMax();
        myUnit.myOnDequeueAction += AddTickToManager;
        myUnit.myOnDequeueAction += SetBarFillAmount;
        myUnit.myOnEnqueueAction += RemoveTickFromManager;
        myUnit.myOnDamageAction += BarUpdate;
        myUnit.myOnHealAction += BarUpdate;
        myUnit.myOnEnqueueAction += HideHealthBar;
        myUnit.myOnDequeueAction += ShowHealthBar;

        myFollowOffset = this.myTrans.position - myUnit.myTrans.position;

    }
    protected void SetDir()
    {
        if (!myUnit.myIsFacingRight)
        {
           var ScaleTemp= myTrans.localScale;
            ScaleTemp.x *= -1;
            myTrans.localScale = ScaleTemp;

            myForwardBar.fillOrigin = 1;
        }
       
    }
    protected void SetBarFillAmountMax()
    {
        myForwardBar.fillAmount = 1f;
    }
    protected void SetBarFillAmount()
    {
        myForwardBar.fillAmount = myUnit.GetCurrentHealth/ myUnit.GetMaxHealth();
    }
    protected void BarUpdate()
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

    protected void FollowMyUnit(float tick)
    {

        myTrans.position =myUnit.myTrans.position+ myFollowOffset;
    }
    protected void HideHealthBar()
    {
        _myObj.SetActive(false);
    }
  protected  void ShowHealthBar()
    {
        _myObj.SetActive(true);
    }
}
