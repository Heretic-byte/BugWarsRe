using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
public class UnitHero : Unit, ICanBeStun
{
    [SerializeField]
    private Sprite _portrait;
    public Sprite myPortrait { get => _portrait; set => _portrait = value; }

    private Vector3 _startTemplePos;
    public Vector3 myStartTemplePos { get => _startTemplePos; }

    public UnityAction myOnRushBattleField { get; set; }
    public UnityAction myOnStuned { get; set; }
    public UnityAction myOnRespawn { get; set; }
    public UnityAction myOnRecall { get; set; }

  
    [SerializeField]
    private float _respawnTime = 15f;
    public float myRespawnTime { get => _respawnTime; }

    private float myCurrentRespawnTime{get;set;}

    private int _deathCount = 0;
    public int myDeathCount { get => _deathCount; set => _deathCount = value; }
    public bool _myIsHeroInLine { get;private set; }


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
        return myStat.m_BaseSpellAmply;
    }
    #endregion
    private void Awake()
    {
        MainSetInstance();
        SetHpToMax();
        _startTemplePos = myTrans.position;
        _myIsHeroInLine = false;
    }
    public override void GetKill()
    {
        base.GetKill();
        myOnEnqueueAction?.Invoke();

        myDeathCount++;

        
        GoBackToTemple(myDeathDelay).OnComplete(SetRespawnDelay);
    }
 
    public void GoRushBattleField(Vector3 _pos)
    {
        Debug.Log("RUSH2");
        myTrans.position = _pos;
        _myIsHeroInLine = true;
        myOnRushBattleField?.Invoke();
        foreach(var behav in myUnitBehaviors)
        {
            behav.AddTickToManager();
        }
   
    }
    public Sequence GoBackToTemple(float _recallDelay)
    {
        myOnRecall.Invoke();

        foreach (var behav in myUnitBehaviors)
        {
            behav.RemoveTickFromManager();
        }

        Sequence sequence = DOTween.Sequence();
      return  sequence.SetDelay(_recallDelay)
            .AppendCallback(
            delegate
            {
                myTrans.position = myStartTemplePos;
                _myIsHeroInLine = false;
              
            });
    }

    public void SetRespawnDelay()
    {
        Sequence RespawnSeq = DOTween.Sequence();

        myCurrentRespawnTime = (myDeathCount * myRespawnTime) * 0.8f;
     
         RespawnSeq.SetDelay(myCurrentRespawnTime).AppendCallback(Respawn);
    }
    private void Respawn()
    {
        _isDead = false;
        myCollider2D.enabled = true;
        SetHpToMax();
       
        myOnRespawn?.Invoke();
    }

    public void GetStunDur(float _dur)
    {
        myOnStuned?.Invoke();


    }
    
}
