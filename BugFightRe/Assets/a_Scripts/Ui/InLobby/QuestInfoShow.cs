using UnityEngine.UI;
using UnityEngine;

public class QuestInfoShow : MonoBehaviour
{
    //패널에 걸려서 별갯수보여주는 애
    [SerializeField]
    Image[] _starQuestIcon;

    public Image[] m_StarQuestIcon { get => _starQuestIcon;}
    StageData m_StageData { get; set; }

    public void SetStageData(StageData stageData)
    {
        m_StageData = stageData;

        ShowStar(m_StageData.GetClearedQuestCount());
    }

    void ShowStar(int count)
    {
        for (int i = 0; i < count; i++)
        {
            m_StarQuestIcon[i].color = Color.white;
        }
    }
}
