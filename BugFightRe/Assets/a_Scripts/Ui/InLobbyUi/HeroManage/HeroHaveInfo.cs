using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;
using HeroSelectUI;

public class HeroHaveInfo : MonoBehaviour
{
    [Header("영웅 info")]
    [SerializeField]
    HeroInfo _heroInfo;
    [SerializeField]
    StarShow _attackStarShow;
    [SerializeField]
    StarShow _defenseStarShow;
    [SerializeField]
    StarShow _speedStarShow;

    public HeroInfo m_HeroInfo { get => _heroInfo; }
    StarShow m_AttackStarShow { get => _attackStarShow; }
    StarShow m_DefenseStarShow { get => _defenseStarShow; }
    StarShow m_SpeedStarShow { get => _speedStarShow; }

    const float StarDamageRatio = 50f;
    const float StarAttackSpeedRatio = 0.6f;
    const float StarAttackRange = 0.75f;
    const float StarArmorRatio = 7f;
    const float StarMagicArmorRatio = 15f;
    const float StarHealthRatio = 750f;
    const float StarMoveSpeedRatio = 0.15f;

    HeroManager m_HeroManage { get; set; }

    public void OnStart()
    {
        m_HeroManage = HeroManager.GetInstance;
        m_HeroInfo.OnEquipCallback(SetStar);
        m_HeroInfo.OnUnEquipCallback(delegate { SetNullStar(); });
        m_HeroInfo.OnUnEquipCallback(SetStar);
    }

    public void SetHeroHaveInfoPanel(HeroData heroData)
    {
        var HeroStatValue = m_HeroManage.GetHeroStatData(heroData.m_HeroIndex);
        StatDataBase.StatValue HeroFinalStat = new StatDataBase.StatValue(HeroStatValue);//this is for just Ui
        m_HeroInfo.SetHeroData(HeroFinalStat,heroData);
    }

    public void SetNullHaveInfo()
    {
        m_HeroInfo.SetNullHeroName();
        m_HeroInfo.SetNullItemData();
        SetNullStar();
    }

    void SetNullStar()
    {
        m_AttackStarShow.SetEmptyStarSequence(5);
        m_DefenseStarShow.SetEmptyStarSequence(5);
        m_SpeedStarShow.SetEmptyStarSequence(5);
    }

    void SetStar(StatDataBase.StatValue HeroStatValue)
    {
        m_AttackStarShow.SetStarSequence(SetAttackStar(HeroStatValue));
        m_DefenseStarShow.SetStarSequence(SetDefenseStar(HeroStatValue));
        m_SpeedStarShow.SetStarSequence(SetSpeedStar(HeroStatValue));
        //늘어나는것만됨
    }

    //데미지 50당 별1개
    //공속 0.6당 별1개
    int SetAttackStar(StatDataBase.StatValue statValue)
    {
        float StarPercent1 = statValue.m_DamageBonus / StarDamageRatio;
        float StarPercent2 = statValue.m_AttackSpeedBonus / StarAttackSpeedRatio;
        float StarPercent3 = statValue.m_AttackRangeBonus / StarAttackRange;
       
        return (int)((StarPercent1 + StarPercent2 + StarPercent3) / 3f);
    }

    //7당 별1개
    int SetDefenseStar(StatDataBase.StatValue statValue)
    {
        float StarPercent1 = statValue.m_DamageBonus / StarArmorRatio;
        float StarPercent2 = statValue.m_AttackSpeedBonus / StarMagicArmorRatio;
        float StarPercent3 = statValue.m_AttackSpeedBonus / StarHealthRatio;

        return (int)((StarPercent1 + StarPercent2+ StarPercent3) / 3f);
    }

    //
    int SetSpeedStar(StatDataBase.StatValue statValue)
    {
        float StarPercent1 = statValue.m_MovementBonus / StarMoveSpeedRatio;

        return (int)(StarPercent1 );
    }
    //이것과별개로 인벤토리 띄워줘야함

    //아이템으로 인한 보너스 스텟은 어떻게 보여줄것인가
    //별한개당 몇의 수치를 의미하는가
}
