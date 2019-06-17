using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events;
using MyMarmot.Tools;
[System.Serializable]
public class ItemInstanceData
{
    //who
    public int m_EquippedHeroInstanceIndex = -1;
    //this item
    public int m_ItemSlot=-1;
    //where slot
    public int m_ItemInstanceIndex=0;

    UnityAction m_OnEquip { get; set; }
    public UnityAction m_OnUnEquip { get; set; }

    public void SetNullDelegate()
    {
        m_OnEquip = null;
        m_OnUnEquip = null;
    }

    public void AddOnEquip(UnityAction unityAction)
    {
        m_OnEquip += unityAction;
    }

    public void RemoveOnEquip(UnityAction unityAction)
    {
        m_OnEquip -= unityAction;
    }

    public void AddOnUnEquip(UnityAction unityAction)
    {
        m_OnUnEquip += unityAction;
        Debug.Log("REAL:" + m_OnUnEquip);
    }

    public void RemoveOnUnEquip(UnityAction unityAction)
    {
        m_OnUnEquip -= unityAction;
    }

    public void SetItemInstanceIndex(int itemIndex)
    {
        m_ItemInstanceIndex = itemIndex;
    }

    public void Equip(int heroIndex,int heroSlotIndex)
    {
        

        m_EquippedHeroInstanceIndex = heroIndex;
        m_ItemSlot = heroSlotIndex;

        m_OnEquip?.Invoke();
    }

    public void UnEquip()
    {
        m_EquippedHeroInstanceIndex = -1;
        m_ItemSlot = -1;
        
        m_OnUnEquip?.Invoke();
    }






}
