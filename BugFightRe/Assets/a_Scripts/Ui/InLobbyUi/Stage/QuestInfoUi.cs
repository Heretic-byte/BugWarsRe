using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestInfoUi : MonoBehaviour
{
    [SerializeField]
    Image[] _starObjs;
    public Image[] m_StarObjs { get => _starObjs; }
    public StageData m_StageData { get; set; }

    [SerializeField]
    Text[] _questTexts;
    public Text[] m_QuestTexts { get => _questTexts;}

    public void SetStageData(StageData stageData)
    {
        m_StageData = stageData;

        SetInfoUi();
    }

    private void SetInfoUi()
    {
        SetQuestInfo();
    }


    private void SetQuestInfo()
    {
        var questDatas = m_StageData.GetQuests();

        for (int i = 0; i < questDatas.Length; i++)
        {
            m_QuestTexts[i].text = questDatas[i].m_QuestDescription;

            if(questDatas[i].CheckCleared(m_StageData.GetStageIndexName()))
            {
                m_StarObjs[i].color = Color.white;
            }
        }
    }
}
