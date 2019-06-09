using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class StageButton : MonoBehaviour,IPointerClickHandler
{
    bool m_IsCleared { get; set; }

    [SerializeField]
    StageButton _unlockStageButtons;

    [SerializeField]
    Color _lockedColor;

    [SerializeField]
    bool _isUnlocked;

    [SerializeField]
    EventStageData _onClickStageData;

    [SerializeField]
    StageData _stageData;
  
    private Text m_StageIndexName { get; set; }
    public StageButton m_UnlockStageButtons { get => _unlockStageButtons; }
    private Image[] m_StageButtonIcons { get; set; }
    public Color m_LockedColor { get => _lockedColor; }
    public bool m_IsUnlocked { get => _isUnlocked; }
    public EventStageData m_OnClick { get => _onClickStageData; }
    public QuestStarStageShow m_QuestStar { get; set; }
    public StageData m_StageData { get => _stageData; }

    private void Awake()
    {
        SetText();
        SetIconImages();
        SetQuestStar();
    }

    private void Start()
    {
        SetStageButton();
    }
    private void SetText()
    {
        m_StageIndexName = GetComponentInChildren<Text>();
        m_StageIndexName.text = m_StageData.GetStageIndexName();
    }
    private void SetQuestStar()
    {
        m_QuestStar = GetComponent<QuestStarStageShow>();
    }
    private void SetStageButton()
    {
        if(m_IsUnlocked)
        {
            UnlockStage();
            LoadClear();
            return;
        }

        LockStage();
    }

    private void SetIconImages()
    {
        m_StageButtonIcons=GetComponentsInChildren<Image>();
    }

    private void LoadClear()
    {
        if(m_IsCleared)
        {
            ClearStage();
        }
    }

    public void ClearStage()
    {
        ShowCleared();
        SetOtherStageUnlock();
    }

    private void SetOtherStageUnlock()
    {
        m_UnlockStageButtons.UnlockStage();
    }

    private void LockStage()
    {
        SetLockIconColor();
    }

    private void SetLockIconColor()
    {
        foreach (var BB in m_StageButtonIcons)
        {
            BB.DOColor(m_LockedColor, 0);
        }
    }

    private void SetUnlockIconColor()
    {
        foreach (var CC in m_StageButtonIcons)
        {
            CC.DOColor(Color.white, 0);
        }
    }

    public void UnlockStage()
    {
        SetUnlockIconColor();
    }

    private void ShowCleared()
    {
        SetStarQuest();
    }
  
    private void SetStarQuest()
    {
        m_QuestStar.ShowStar(m_StageData.GetClearedQuestCount());
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("AA");
        if(!m_IsUnlocked)
        { 
            return;
        }
        Debug.Log("BB");
        m_OnClick?.Invoke(m_StageData);
        //TODO: 스테이지 정보 패널에 정보 출력
        
    }


    [Serializable]
    public class EventStageData : UnityEvent<StageData> { }
}
