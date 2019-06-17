using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkillUse : MonoBehaviour
{
    [SerializeField]
    SkillBase[] _Skill;
    public SkillBase[] m_Skill { get => _Skill; }
    UnitHero m_UnitHero { get; set; }

    public void CreateSkillUseButton(UnitHero skillUseHero,Transform[] targetObj)
    {
        m_UnitHero = skillUseHero;
      
        for(int i=0; i< m_Skill.Length;i++)
        {
        m_Skill[i].CreateSkillButton(skillUseHero, targetObj[i].gameObject);
        }
    }
}
