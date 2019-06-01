using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Serializer :MonoBehaviour
{
    public Transform transs;
    public ubRangeAttack target;

    [ContextMenu("SavePosition")]
    public void Test()
    {
       target.myBulletShootingPos = transs.localPosition;
    }


     Vector3 SetPos(Transform _targetTrans)
    {
        return _targetTrans.position;
    }
}   
