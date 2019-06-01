using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ubHeroHealthBar : ubHealthBar
{
    UnitHero _myUnitHero { get; set; }

    public override void AddTickToManager()
    {
        //base.AddTickToManager();
    }

    public override void FixedTickFloat(float _tick)
    {
       
    }

    public override void RemoveTickFromManager()
    {
       // base.RemoveTickFromManager();
    }

    protected override void Awake()
    {
        base.Awake();
        _myUnitHero = GetComponent<UnitHero>();
        _myUnitHero.myOnRespawn += ShowHealthBar;
    }


}
