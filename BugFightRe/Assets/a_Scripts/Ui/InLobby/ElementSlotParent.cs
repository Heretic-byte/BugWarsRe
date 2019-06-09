using UnityEngine;
using UnityEngine.UI;


public class ElementSlotParent : MonoBehaviour
{
    public IconShowUi[] m_IconShowUis { get; set; }

    void Start()
    {
        m_IconShowUis = GetComponentsInChildren<IconShowUi>();
    }


    public void SetIcons(StageDataElement[] stageDataElement)
    {
       for(int i=0; i< stageDataElement.Length;i++)
        {
            m_IconShowUis[i].ShowIcon(stageDataElement[i].m_ElementIcon);
        }
    }

}
