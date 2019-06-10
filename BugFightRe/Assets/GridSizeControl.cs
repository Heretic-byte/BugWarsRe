using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridSizeControl : MonoBehaviour
{
    [SerializeField]
    int _horizontalCellCount = 8;
    [SerializeField]
    float _cellVerticalSize = 100f;
    public int m_HorizontalCellCount { get => _horizontalCellCount;}
    public float m_CellVerticalSize { get => _cellVerticalSize;}

    RectTransform rectTransform { get; set; }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetGrid()
    {
        int childCount = rectTransform.childCount;

        var rect= rectTransform.rect;
        rect.height = (rectTransform.childCount / m_HorizontalCellCount) * m_CellVerticalSize;

        if (childCount % m_HorizontalCellCount > 0)
        {
            rect.height += m_CellVerticalSize;
        }

        rectTransform.sizeDelta = rect.size;

        Debug.Log("Height:" + rect.size);
    }




}
