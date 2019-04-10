using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Nexus : Unit
{
    [SerializeField]
    Vector3[] _SpawnPointArray;

    public Vector3[] mySpawnPointArray { get => _SpawnPointArray; set => _SpawnPointArray = value; }

    private int _mySpawnCount = 0;

    public override float GetArmor()
    {
        return myStat.m_BaseArmor;
        
    }

    public override float GetAttackDamage()
    {
        throw new System.NotImplementedException();
    }

    public override float GetAttackSpeed()
    {
        throw new System.NotImplementedException();
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

    private void Awake()
    {
       
        MainSetInstance();
       
        SetHpToMax();
    }


    public override void GetKill()
    {
        base.GetKill();
        myObj.SetActive(false);
    }
    public Vector3 GetSpawnPos()
    {


        return myTrans.position+mySpawnPointArray[_mySpawnCount++ % 3];
    }
}
