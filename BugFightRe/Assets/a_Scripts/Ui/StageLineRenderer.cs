using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageLineRenderer : MonoBehaviour
{
    [SerializeField]
    StageButton _firstStageButton;

    public StageButton m_FirstStageButton { get => _firstStageButton; }
    LineRenderer m_LineRender { get; set; }

    [SerializeField]
    private int _stageBtnCount = 10;
    public int m_StageBtnCount { get => _stageBtnCount;}

    int index = 0;

    void Start()
    {
        m_LineRender = GetComponent<LineRenderer>();
    }
   

    void SetLine()
    {
        m_LineRender.positionCount = m_StageBtnCount;

        SetUnlockStages(m_FirstStageButton.m_UnlockStageButtons);
    }
    
    void SetUnlockStages(StageButton[] unlockBtn)
    {
        for(int i=0; i< unlockBtn.Length;i++)
        {
            m_LineRender.SetPosition(index, unlockBtn[i].transform.position);
            index++;
            SetUnlockStages(unlockBtn[i].m_UnlockStageButtons);
           
        }
    }


}
