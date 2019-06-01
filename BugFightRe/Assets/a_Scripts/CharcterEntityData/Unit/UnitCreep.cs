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
    public override float GetMaxHealth()
    {
        return myStat.m_BaseHealth;
    }
    public override float GetSpellArmorPercent()
    {
        return myStat.m_BaseMagicArmor;
    }
    public override float GetSpellDamagePercent()
    {
        return myStat.m_BaseSpellAmplify;
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
