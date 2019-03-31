using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ubGiveManaOnDeath : myUnitBehavior
{
    [SerializeField]
    private float _manaGiveRange;
    [SerializeField]
    private LayerMask _enemyHeroLayer;
   

    public float myManaGiveRange { get => _manaGiveRange; set => _manaGiveRange = value; }
    public LayerMask myEnemyHeroLayer { get => _enemyHeroLayer; set => _enemyHeroLayer = value; }
   
   event DeleGiveMana GiveManaDele;
    Vector3 rayOrigin;

    public override void AddTickToManager()
    {
       
    }

    public override void FixedTickFloat(float _tick)
    {
       
    }

    public override void RemoveTickFromManager()
    {
       
    }

    public override void SetInstance()
    {
        myUnit = GetComponent<Unit>();
        myTrans = myUnit.myTrans;
        myUnit.myOnKillAction += GiveManaIfHeroHere;
       
    }

 
    void GiveManaIfHeroHere()
    {
      var heroHittenAry =  CastRayAllSide();

        foreach (var hitten in heroHittenAry)
        {
            if (hitten.collider != null)
            {
                AddMana();
                break;
            }
        }


    }
    RaycastHit2D[] CastRayAllSide()
    {
        rayOrigin = myTrans.position + myUnit.myRayCastOffset;
        RaycastHit2D[] heroHitten = new RaycastHit2D[2];
        heroHitten[0] = Physics2D.Raycast(rayOrigin, Vector2.right, myManaGiveRange, myEnemyHeroLayer);
        heroHitten[1] = Physics2D.Raycast(rayOrigin, Vector2.left, myManaGiveRange, myEnemyHeroLayer);
        return heroHitten;
    }
    void AddMana()
    {
        ManaManager.myInstance.myPlayerAndEnemyManaDeleDic[myEnemyHeroLayer]?.Invoke(myUnit.myStat.m_BaseManaReward);
    }
  
}
