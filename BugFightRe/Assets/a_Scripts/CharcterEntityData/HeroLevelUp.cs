using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class HeroLevelUp : MonoBehaviour
{

    //레벨업최대치
    //레벨업시 오를 스텟
    //레벨업 시키기
    //혹은 낮추기
    //
    [Tooltip("base level is 1")]
    [SerializeField]
    int _MaxLevel;
    [SerializeField]
    UnityEvent _OnLevelUp;
    [SerializeField]
    float _GrowRate = 25f;
    UnitHero myHero { get; set; }
    public int myMaxLevel { get => _MaxLevel; }
    public int myCurrentLevel { get; private set; } = 1;
    public UnityEvent myOnLevelUp { get => _OnLevelUp;  }

    StatDataBase.StatValue myBonusStat;

    Vector3 _mybonusScale { get; set; }
     float myGrowRate { get => _GrowRate;  }

    void Start()
    {
        myHero = GetComponent<UnitHero>();
        StatSet(myHero.myStat);
    }

    void StatSet(StatDataBase myHeroStat)
    {
        myBonusStat.m_HealthBonus = myHeroStat.m_BaseHealth / myGrowRate;
        myBonusStat.m_AttackSpeedBonus = myHeroStat.m_BaseAttackSpeed/ myGrowRate;
        myBonusStat.m_DamageBonus = myHeroStat.m_BaseDamage / myGrowRate;
        myBonusStat.m_SpellAmplifyBonus= myGrowRate;
        myBonusStat.m_ArmorBonus= myHeroStat.m_BaseArmor / myGrowRate;
        myBonusStat.m_MagicArmorBonus= myHeroStat.m_BaseMagicArmor / myGrowRate;
        myBonusStat.m_ManaRewardBonus = myHeroStat.m_BaseManaReward / (int)myGrowRate;
        //
        _mybonusScale = myHero.myTrans.localScale / myGrowRate;
    }
    public void LevelUp()
    {
        if (myCurrentLevel >= myMaxLevel)
        {
            return;
        }

        myCurrentLevel++;
        myOnLevelUp?.Invoke();
        LevelUpStatPlus();
    }

    void LevelUpStatPlus()
    {
       
        //
        myHero.myRealStat.SetModifyStat(myBonusStat);
        myHero.myTrans.DOScale(myHero.myTrans.localScale + _mybonusScale,0.4f);
    }
}






