using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "DataBase/QuestData")]
public  class QuestData : StageDataElement
{
    [SerializeField]
    [TextArea]
    string _questDescription;
    [SerializeField]
    EventQuestData _eventQuestData;
    public string m_QuestDescription { get => _questDescription;}
    public EventQuestData m_EventQuestData { get => _eventQuestData; }

    public void ClearQuest(StageData stageData)
    {
        PlayerPrefs.SetInt(stageData.GetStageIndexName(), 1);
    }

    public bool CheckCleared(string stageName)
    {
        int result = PlayerPrefs.GetInt(stageName, 0);

        if(result!=0)
        {
            return true;
        }

        return false;
    }

    public virtual  void SetQuestToScene()
    {
        m_EventQuestData?.Invoke(this);
    }

    public class QuestCompare : IComparer<QuestData>
    {
        string m_StageName { get; set; }

        public QuestCompare(string stageName)
        {
            m_StageName = stageName;
        }

        public int Compare(QuestData x, QuestData y)
        {
            if(x.CheckCleared(m_StageName))
            {
                return 1;
            }
            if (y.CheckCleared(m_StageName))
            {
                return -1;
            }

            return 0;

        }
    }

    [System.Serializable]
    public class EventQuestData:UnityEvent<QuestData> { }
}
