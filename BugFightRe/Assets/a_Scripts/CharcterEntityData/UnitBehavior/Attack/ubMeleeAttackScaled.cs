using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ubMeleeAttackScaled : ubMeleeAttack
{

    public override void AddTickToManager()
    {
        GameManager.GetInstance.AddScaledTickToManager(FixedTickFloat);
    }

    public override void RemoveTickFromManager()
    {
        GameManager.GetInstance.RemoveScaledTickFromManager(FixedTickFloat);
    }


}
