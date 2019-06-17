using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Stat", menuName = "StatDataBase")]
public class StatDataBase :ScriptableObject {

    [SerializeField]
    [TextArea]
    string _desc;
    public string m_Description { get => _desc; }

    [Header("BaseStats")]
    public float m_BaseHealth = 800; 
    public float m_BaseAttackSpeed =1;
    public float m_BaseDamage = 50;
    public float m_BaseSpellAmplify = 1;
    public float m_BaseArmor=0;
    public float m_BaseMagicArmor=0;
    public float m_BaseMovementSpeed = 10f;
    public float m_BaseAttackRange = 5f;
    //public float m_BaseAllyBlockRange = 5f;
    public int m_BaseManaReward = 100;

    public string GetStatString()
    {
        return 
            "공격력: " + m_BaseDamage + "\n\r" +
            "공격속도: " + m_BaseAttackSpeed + "\n\r"+
            "사거리: " + m_BaseAttackRange + "\n\r" +
            "주문력: " + m_BaseSpellAmplify + "\n\r" +
            "체력:"+m_BaseHealth+"\n\r"+
            "물리방어: " + m_BaseArmor + "\n\r" +
            "마법방어: " + m_BaseMagicArmor + "\n\r" +
            "이동속도: " + m_BaseMovementSpeed + "\n\r" +
            "마나보상: " + m_BaseManaReward + "\n\r"
            ;
    }

    [System.Serializable]
    public struct StatValue
    {
        public float m_HealthBonus;
        public float m_AttackSpeedBonus;
        public float m_DamageBonus;
        public float m_SpellAmplifyBonus;
        public float m_ArmorBonus;
        public float m_MagicArmorBonus;
        public int m_ManaRewardBonus;
        //
        public float m_MovementBonus;
        public float m_AttackRangeBonus;

        public string GetStatString()
        {
            return
                "공격력: " + m_DamageBonus + "\n\r" +
                "공격속도: " + m_AttackSpeedBonus + "\n\r" +
                "사거리: " + m_AttackRangeBonus + "\n\r" +
                "주문력: " + m_SpellAmplifyBonus + "\n\r" +
                "체력: " + m_HealthBonus + "\n\r" +
                "물리방어: " + m_ArmorBonus + "\n\r" +
                "마법방어: " + m_MagicArmorBonus + "\n\r" +
                "이동속도: " + m_MovementBonus + "\n\r" +
                "마나보상: " + m_ManaRewardBonus + "\n\r"
                ;
        }

        public  StatValue(StatDataBase myHeroStat)
        {
            m_HealthBonus = myHeroStat.m_BaseHealth;

            m_AttackSpeedBonus = myHeroStat.m_BaseAttackSpeed;
            m_DamageBonus = myHeroStat.m_BaseDamage;
            m_SpellAmplifyBonus = myHeroStat.m_BaseSpellAmplify;
            m_ArmorBonus = myHeroStat.m_BaseArmor;
            m_MagicArmorBonus = myHeroStat.m_BaseMagicArmor;
            m_ManaRewardBonus = myHeroStat.m_BaseManaReward;

            m_MovementBonus = myHeroStat.m_BaseMovementSpeed;
            m_AttackRangeBonus = myHeroStat.m_BaseAttackRange;
        }

     

