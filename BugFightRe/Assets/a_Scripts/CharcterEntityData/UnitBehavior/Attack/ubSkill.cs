using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ubSkill : myUnitBehavior
{
    [Header("MustBe 2")]
    [SerializeField]
    private SkillBase[] _mySkills;
    public SkillBase[] mySkills { get => _mySkills;  }

    public override void SetInstance()
    {


    }

    public override void AddTickToManager()
    {
       
    }

    public override void FixedTickFloat(float _tick)
    {
      
    }

    public override void RemoveTickFromManager()
    {

    }

    
}
