using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Serializer :MonoBehaviour
{
    public Transform[] transs;
    public Nexus target;

    [ContextMenu("SavePosition")]
    public void Test()
    {

        for(int i=0; i < target.mySpawnPointArray.Length;i++)
        {
            target.mySpawnPointArray[i] = transs[i].localPosition;
        }
    }


     Vector3 SetPos(Transform _targetTrans)
    {
        return _targetTrans.position;
    }
}   
