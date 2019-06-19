using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSlotInfo : MonoBehaviour
{
    public int m_CurrentSelectedItemIndex { get;private set; }=-1;

    public void SetSelectedItemIndex(int index)
    {//unityEvent,slottoggle
        m_CurrentSelectedItemIndex = index;
    }


}
