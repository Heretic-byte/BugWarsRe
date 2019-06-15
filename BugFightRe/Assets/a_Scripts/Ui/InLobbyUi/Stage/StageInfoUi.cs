using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class StageInfoUi : MonoBehaviour
{
    [Header("스테이지 이름표시")]
    [SerializeField]
    Text _stageName;

    [Header("퀘스트 상세패널")]
    [SerializeField]
    QuestInfoUi _questInfoUi;
    public QuestInfoUi m_QuestInfoUi { get => _questInfoUi;}
    [Header("퀘스트 별표시")]
    [SerializeField]
    QuestInfoShow _questInfoShow;
    public QuestInfoShow m_QuestInfoShow { get => _questInfoShow;}
    [SerializeField]
    ElementSlotParent _enemyHeroSlotP;
    [SerializeField]
    ElementSlotParent _enemyMonsterSlotP;
    [SerializeField]
    ElementSlotParent _itemSlotP;
    [SerializeField]
    ElementSlotParent _prizeSlotP;

    private ElementSlotParent m_EnemyMonsterSlotP { get => _enemyMonsterSlotP; }
    private ElementSlotParent m_EnemyHeroSlotP { get => _enemyHeroSlotP; }
    private ElementSlotParent m_ItemSlotP { get => _itemSlotP; }
    private ElementSlotParent m_PrizeSlotP { get => _prizeSlotP; }
    private Text m_StageName { get => _stageName;  }

    public void SetStageInfo(StageData stageData)
    {
        m_StageName.text = stageData.GetStageName();

        m_QuestInfoUi.SetStageData(stageData);
        m_QuestInfoShow.SetStageData(stageData);

        m_EnemyHeroSlotP.SetIcons(stageData.m_EnemyHeroDatas);
        m_EnemyMonsterSlotP.SetIcons(stageData.m_EnemyDatas);
        m_ItemSlotP.SetIcons(stageData.m_ItemDatas);
        m_PrizeSlotP.SetIcons(stageData.m_PrizeItemDatas);


    }
}
