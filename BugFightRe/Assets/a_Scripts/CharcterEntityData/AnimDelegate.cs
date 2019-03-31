using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDelegate : myUnitBehavior
{
    Animator _anim;

    public Animator myAnim { get => _anim; set => _anim = value; }

  

    public override void FixedTickFloat(float _tick)
    {
       
    }

    public override void AddTickToManager()
    {
        GameManager.myInstance.AddOnlyScaleTickToManager(SetAnimSpeedScale);
    }

    public override void RemoveTickFromManager()
    {
        GameManager.myInstance.RemoveOnlyScaleTickFromManager(SetAnimSpeedScale);
        
    }

    public override void SetInstance()
    {
        myUnit = GetComponent<Unit>();
        myTrans = myUnit.myTrans;
        myAnim = GetComponentInChildren<Animator>();


        SetDelegateToMainUnitClass();

        AddTickToManager();
    }
    void SetDelegateToMainUnitClass()
    {
       
        myUnit.myOnAttack += SetTriggerOnAttack;
        myUnit.myOnDamageAction += SetTriggerOnDamage;
        myUnit.myOnKillAction += SetTriggerOnDead;
        myUnit.myOnKillAction += ResetTriggerAll;
        myUnit.myOnWalking += SetBoolOnStartWalk;
        myUnit.myOnNotWalking += SetBoolOnStopWalk;

      
    }

    public void SetBoolOnStartWalk()
    {
        myAnim.SetBool("Walk", true);
    }
    public void SetBoolOnStopWalk()
    {
        myAnim.SetBool("Walk", false);
    }
    public void SetTriggerOnDead()
    {
        myAnim.SetTrigger("Dead");
    }
    public void SetTriggerOnAttack()
    {
        myAnim.SetTrigger("Attack1");
    }
    public void SetTriggerOnDamage()
    {
        myAnim.SetTrigger("OnDamage");
    }
    public void ResetTriggerAll()
    {
        myAnim.ResetTrigger("Attack1");
        myAnim.ResetTrigger("OnDamage");
    }
    public void SetAnimSpeedScale(float _v)
    {
        myAnim.speed = _v;
    }
}
