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
    public UnityAction<float> myOnRespawnCountDown { get; set; }
    [SerializeField]
    private UnityEvent _OnRecallEvent;

    [SerializeField]
    private float _respawnTime = 15f;
     float myRespawnTime { get => _respawnTime; }

    public float myCurrentRespawnTime{get;private set;}

    private int _deathCount = 0;
    public int myDeathCount { get => _deathCount; set => _deathCount = value; }
    public bool _myIsHeroInLine { get;private set; }

    public StatDataBase.StatValue myRealStat { get; private set; }=new StatDataBase.StatValue();
    public UnityEvent myOnRecallEvent { get => _OnRecallEvent;  }

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
    private void Awake()
    {
        MainSetInstance();
        
        myRealStat.SetStat(myStat);
        
        SetHpToMax();
        _startTemplePos = myTrans.position;
        _myIsHeroInLine = false;

        myCurrentRespawnTime = _respawnTime;
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
        myOnRecallEvent?.Invoke();

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

        myCurrentRespawnTime = ((myDeathCount*0.8f) * myRespawnTime);

        RespawnSeq.PrependInterval(myCurrentRespawnTime)
           .PrependCallback(delegate { myOnRespawnCountDown.Invoke(myCurrentRespawnTime); })
           .onComplete += Respawn;
    }
    public void Respawn()
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
