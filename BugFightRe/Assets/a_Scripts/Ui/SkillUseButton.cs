using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class SkillUseButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    UnityAction m_OnClickAction { get; set; }
    UnityAction m_OnBeginDragAction { get; set; }
    UnityAction m_OnMoveDragAction { get; set; }
    UnityAction m_OnEndDragAction { get; set; }
    HeroSkillUse m_SkillUse { get; set; }


    public void SetSkill(UnitHero unitHero)
    {
        m_SkillUse=unitHero.GetComponent<HeroSkillUse>();

        if(m_SkillUse.m_Skill.m_IsDragSkill)
        {
           
        }
       
    }

     void SetClickSkill(UnityAction clickUseSkill)
    {
        m_OnClickAction = clickUseSkill;
    }

     void SetNullClickSkill()
    {
        m_OnClickAction = null;
    }

     void SetDragSkill(UnityAction beginDragSkill, UnityAction moveDragSkill, UnityAction endDragSkill)
    {
        m_OnBeginDragAction = beginDragSkill;
        m_OnMoveDragAction = moveDragSkill;
        m_OnEndDragAction = endDragSkill;
    }

     void SetNullDragSkill()
    {
        m_OnBeginDragAction = null;
        m_OnMoveDragAction = null;
        m_OnEndDragAction = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_OnBeginDragAction?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_OnMoveDragAction?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_OnEndDragAction?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_OnClickAction?.Invoke();
    }
}
