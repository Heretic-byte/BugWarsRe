using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SkillBase : ScriptableObject
{
    public string m_SkillName;
    [TextArea]
    public string m_SkillDesc;
    public int m_ManaCost;
    public int m_CoolDown;
    public Sprite m_SkillIcon;
    public abstract void CreateSkillButton(UnitHero unitHero, GameObject targetObj);
}
