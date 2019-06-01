using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ubMeleeAttackUnScaled : ubMeleeAttack
{

    public override void AddTickToManager()
    {
        GameManager.GetInstance.AddUnScaledTickToManager(FixedTickFloat);
    }

    public override void RemoveTickFromManager()
    {
        GameManager.GetInstance.RemoveUnScaledTickFromManager(FixedTickFloat);
    }


}