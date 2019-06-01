using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickSkillButton : MonoBehaviour, IPointerClickHandler
{
    UnityAction<UnitHero> m_OnClickAction { get; set; }
    UnitHero m_UnitHero { get; set; }

    public void SetInstance(UnitHero unitHero)
    {
        m_UnitHero = unitHero;
    }

    public void SetClickSkill(UnityAction<UnitHero> clickUseSkill)
    {
        m_OnClickAction = clickUseSkill;
    }

    void SetNullClickSkill()
    {
        m_OnClickAction = null;
    }
  
    public void OnPointerClick(PointerEventData eventData)
    {
        m_OnClickAction?.Invoke(m_UnitHero);
    }
}
