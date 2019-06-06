using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestData : ScriptableObject
{
    public void ClearQuest(StageData stageData)
    {
        PlayerPrefs.SetInt(stageData.GetStageName(), 1);
    }

    public bool CheckCleared(StageData stageData)
    {
        int result = PlayerPrefs.GetInt(stageData.GetStageName(), 0);

        if(result!=0)
        {
            return true;
        }

        return false;
    }

    public abstract void SetQuestToScene();
}
