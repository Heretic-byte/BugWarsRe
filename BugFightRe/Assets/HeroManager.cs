using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
namespace HeroSelectUI
{
    public class HeroManager : Singleton<HeroManager>
    {
        [SerializeField]
        HeroInstanceData[] _heroInstanceDatas;
        HeroInstanceData[] m_HeroInstanceDatas { get => _heroInstanceDatas; }

        [SerializeField]
        Sprite[] _bugTypeIcon;
        Sprite[] m_BugTypeIcon { get => _bugTypeIcon; }

        public HeroInstanceData GetHeroInstanceData(int index)
        {
            return m_HeroInstanceDatas[index];
        }

        public Sprite GetHeroBugTypeIcon(int index)
        {
            return m_BugTypeIcon[index];
        }

        public void SelectHeroToGoStage(HeroData heroData)
        {
            //선택버튼으로 넘어가야함

            //콜백으로 몰라도 되게만들어야하

        }

        public void DeselectHeroFromGoStage(HeroData heroData)
        {


        }

        //forArray
      
        //forBtn

    }

    [System.Serializable]
    public struct HeroInstanceData
    {
        public Sprite m_HeroIcon;
        public GameObject m_HeroPrefab;
    }
}