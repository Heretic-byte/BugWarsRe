using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace HeroSelectUI
{
    public class HeroSelectSlotUi : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField]
        Image _iconImage;
        public Image m_IconImage { get => _iconImage; }

        [SerializeField]
        Image _bugTypeImage;
        public Image m_BugTypeImage { get => _bugTypeImage; }

        [SerializeField]
        Text _heroLevelText;
        public Text m_HeroLevelText { get => _heroLevelText; }

        SelectedHero m_SelectedHeroData { get; set; } = new SelectedHero();

        HeroManager m_HeroManage { get; set; }

        [SerializeField]
        EventSelectedHeroData _onSelectedHeroData;
        public EventSelectedHeroData m_OnSelectedHeroData { get => _onSelectedHeroData; }

        [Header("ForFade")]
        [SerializeField]
        GameObject _fadeIconObj;
        public GameObject m_FadeIconObj { get => _fadeIconObj; }

        void Awake()
        {
            Deselected();
            m_SelectedHeroData.SetOnSelect(Selected);
            m_SelectedHeroData.SetOnDeselect(Deselected);
        }

        public void SetHeroData(HeroData heroData)
        {
            m_HeroManage = HeroManager.GetInstance;

            m_SelectedHeroData.SetHeroData(heroData);
           
            var heroInstance = m_HeroManage.GetHeroInstanceData(m_SelectedHeroData.m_HeroData.m_HeroIndex);
            m_SelectedHeroData.SetHeroIcon(heroInstance.m_HeroIcon, m_HeroManage.GetHeroBugTypeIcon((int)m_SelectedHeroData.m_HeroData.m_HeroType));
            ShowSlot();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_OnSelectedHeroData?.Invoke(m_SelectedHeroData);
        }

        void HideSlot()
        {
            m_IconImage.sprite = null;
            m_IconImage.color = new Color(0, 0, 0, 0);
            m_HeroLevelText.text = null;
            m_BugTypeImage.sprite = null;
            m_BugTypeImage.color = new Color(0, 0, 0, 0);
        }

        void ShowSlot()
        {
            m_IconImage.color = Color.white;
            m_BugTypeImage.color = Color.white;

            m_IconImage.sprite = m_SelectedHeroData.m_HeroIcon;
            m_BugTypeImage.sprite = m_SelectedHeroData.m_HeroBugTypeIcon;
            m_HeroLevelText.text = "Lv" + m_SelectedHeroData.m_HeroData.m_HeroLevel.ToString();
        }

        void Selected()
        {
            m_FadeIconObj.gameObject.SetActive(true);
        }

        void Deselected()
        {
            m_FadeIconObj.gameObject.SetActive(false);
        }

        void SetIconSprite(Sprite icon)
        {
            m_IconImage.color = Color.white;
            m_IconImage.sprite = icon;
        }

        void SetNullIcon()
        {
            m_IconImage.sprite = null;
            m_IconImage.color = new Color(0, 0, 0, 0);
        }
    }

    [System.Serializable]
    public class EventSelectedHeroData : UnityEvent<SelectedHero> { }

    [System.Serializable]
    public class SelectedHero
    {
        //로비에서 넘겨주는 선택영웅데이터
        public HeroData m_HeroData { get; private set; }
        public UnityAction m_OnSelected { get; private set; }
        public UnityAction m_OnDeselected { get; private set; }
        public Sprite m_HeroIcon { get; private set; }
        public Sprite m_HeroBugTypeIcon { get; private set; }
        public void SetHeroData(HeroData heroData)
        {
            m_HeroData = heroData;
        }
        public void SetHeroIcon(Sprite heroSpr,Sprite bugType)
        {
            m_HeroIcon = heroSpr;
            m_HeroBugTypeIcon = bugType;
        }
        public void SetOnSelect(UnityAction onSelect)
        {
            m_OnSelected += onSelect;
        }

        public void SetOnDeselect(UnityAction onDeselect)
        {
            m_OnDeselected += onDeselect;
        }
    }
}