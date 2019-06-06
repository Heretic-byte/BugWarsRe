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
   
    public UnityAction<int> myOnCancel { get; set; }
   
    public UnityAction myOnRecall { get; set; }
    public UnityAction myOnRecallComplete { get; set; }
    public UnityAction<float> myOnStuned { get; set; }
    public UnityAction<float> myOnRespawnCountDown { get; set; }

    [SerializeField]
    private UnityEvent _OnRecallEvent;

    [SerializeField]
    private float _respawnTime = 10f;
     float myRespawnTime { get => _respawnTime; }

    public float myCurrentRespawnTime{get;private set;}

    private int _deathCount = 0;
    public int myDeathCount { get => _deathCount; set => _deathCount = value; }
    public bool _myIsHeroInLine { get;private set; }

   
    public UnityEvent myOnRecallEvent { get => _OnRecallEvent;  }
    Sequence _RecallSequence { get; set; }
    public int GetCurrentLaneNumber { get; private set; } = 0;
    public UnityAction myOnRespawn { get; set; }
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

        myCurrentRespawnTime = myRespawnTime;

        myOnRecall += SetCurrentLaneNumberNull;
    }
    public void SetCurrentLaneNumber(int battleFieldLane)
    {
        GetCurrentLaneNumber = battleFieldLane;
    }
    public void SetCurrentLaneNumberNull()
    {
        GetCurrentLaneNumber = 0;
    }
    public override void TakeKill()
    {
        base.TakeKill();

        KillMyTweenForDeath();
        myDeathCount++;

        GoBackToTemple(myDeathDelay).OnComplete(SetRespawnDelay);
    }
 
    public override void GoRushBattleField(Vector3 _pos)
    {
        base.GoRushBattleField(_pos);

        _myIsHeroInLine = true;
    }

    public Sequence GoBackToTemple(float _recallDelay)
    {
        myOnRecall.Invoke();
        myOnRecallEvent?.Invoke();
       
        RemoveBehavTick();

        _RecallSequence = DOTween.Sequence();
      return  _RecallSequence.SetDelay(_recallDelay)
            .AppendCallback(
            delegate
            {
                myOnRecallComplete?.Invoke();
                myTrans.position = myStartTemplePos;
                _myIsHeroInLine = false;
            });
    }


    
    public void SetRespawnDelay()
    {
        Sequence RespawnSeq = DOTween.Sequence();

        myCurrentRespawnTime = (myDeathCount * myRespawnTime);

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
        if(_myIsHeroInLine)
        {
            CancelPortal();
            myOnStuned?.Invoke(_dur);
        }
       
    }

    public void CancelPortal()
    {
        _RecallSequence?.Kill();
        myOnCancel?.Invoke(GetCurrentLaneNumber);
    }

    void KillMyTweenForDeath()
    {
        _RecallSequence?.Kill();
    }
}
