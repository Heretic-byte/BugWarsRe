using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunglePortal : MonoBehaviour,IlinePosHolder
{
   // [Tooltip("SetThisForOpponent")]
    [SerializeField]
    private LayerMask _WhatToHit;

    public LayerMask myWhatToHit { get => _WhatToHit;}
    
    [SerializeField]
    private float _Radius;
    public float myRadius { get => _Radius;}

    ColliderDicSingletone myManagerCollDic { get; set; }

    [SerializeField]
    Vector3[] _SpawnPointArray
         = { new Vector3(0.145f, 0.0f, -0.3f)
    ,new Vector3(0.145f, 0.1f, -0.3f)
    ,new Vector3(0.145f, 0.2f, -0.3f)};

    public Vector3[] myPosArray { get => _SpawnPointArray; set => _SpawnPointArray = value; }

    private void Start()
    {
        GameManager.myInstance.AddScaledTickToManager(MyTick);
        myManagerCollDic = ColliderDicSingletone.myInstance;
    }

    void MyTick(float tick)
    {
        CircleCastForHero();
    }

    void CircleCastForHero()
    {
        var hitten= Physics2D.OverlapCircleAll(transform.position, myRadius, myWhatToHit);

        if(hitten.Length>0)
        {
           for(int i=0; i< hitten.Length;i++)
            {
                hitten[i].GetComponent<UnitHero>().GoBackToTemple(0);
            }
        }
    }

    public Vector3 GetSpawnPos()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetSpawnPos(int index)
    {
        return transform.position + myPosArray[index];
    }
}
