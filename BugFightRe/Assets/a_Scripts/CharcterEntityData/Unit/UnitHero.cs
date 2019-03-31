using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
public class UnitHero : Unit
{
    [SerializeField]
    private Sprite _portrait;
    public Sprite myPortrait { get => _portrait; set => _portrait = value; }

    
     UnityAction _onRushBattleField;
    public UnityAction myOnRushBattleField { get => _onRushBattleField; set => _onRushBattleField = value; }


    public override float GetArmor()
    {
        return myStat.m_BaseArmor;
    }
    public override float GetAttackDamage()
    {
        return myStat.m_BaseDamage;
    }
    public override float GetAttackSpeed()
    {
        return myStat.m_BaseAttackSpeed;
    }
    public override float GetHealth()
    {
        return myStat.m_BaseHealth;
    }
    public override float GetSpellArmorPercent()
    {
        return myStat.m_BaseMagicArmor;
    }
    public override float GetSpellDamagePercent()
    {
        return myStat.m_BaseSpellAmply;
    }

    private void Awake()
    {
        MainSetInstance();
        SetHpToMax();
    }
    public override void GetKill()
    {
        base.GetKill();
        OnEnqueueAction?.Invoke();
      
    }
 
    public void GoRushBattleField(Vector3 _pos)
    {
        myTrans.position = _pos;

        myOnRushBattleField?.Invoke();
        foreach(var behav in myUnitBehaviors)
        {
            behav.AddTickToManager();
        }
   
    }
    public void GoBackToTemple()
    {
        foreach (var behav in myUnitBehaviors)
        {
            behav.RemoveTickFromManager();
        }
    }
    
}
