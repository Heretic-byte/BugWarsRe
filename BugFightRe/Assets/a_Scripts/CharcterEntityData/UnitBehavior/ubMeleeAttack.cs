
using UnityEngine;
using UnityEngine.Events;

public class ubMeleeAttack : myUnitBehavior
{


    public float myAttackSpeed { get; set; }
    public float myAttackRange { get; set; }

   

    Vector3 _attackDir = Vector3.right;
    ColliderDicSingletone myManagerCollDic;
    float _attackTimer = 0f;

    RaycastHit2D targetHitten;
    Vector3 rayOrigin;
    public override void SetInstance()
    {
        myUnit = GetComponent<Unit>();
        myTrans = myUnit.myTrans;
        SetTranslateDir();

        myAttackSpeed = myUnit.myStat.m_BaseAttackSpeed;
        myAttackRange = myUnit.myStat.m_BaseAttackRange;

        myManagerCollDic = ColliderDicSingletone.myInstance;


        myUnit.OnDequeueAction += AddTickToManager;
        myUnit.OnEnqueueAction += RemoveTickFromManager;
        myUnit.OnEnqueueAction += SetNullTarget;

     
    }
    public void SetTranslateDir()
    {
        if (!myUnit.myIsFacingRight)
        {
            _attackDir = Vector3.left;
        }
        else
        {
            _attackDir = Vector3.right;
        }
    }

    public override void FixedTickFloat(float _tick)
    {
        _attackTimer += _tick;

        TryAttack();
    }
    public void TryAttack()
    {
        rayOrigin = myTrans.position + myUnit.myRayCastOffset;
        targetHitten = Physics2D.Raycast(rayOrigin, _attackDir, myAttackRange, myUnit.myTargetLayers);

       
        if (targetHitten.collider != null)
        {

            myUnit.myAttackTarget = myManagerCollDic.myColliderDamageAble[targetHitten.collider.GetInstanceID()];
            myUnit.myAttackTarget.myOnKillFromAttacker += SetNullTargetFromTarget;

        }
        else
        {
            myUnit.myAttackTarget = null;
            return;
        }


        if (_attackTimer>myAttackSpeed)
        {
           
          
            myUnit.myOnAttack?.Invoke();
            _attackTimer = 0f;
            myUnit.myAttackTarget.GetPhysicalDamage(myUnit.myStat.m_BaseDamage, myUnit);
        }
     
    }
    //프레임마다 계속 처넣고있음
    //묹제있음

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
        myUnit.myAttackTarget = null;
    }
    public void SetNullTargetFromTarget(DamageAble _mySelfForPointer)
    {
        myUnit.myAttackTarget = null;
        _mySelfForPointer.myOnKillFromAttacker -= SetNullTargetFromTarget;
       
       
    }
}
