using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ubMeleeAttack : ubAttackBase
{
    protected override void TryAttack()
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


        if (_attackTimer > myAttackSpeed)
        {


            myUnit.myOnAttack?.Invoke();
            _attackTimer = 0f;
            myUnit.myAttackTarget.GetPhysicalDamage(myUnit.myStat.m_BaseDamage, myUnit);
        }
    }
}
