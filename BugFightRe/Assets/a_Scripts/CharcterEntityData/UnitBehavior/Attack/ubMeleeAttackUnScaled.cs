using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ubMeleeAttackUnScaled : ubMeleeAttack
{

    public override void AddTickToManager()
    {
        GameManager.myInstance.AddUnScaledTickToManager(FixedTickFloat);
    }

    public override void RemoveTickFromManager()
    {
        GameManager.myInstance.RemoveUnScaledTickFromManager(FixedTickFloat);
    }


}