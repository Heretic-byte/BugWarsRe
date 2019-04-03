﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ubRangeAttackScaled : ubRangeAttack
{
    public override void AddTickToManager()
    {
        GameManager.myInstance.AddScaledTickToManager(FixedTickFloat);
    }

    public override void RemoveTickFromManager()
    {
        GameManager.myInstance.RemoveScaledTickFromManager(FixedTickFloat);
    }
}
