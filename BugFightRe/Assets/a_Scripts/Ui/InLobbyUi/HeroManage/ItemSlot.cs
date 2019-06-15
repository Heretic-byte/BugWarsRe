using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    ItemData m_SlotItem { get; set; }
    [SerializeField]
    IconShowUi _iconShowUi;
    //터치시 현재 아이템정보 띄워줘야함
    //또한 이는 인벤토리에서 그대로 쓸수있어야함
  
    public void SetItemData(ItemData itemData)
    {
        m_SlotItem = itemData;
        _iconShowUi.ShowIcon(m_SlotItem.m_ElementIcon);
    }

    public void SetNullItemData()
    {
        m_SlotItem = null;
        _iconShowUi.HideIcon();
    }


}
