using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkillUse : MonoBehaviour
{
    [SerializeField]
    SkillBase _Skill;
    public SkillBase m_Skill { get => _Skill; }
    UnitHero m_UnitHero { get; set; }

    public void CreateSkillUseButton(UnitHero skillUseHero,GameObject targetObj)
    {
        m_UnitHero = skillUseHero;
      
        m_Skill.CreateSkillButton(skillUseHero,targetObj);
    }
}
