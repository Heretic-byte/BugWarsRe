using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;
using System.Collections.Generic;
using MyMarmot.Tools;
namespace HeroSelectUI
{
    public class HeroManager : Singleton<HeroManager>
    {
        [SerializeField]
        GameObject _heroSlotUiPrefab;
        GameObject m_HeroSlotUiPrefab { get => _heroSlotUiPrefab; }

        [SerializeField]
        Transform _heroSlotParentGrid;
        Transform m_HeroSelectSlotParentGrid { get => _heroSlotParentGrid; }

        [SerializeField]
        Transform _heroManageSlotParentGrid;
        Transform m_HeroManageSlotParentGrid { get => _heroManageSlotParentGrid; }

        [SerializeField]
        HeroSelectedBtnUi[] _heroSelectedBtnUis;
        public HeroSelectedBtnUi[] m_HeroSelectedBtnUis { get => _heroSelectedBtnUis; }

        [SerializeField]
        HeroUiInfoData[] _heroUiInfoDatas;
        public HeroUiInfoData[] m_HeroUiInfoDatas { get => _heroUiInfoDatas; }

        [SerializeField]
        HeroHaveInfo _heroHaveInfo;
        public HeroHaveInfo m_HeroHaveInfo { get => _heroHaveInfo; }

        [SerializeField]
        HeroPortraitManager _heroPortraitManager;
        public HeroPortraitManager m_HeroPortraitManager { get => _heroPortraitManager; }

        [Header("Test its SaveData")]//Current SaveData
        [SerializeField]
        private HeroSaveData heroSaveData;
        public HeroSaveData m_HeroSaveData { get => heroSaveData; }

        [ColorUsage(true)]
        public Color m_HideColor;

        [ContextMenu("SaveHeroData")]
        public void Save()
        {
            m_HeroSaveData.SaveData();
        }
        [ContextMenu("LoadHeroData")]
        public void Load()
        {
            m_HeroSaveData.LoadData();
        }

        void Start()
        {
            LoadAndCreateHero();
        }

        void LoadAndCreateHero()
        {
            m_HeroSaveData.LoadData();
            HeroSaveData.HeroLoadData heroLoadData = m_HeroSaveData.LoadHeroData();

            int myHaveHero = heroLoadData.m_MyHaveHero.Count;
            int myNotHaveHero = heroLoadData.m_MyNotHaveHero.Count;

            CreateHeroModel();

            for (int i = 0; i < myHaveHero; i++)
            {
                var heroFirstData = heroLoadData.m_MyHaveHero.First.Value;
                CreateMyHeroUi(heroFirstData);
                heroLoadData.m_MyHaveHero.RemoveFirst();
            }

            for (int j = 0; j < myNotHaveHero; j++)
            {
                var heroFirstData = heroLoadData.m_MyNotHaveHero.First.Value;
                CreateManageNotHaveHeroSlotUi(heroFirstData);
                heroLoadData.m_MyNotHaveHero.RemoveFirst();
            }
        }

        private void CreateHeroModel()
        {
            for(int i=0; i< m_HeroUiInfoDatas.Length;i++)
            m_HeroPortraitManager.CreateHeroModel(i);
        }

        public void SetHaveInfoPanel(HeroData heroData)
        {
            m_HeroHaveInfo.transform.parent.gameObject.SetActive(true);
            m_HeroHaveInfo.SetHeroHaveInfoPanel(heroData);
            m_HeroPortraitManager.MoveCamera(heroData.m_HeroIndex);
        }

        public string GetHeroName(int index)
        {
            return m_HeroUiInfoDatas[index].m_HeroName;
        }

        public StatDataBase GetHeroStatData(int index)
        {
            return m_HeroUiInfoDatas[index].m_HeroRealStat;
        }

        public HeroInstanceData GetHeroInstanceData(int index)
        {
            return m_HeroUiInfoDatas[index].m_HeroInstanceData;
        }

        public Sprite GetHeroBugTypeIcon(int index)
        {
            return m_HeroUiInfoDatas[index].m_BugTypeIcon;
        }

        public void CreateMyHeroUi(HeroData heroData)
        {   //call this for add new Character
            CreateStageHeroSlotUi(heroData);
            CreateManageMyHaveHeroSlotUi(heroData);
            //이미있는 nothave ui를 지워야함

        }

        HeroSelectSlotUi CreateHeroSelectSlotUi(HeroData heroData,Transform parentTrans)
        {
            var Obj = Instantiate(m_HeroSlotUiPrefab, parentTrans)
                   .GetComponent<HeroSelectSlotUi>();
            Obj.SetHeroData(heroData);

            return Obj;
        }

        void CreateStageHeroSlotUi(HeroData heroData)
        {
            var Obj = CreateHeroSelectSlotUi(heroData,m_HeroSelectSlotParentGrid);
            Obj.SetSlotDelegate();
        }

        void CreateManageMyHaveHeroSlotUi(HeroData heroData)
        {
            var ManageObj = CreateHeroSelectSlotUi(heroData, m_HeroManageSlotParentGrid);
            ManageObj.SetManageSlotHaveDelegate();
        }

