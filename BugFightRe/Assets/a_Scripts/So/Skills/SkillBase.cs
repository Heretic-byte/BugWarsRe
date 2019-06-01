using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SkillBase : ScriptableObject
{
    public int m_ManaCost;
    public int m_CoolDown;
  
    public abstract void CreateSkillButton(UnitHero unitHero, GameObject targetObj);
}
