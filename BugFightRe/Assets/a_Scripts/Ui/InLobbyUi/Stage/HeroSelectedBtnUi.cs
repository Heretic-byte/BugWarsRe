using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HeroSelectUI
{
    public class HeroSelectedBtnUi : MonoBehaviour, IPointerClickHandler
    {
        //얘가 밑에있는 버튼임
        [SerializeField]
        Image _iconImage;
        public Image m_IconImage { get => _iconImage; }

        [SerializeField]
        Image _bugTypeImage;
        public Image m_BugTypeImage { get => _bugTypeImage; }

        [SerializeField]
        Text _heroLevelText;
        public Text m_HeroLevelText { get => _heroLevelText; }

        public SelectedHero m_SelectedHero { get;private set; }
        HeroManager m_HeroManage { get; set; }

        void Start()
        {
            HideSlot();
            m_HeroManage = HeroManager.GetInstance;
        }

        public void SelectHero(SelectedHero selectedHeroData)
        {
            m_SelectedHero = selectedHeroData;
            m_SelectedHero.SetHeroStage(this);
            ShowSlot();
        }

        public void DeselectHero()
        {
            SetNullHero();
            HideSlot();
        }

        void HideSlot()
        {
            m_IconImage.sprite = null;
            m_IconImage.color = new Color(0, 0, 0, 0);
            m_HeroLevelText.text = null;
            m_BugTypeImage.sprite = null;
            m_BugTypeImage.color = new Color(0, 0, 0, 0);
        }
       
        void SetNullHero()
        {
            m_SelectedHero = null;
        }
        void ShowSlot()
        {
            m_IconImage.color = Color.white;
            m_BugTypeImage.color = Color.white;

            m_HeroLevelText.text =m_SelectedHero.m_HeroData.GetLevelText();
            m_IconImage.sprite = m_SelectedHero.m_HeroIcon;
            m_BugTypeImage.sprite =  m_SelectedHero.m_HeroBugTypeIcon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(m_SelectedHero!=null)
            {
                m_SelectedHero.Deselect();
            }
        }
    }
}