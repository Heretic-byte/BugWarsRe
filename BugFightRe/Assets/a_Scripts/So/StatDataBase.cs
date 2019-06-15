using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Stat", menuName = "StatDataBase")]
public class StatDataBase :ScriptableObject {

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