        public void SetModifyStat(StatDataBase myHeroStat)
        {
            BonusPlusHealth(myHeroStat.m_BaseHealth);
            BonusPlusAttackSpeed(myHeroStat.m_BaseAttackSpeed);
            BonusPlusArmor(myHeroStat.m_BaseArmor);
            BonusPlusAttackRange(myHeroStat.m_BaseAttackRange);
            BonusPlusDamage(myHeroStat.m_BaseDamage);
            BonusPlusMagicArmor(myHeroStat.m_BaseMagicArmor);
            BonusPlusManaReward(myHeroStat.m_BaseManaReward);
            BonusPlusMoveSpeed(myHeroStat.m_BaseMovementSpeed);
            BonusPlusSpellAmplify(myHeroStat.m_BaseSpellAmplify);
        }
        public void SetModifyStat(StatValue myHeroStat)
        {
            BonusPlusHealth(myHeroStat.m_HealthBonus);
            BonusPlusAttackSpeed(myHeroStat.m_AttackSpeedBonus);
            BonusPlusArmor(myHeroStat.m_ArmorBonus);
            BonusPlusAttackRange(myHeroStat.m_AttackRangeBonus);
            BonusPlusDamage(myHeroStat.m_DamageBonus);
            BonusPlusMagicArmor(myHeroStat.m_MagicArmorBonus);
            BonusPlusManaReward(myHeroStat.m_ManaRewardBonus);
            BonusPlusMoveSpeed(myHeroStat.m_MovementBonus);
            BonusPlusSpellAmplify(myHeroStat.m_SpellAmplifyBonus);
        }
        public void SetUnModifyStat(StatDataBase myHeroStat)
        {
            BonusMinusHealth(myHeroStat.m_BaseHealth);
            BonusMinusAttackSpeed(myHeroStat.m_BaseAttackSpeed);
            BonusMinusArmor(myHeroStat.m_BaseArmor);
            BonusMinusAttackRange(myHeroStat.m_BaseAttackRange);
            BonusMinusDamage(myHeroStat.m_BaseDamage);
            BonusMinusMagicArmor(myHeroStat.m_BaseMagicArmor);
            BonusMinusManaReward(myHeroStat.m_BaseManaReward);
            BonusMinusMoveSpeed(myHeroStat.m_BaseMovementSpeed);
            BonusMinusSpellAmplify(myHeroStat.m_BaseSpellAmplify);
        }
        public void SetUnModifyStat(StatValue myHeroStat)
        {
            BonusMinusHealth(myHeroStat.m_HealthBonus);
            BonusMinusAttackSpeed(myHeroStat.m_AttackSpeedBonus);
            BonusMinusArmor(myHeroStat.m_ArmorBonus);
            BonusMinusAttackRange(myHeroStat.m_AttackRangeBonus);
            BonusMinusDamage(myHeroStat.m_DamageBonus);
            BonusMinusMagicArmor(myHeroStat.m_MagicArmorBonus);
            BonusMinusManaReward(myHeroStat.m_ManaRewardBonus);
            BonusMinusMoveSpeed(myHeroStat.m_MovementBonus);
            BonusMinusSpellAmplify(myHeroStat.m_SpellAmplifyBonus);
        }
        public void BonusPlusHealth(float v)
        {
            m_HealthBonus += v;
        }

        public void BonusMinusHealth(float v)
        {
            m_HealthBonus -= v;
        }

        public void BonusPlusAttackSpeed(float v)
        {
            m_AttackSpeedBonus += v;
        }

        public void BonusMinusAttackSpeed(float v)
        {
            m_AttackSpeedBonus -= v;
        }

        public void BonusPlusDamage(float v)
        {
            m_DamageBonus += v;
        }

        public void BonusMinusDamage(float v)
        {
            m_DamageBonus -= v;
        }

        public void BonusPlusSpellAmplify(float v)
        {
            m_SpellAmplifyBonus += v;
        }

        public void BonusMinusSpellAmplify(float v)
        {
            m_SpellAmplifyBonus -= v;
        }

        public void BonusPlusArmor(float v)
        {
            m_ArmorBonus += v;
        }

        public void BonusMinusArmor(float v)
        {
            m_ArmorBonus -= v;
        }

        public void BonusPlusMagicArmor(float v)
        {
            m_MagicArmorBonus += v;
        }

        public void BonusMinusMagicArmor(float v)
        {
            m_MagicArmorBonus -= v;
        }

        public void BonusPlusManaReward(int v)
        {
            m_ManaRewardBonus += v;
        }

        public void BonusMinusManaReward(int v)
        {
            m_ManaRewardBonus -= v;
        }

        public void BonusPlusAttackRange(float v)
        {
            m_AttackRangeBonus += v;
        }

        public void BonusMinusAttackRange(float v)
        {
            m_AttackRangeBonus -= v;
        }

        public void BonusPlusMoveSpeed(float v)
        {
            m_MovementBonus += v;
        }

        public void BonusMinusMoveSpeed(float v)
        {
            m_MovementBonus -= v;
        }
    }

}
