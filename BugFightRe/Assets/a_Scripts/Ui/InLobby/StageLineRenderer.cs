using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageLineRenderer : MonoBehaviour
{
    [SerializeField]
    RectTransform[] _stageButtons;

    public RectTransform[] m_StageButton { get => _stageButtons; }
    LineRenderer m_LineRender { get; set; }

    void Start()
    {
        m_LineRender = GetComponent<LineRenderer>();
        SetLine();
    }
   
    void SetLine()
    {
        m_LineRender.positionCount = m_StageButton.Length;

        int index = 0;

        foreach (var AA in m_StageButton)
        {
            m_LineRender.SetPosition(index,AA.position);
           
            index++;
        }
        //SetLastPoint(index - 1);
    }
    void SetLastPoint(int lastIndex)
    {
        var LastPos=m_LineRender.GetPosition(lastIndex);
        LastPos.z = 0;
        m_LineRender.SetPosition(lastIndex, LastPos);
    }

}
