using UnityEngine;

[CreateAssetMenu(menuName ="DataBase/StageData")]
public class StageData : ScriptableObject
{
    [SerializeField]
    private int _mainIndex = 1;
    private int m_MainIndex { get => _mainIndex; }

    [SerializeField]
    private int _stageIndex = 1;
    private int m_StageIndex { get => _stageIndex;}

    [SerializeField]
    private string _stageName = "통나무숲";
    private string m_StageName { get => _stageName;}

    [Header("Max Is 5")]
    [SerializeField]
    private EnemyData[] _enemyDatas;
    private EnemyData[] m_EnemyDatas { get => _enemyDatas;}
    [Header("Max Is 5")]
    [SerializeField]
    private ItemData[] _itemDatas;
    private ItemData[] m_ItemDatas { get => _itemDatas;}
    [Header("Max Is 3")]
    [SerializeField]
    private QuestData[] _questDatas;
    private QuestData[] m_QuestDatas { get => _questDatas;}

    public string GetStageName()
    {
        return m_MainIndex.ToString() + "-" + m_StageIndex.ToString();
    }

    public int GetClearedQuest()
    {
        int resultCount = 0;

        if(m_QuestDatas.Length<1)
        {
            return 0;
        }

        foreach(var AA in m_QuestDatas)
        {
            if(AA.CheckCleared(this))
            {
                resultCount++;
            }
        }

        return resultCount;
    }



}
