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

        SelectedHero m_SelectedHeroData { get; set; }

        HeroManager m_HeroManage { get; set; }

        [Header("ForFade")]
        [SerializeField]
        GameObject _fadeIconObj;
        public GameObject m_FadeIconObj { get => _fadeIconObj; }

        UnityAction m_OnClick { get; set; }

        [SerializeField]
        HeroHaveInfo _heroHaveInfo;
        public HeroHaveInfo m_HeroHaveInfo { get => _heroHaveInfo; }

        public void Awake()
        {
            Deselected();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            m_OnClick?.Invoke();
        }

        public void SetSlotDelegate()
        {//생성시 반드시 불러줘야함
            m_OnClick = OnClickSlot;
        }
        public void SetManageSlotHaveDelegate()
        {
            m_OnClick = OnClickManageSlotHave;
        }
        public void SetManageSlotNotHaveDelegate()
        {
            m_OnClick = OnClickManageNotHave;
        }
        private void OnClickSlot()
        {
            if (m_FadeIconObj.activeSelf)
            {
                //켜저있으면 선택됬던거
                m_SelectedHeroData.Deselect();
            }
            else
            {
                m_HeroManage.SelectHero(m_SelectedHeroData);
                Selected();
            }
        }

        private void OnClickManageSlotHave()
        {
            //가지고 있는애들은 
            //업글패널띄워야함
            m_HeroManage.SetHaveInfoPanel(m_SelectedHeroData.m_HeroData);
        }
        private void OnClickManageNotHave()
        {
            //그럼이것도 새로운 영웅 얻으면 바껴야겠네?

            //가지고 있지않은애들은
            //정보패널만띄워야함
        }


        public void SetHeroData(HeroData heroData)
        {
            //생성될때 이걸부름
            m_HeroManage = HeroManager.GetInstance;

            m_SelectedHeroData = new SelectedHero();

            m_SelectedHeroData.SetHeroData(heroData);

            m_SelectedHeroData.SetHeroHome(this);

            var heroInstance = m_HeroManage.GetHeroInstanceData(m_SelectedHeroData.m_HeroData.m_HeroIndex);

            m_SelectedHeroData.SetHeroIcon(heroInstance.m_HeroIcon, m_HeroManage.GetHeroBugTypeIcon(m_SelectedHeroData.m_HeroData.m_HeroIndex));

            ShowSlot();

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

        public void Deselected()
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
        public Sprite m_HeroIcon { get; private set; }
        public Sprite m_HeroBugTypeIcon { get; private set; }

        public HeroSelectedBtnUi m_MyStageEntrance { get; private set; }
        public HeroSelectSlotUi m_MyHomeEntrance { get; private set; }

        public void SetHeroData(HeroData heroData)
        {
            m_HeroData = heroData;
        }
        public void SetHeroIcon(Sprite heroSpr, Sprite bugType)
        {
            m_HeroIcon = heroSpr;
            m_HeroBugTypeIcon = bugType;
        }

        public void SetHeroHome(HeroSelectSlotUi heroSelectSlotUi)
        {
            m_MyHomeEntrance = heroSelectSlotUi;
        }
        public void SetHeroStage(HeroSelectedBtnUi heroSelectedBtnUi)
        {
            m_MyStageEntrance = heroSelectedBtnUi;
        }

        public void Deselect()
        {
            m_MyStageEntrance.DeselectHero();
            m_MyHomeEntrance.Deselected();
            m_MyStageEntrance = null;
        }
    }
}