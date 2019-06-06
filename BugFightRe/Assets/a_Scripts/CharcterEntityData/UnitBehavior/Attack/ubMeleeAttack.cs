using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MyMarmot.Tools;
public abstract class ubMeleeAttack : ubAttackBase
{
    protected override void TryAttack()
    {
        rayOrigin = myTrans.position + myUnit.myRayCastOffset;
        targetHitten = Physics2D.Raycast(rayOrigin, _attackDir, myAttackRange, myUnit.myTargetLayers);


        if (targetHitten.collider != null)
        {
            myUnit.myAttackTarget = myManagerCollDic.GetDamageAble(targetHitten.collider.GetInstanceID());
        }
        else
        {
            myUnit.myAttackTarget = null;
            return;
        }

        if (_attackTimer > myAttackSpeed)
        {
            _attackTimer = 0f;

            myUnit.myOnAttack?.Invoke();
        }
    }
    public override void Attack()
    {

        if (myUnit.myAttackTarget == null)
        {
            return;
        }
        myUnit.myAttackTarget.TakePhysicalDamage(myUnit.myStat.m_BaseDamage, myUnit);
    }
}
