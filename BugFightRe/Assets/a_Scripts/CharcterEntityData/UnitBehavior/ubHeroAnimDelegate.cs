using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ubHeroAnimDelegate : ubAnimDelegate
{
    UnitHero _myUnitHero { get; set; }

    protected override void SetDelegateToMainUnitClass()
    {
        base.SetDelegateToMainUnitClass();
        _myUnitHero = myUnit as UnitHero;
        _myUnitHero.myOnRespawn += ResetTriggerAll;
        _myUnitHero.myOnRespawn += SetTriggerOnIdle;
        _myUnitHero.myOnRecall += SetBoolOnStopWalk;
    }
}
