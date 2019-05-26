using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleRune : MonoBehaviour
{
    [SerializeField]
    private LayerMask _WhatToHit;

    public LayerMask myWhatToHit { get => _WhatToHit; }

    [SerializeField]
    private float _Radius;
    public float myRadius { get => _Radius; }

    void OnEnable()
    {
        GameManager.myInstance.AddScaledTickToManager(MyTick);
    }

    void OnDisable()
    {
        GameManager.myInstance.RemoveScaledTickFromManager(MyTick);
    }

    void MyTick(float tick)
    {
        CircleCastForHero();
    }

    void CircleCastForHero()
    {
        var hitten = Physics2D.OverlapCircle(transform.position, myRadius, myWhatToHit);

        if (hitten != null)
        {
            hitten.GetComponent<HeroLevelUp>().LevelUp();
            Debug.Log("LevelUp");
            gameObject.SetActive(false);
        }
    }

   
}