        void CreateManageNotHaveHeroSlotUi(HeroData heroData)
        {
            var NotHaveManageObj = CreateHeroSelectSlotUi(heroData,m_HeroManageSlotParentGrid);
            NotHaveManageObj.SetManageSlotNotHaveDelegate();
        }

        void SelectHeroToGoStage(SelectedHero heroData)
        {
            var btnIndex = FindUnselectedBtnEarly();
            if (btnIndex == -1)
            {
                Debug.Log("꽉참");
                return;
            }
            Debug.Log(btnIndex);
            m_HeroSelectedBtnUis[btnIndex].SelectHero(heroData);
        }

        //아직안씀
        void SortHero(int CurrentIndex)
        {
            if (CurrentIndex >= m_HeroSelectedBtnUis.Length)
            {
                Debug.Log("소트꽉참1");
                return;
            }

            var selected = m_HeroSelectedBtnUis[CurrentIndex].m_SelectedHero;

            if (selected == null)
            {
                var FoundIndex = FindSelectedBtnEarly();
                if (FoundIndex == -1)
                {
                    return;
                }
                var NewSelected = m_HeroSelectedBtnUis[FoundIndex].m_SelectedHero;
                m_HeroSelectedBtnUis[CurrentIndex].SelectHero(NewSelected);
                Debug.Log("소트성공");
            }

            CurrentIndex++;
            SortHero(CurrentIndex);
            Debug.Log("소트넘어감");
        }

        private int FindSelectedBtnEarly()
        {
            for (int i = 0; i < m_HeroSelectedBtnUis.Length; i++)
            {
                if (m_HeroSelectedBtnUis[i].m_SelectedHero != null)
                {
                    return i;
                }
            }
            return -1;
        }

        private int FindUnselectedBtnEarly()
        {
            for (int i = 0; i < m_HeroSelectedBtnUis.Length; i++)
            {
                if (m_HeroSelectedBtnUis[i].m_SelectedHero == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public void SelectHero(SelectedHero heroData)
        {
            SelectHeroToGoStage(heroData);
        }

        public SelectedHero[] GetSelectedHeroPrefabObj()
        {
            SelectedHero[] HeroSelectedObj = new SelectedHero[m_HeroSelectedBtnUis.Length];
            for (int i = 0; i < m_HeroSelectedBtnUis.Length; i++)
            {
                if (m_HeroSelectedBtnUis[i].m_SelectedHero != null)
                {
                    HeroSelectedObj[i] = m_HeroSelectedBtnUis[i].m_SelectedHero;
                }
            }

            return HeroSelectedObj;
        }
      


        void SetSelectHeroForCreateHero()
        {
            //call this before stage entrance
            var selectedHero = GetSelectedHeroPrefabObj();
            PlayerManager.GetInstance.SetSelectHero(selectedHero);

        }

        public void MoveToStage(int index)
        {
            SetSelectHeroForCreateHero();
            mySceneManager.GetInstance.MoveScene(index);
        }
    }

    [System.Serializable]
    public class HeroUiInfoData
    {
        public string m_HeroName;
        public Sprite m_BugTypeIcon;
        public StatDataBase m_HeroRealStat;
        public HeroInstanceData m_HeroInstanceData;
    }

    [System.Serializable]
    public class EventHeroData : UnityEvent<HeroData> { }
    [System.Serializable]
    public struct HeroInstanceData
    {
        public Sprite m_HeroIcon;
        public GameObject m_HeroPrefab;
    }
    [System.Serializable]
    public class HeroSaveData
    {
        [System.Serializable]
        public class HeroLoadData
        {
            public LinkedList<HeroData> m_MyHaveHero = new LinkedList<HeroData>();
            public LinkedList<HeroData> m_MyNotHaveHero = new LinkedList<HeroData>();
        }

        public HeroData[] m_TotalHeroDatas;//기본값있음


        public void LoadData()
        {
            var LoadedData = SaveLoadManager.Load("MyHeroData") as HeroData[];
            if (LoadedData != null)
            {
                m_TotalHeroDatas = LoadedData;
            }
        }
        public void SaveData()
        {
            for(int i=0; i< m_TotalHeroDatas.Length;i++)
            {
                foreach (var AA in m_TotalHeroDatas[i].m_ItemSaveData)
                {
                    AA.SetNullDelegate();
                }
            }

            SaveLoadManager.Save(m_TotalHeroDatas, "MyHeroData");
        }

        public HeroLoadData LoadHeroData()
        {
            HeroLoadData heroLoadData = new HeroLoadData();

            foreach (var AA in m_TotalHeroDatas)
            {
                if (AA.m_Ihave)
                {
                    heroLoadData.m_MyHaveHero.AddLast(AA);
                }
                else
                {
                    heroLoadData.m_MyNotHaveHero.AddLast(AA);
                }
            }

            return heroLoadData;
        }

        //애초부터 가지고있는 리스트만 만들고
        //만든 다음에 비교하자
    }
}