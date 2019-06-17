using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class UnitCreep : Unit
{
  
   public GameManager myManagerGame { get ; set ; }
    public UnityAction<GameObject> OnEnqueActionObj { get; set; }
  
    private void Awake()
    {
        myRealStat = new StatDataBase.StatValue(myStat);
        
        MainSetInstance();
        
        SetDir();

    }
    protected override void MainSetInstance()
    {
        base.MainSetInstance();
        myManagerGame = GameManager.GetInstance;

       


    }
    #region GetStat
    public override float GetArmor()
    {
        return myRealStat.m_ArmorBonus;
    }
    public override float GetAttackDamage()
    {
        return myRealStat.m_DamageBonus;
    }
    public override float GetAttackSpeed()
    {
        return myRealStat.m_AttackSpeedBonus;
    }
    public override float GetMaxHealth()
    {
        return myRealStat.m_HealthBonus;
    }
    public override float GetSpellArmorPercent()
    {
        return myRealStat.m_MagicArmorBonus;
    }
    public override float GetSpellDamagePercent()
    {
        return myRealStat.m_SpellAmplifyBonus;
    }
    #endregion
    public override void TakeKill()
    {
        base.TakeKill();

        Sequence DieDelay = DOTween.Sequence();
        
        DieDelay.SetDelay(myDeathDelay).OnComplete(OnEnqueue);
    }
    public override void GoRushBattleField(Vector3 _pos)
    {
        base.GoRushBattleField(_pos);

        myObj.SetActive(true);
        myIsDead = false;
        SetHpToMax();
        myUnitStatement = UnitStatement.Walk;
        myCollider2D.enabled = true;
    }
  
    public void OnEnqueue()
    {
        RemoveBehavTick();
        myObj.SetActive(false);
        OnEnqueActionObj?.Invoke(myObj);
    }
}
