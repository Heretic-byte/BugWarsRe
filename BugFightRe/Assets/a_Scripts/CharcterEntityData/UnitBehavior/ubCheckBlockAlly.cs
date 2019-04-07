using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ubCheckBlockAlly : myUnitBehavior
{
    public float myBlockCheckStopRange { get; set; }
    Vector3 _RayDir = Vector3.right;
  
    ColliderDicSingletone myManagerCollDic;

    public override void SetInstance()
    {
        myUnit = GetComponent<Unit>();
        myTrans = myUnit.myTrans;
        SetRayCastDir();
        myBlockCheckStopRange = myUnit.myStat.m_BaseAllyBlockRange;
        myManagerCollDic = ColliderDicSingletone.myInstance;


        myUnit.OnDequeueAction += AddTickToManager;
        myUnit.OnEnqueueAction += RemoveTickFromManager;
        myUnit.myOnGetKillAction += SetNullTarget;
    }
    public void SetRayCastDir()
    {
      
        if (!myUnit.myIsFacingRight)
        {
            _RayDir = Vector3.left;
        }
        else
        {
            _RayDir = Vector3.right;
        }
    }

 
   
    public override void FixedTickFloat(float _tick)
    {


        myUnit.myCollider2D.enabled = false;
        var AllyHitten = Physics2D.Raycast(myTrans.position, _RayDir, myBlockCheckStopRange, myUnit.myAllyLayers);
        myUnit.myCollider2D.enabled = true;

        Debug.DrawRay(myTrans.position , _RayDir* myBlockCheckStopRange, Color.blue);

        if (AllyHitten.collider!=null)
        {
            myUnit.myBlockingAlly = myManagerCollDic.myColliderDamageAble[AllyHitten.collider.GetInstanceID()];
        }
        else
        {
            myUnit.myBlockingAlly = null;
        }

    }

    //프레임마다 계속 처넣고있음
    public override void AddTickToManager()
    {
        GameManager.myInstance.AddScaledTickToManager(FixedTickFloat);
    }

    public override void RemoveTickFromManager()
    {
        GameManager.myInstance.RemoveScaledTickFromManager(FixedTickFloat);
    }
    public void SetNullTarget()
    {
        myUnit.myBlockingAlly = null;
    }
    
}
