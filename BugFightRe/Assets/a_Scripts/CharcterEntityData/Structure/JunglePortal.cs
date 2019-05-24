using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunglePortal : MonoBehaviour
{
    [Tooltip("SetThisForOpponent")]
    [SerializeField]
    private LayerMask _WhatToHit;

    public LayerMask myWhatToHit { get => _WhatToHit;}
    
    [SerializeField]
    private float _Radius;
    public float myRadius { get => _Radius;}

    ColliderDicSingletone myManagerCollDic { get; set; }

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



}
