using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit
{
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
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Awake()
    {
        MainSetInstance();
        SetHpToMax();

        AddBehavTick();
    }

    public override void TakeKill()
    {
        base.TakeKill();
        RemoveBehavTick();
        myObj.SetActive(false);
    }


}
