using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class DragSkillButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    UnityAction<UnitHero> m_OnClickAction { get; set; }
    UnityAction<UnitHero> m_OnBeginDragAction { get; set; }
    UnityAction<UnitHero> m_OnMoveDragAction { get; set; }
    UnityAction<UnitHero> m_OnEndDragAction { get; set; }
    HeroSkillUse m_SkillUse { get; set; }
    UnitHero m_UnitHero { get; set; }

    public void SetInstance(UnitHero unitHero)
    {
        m_UnitHero = unitHero;
    }

    void SetDragSkill(UnityAction<UnitHero> clickSkill, UnityAction<UnitHero> beginDragSkill, UnityAction<UnitHero> moveDragSkill, UnityAction<UnitHero> endDragSkill)
    {
        m_OnClickAction = clickSkill;
        m_OnBeginDragAction = beginDragSkill;
        m_OnMoveDragAction = moveDragSkill;
        m_OnEndDragAction = endDragSkill;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_OnClickAction?.Invoke(m_UnitHero);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_OnBeginDragAction?.Invoke(m_UnitHero);
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_OnMoveDragAction?.Invoke(m_UnitHero);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_OnEndDragAction?.Invoke(m_UnitHero);
    }
}
