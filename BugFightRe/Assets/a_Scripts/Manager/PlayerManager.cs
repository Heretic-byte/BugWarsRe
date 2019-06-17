using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroSelectUI;
public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject[] m_SelectedHeroes { get; set; } = new GameObject[3];
    StageData m_SelectedStage { get; set; }


    public void SetSelectHero(SelectedHero[] selectedHeroes)
    {
        //이전로비씬에서 불림
        for (int i = 0; i < selectedHeroes.Length; i++)
        {
            if (selectedHeroes[i] != null)
            {
                var HeroInstance = HeroManager.GetInstance.GetHeroInstanceData(selectedHeroes[i].m_HeroData.m_HeroIndex);

                m_SelectedHeroes[i] = HeroInstance.m_HeroPrefab;
            }
        }
    }

    public void SetStageData(StageData stageData)
    {
        m_SelectedStage = stageData;
    }

    public StageData GetStageData( )
    {
      return  m_SelectedStage ;
    }


    //    그냥 순서대로 버튼 생성됬으면 좋겠다.

    //히어로 컴퍼넌트가 외부레벨등 데이터 셋되야함

    //씬트랜지션필요

}
