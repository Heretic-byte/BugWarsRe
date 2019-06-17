using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
public class ClickSkillButton : MonoBehaviour, IPointerClickHandler
{
    UnityAction<UnitHero> m_OnClickAction { get; set; }
    UnitHero m_UnitHero { get; set; }
    ManaManager m_ManaManage { get; set; }
    SkillBase m_Skill { get; set; }
    float m_Timer { get; set; }
    Sequence m_CooldownSeq { get; set; }
    
   
    public Image m_CdImage { get; set; }

    ubHeroAnimDelegate m_UnitHeroAnim { get; set; }

    Image m_MainIcon { get; set; }
    Text m_ManaCostText { get; set; }

    public void SetInstance(UnitHero unitHero,SkillBase skillBase)
    {
        m_UnitHero = unitHero;
        m_ManaManage = ManaManager.GetInstance;
        m_Skill = skillBase;

        m_CdImage = transform.GetChild(1).GetComponent<Image>();
        //
        m_MainIcon = GetComponent<Image>();
        m_MainIcon.sprite = skillBase.m_SkillIcon;
        //
        m_ManaCostText = GetComponentInChildren<Text>();
        m_ManaCostText.text = ":"+skillBase.m_ManaCost.ToString();


        m_UnitHeroAnim = m_UnitHero.GetComponent<ubHeroAnimDelegate>();

        SetCoolDownZero();
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
        if(!CheckSkillCanUse())
        {
            return;
        }

        if (!m_ManaManage.UsePlayerMana(m_Skill))
        {
            return;
        }

        m_UnitHeroAnim.UseSkillMotion1();

        m_OnClickAction?.Invoke(m_UnitHero);

        CoolDownStart();
    }

    void CoolDownStart()
    {
        m_CdImage.DOFillAmount(1, 0);

        m_Timer = m_Skill.m_CoolDown;

        m_CdImage.DOFillAmount(0, m_Timer).OnComplete(SetCoolDownZero).SetEase(Ease.Linear);
     

    }
    void SetCoolDownZero()
    {
        m_Timer = 0f;
    }

    bool CheckSkillCanUse()
    {
     
        //우물
        if(!m_UnitHero._myIsHeroInLine)
        {
            return false;
        }

        if (m_Timer>0)
        {
            return false;
        }


        return true;
    }
}
