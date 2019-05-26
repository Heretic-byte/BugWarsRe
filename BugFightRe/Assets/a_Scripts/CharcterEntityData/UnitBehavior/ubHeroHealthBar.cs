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
        base.FixedTickFloat(_tick);
    }

    public override void RemoveTickFromManager()
    {
       // base.RemoveTickFromManager();
    }

    public override void SetInstance()
    {
        base.SetInstance();
        _myUnitHero = myUnit as UnitHero;
        _myUnitHero.myOnRespawn += ShowHealthBar;
    }
}
