using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Nexus : Unit,IlinePosHolder
{
    [SerializeField]
    Vector3[] _SpawnPointArray
        = { new Vector3(0.145f, 0.0f, -0.3f)
    ,new Vector3(0.145f, 0.1f, -0.3f)
    ,new Vector3(0.145f, 0.2f, -0.3f)};

    public Vector3[] myPosArray { get => _SpawnPointArray; set => _SpawnPointArray = value; }
  
    //0.145//0.0 //-0.3
    //0.145//0.1 //-0.3
    //0.145//0.2 //-0.3
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

    public override void TakeKill()
    {
        base.TakeKill();
        myObj.SetActive(false);
    }

    public Vector3 GetSpawnPos()
    {
        return myTrans.position+myPosArray[_mySpawnCount++ % 3];
    }

    public Vector3 GetSpawnPos(int index)
    {
        return myTrans.position + myPosArray[index];
    }
}
