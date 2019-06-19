using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using HeroSelectUI;
public class InvenSlotUi : MonoBehaviour
{
    [SerializeField]//slot item icon
    IconShowUi _iconShowUi;
    [SerializeField]//who equiped
    IconShowUi _equipIcon;//이걸로 클릭시킬것

    HeroManager m_HeroManager { get; set; }
    ItemManager m_ItemManager { get; set; }
    HeroInfo m_HeroInfo { get; set; }
    InvenSlotInfo m_InvenSlotInfo { get; set; }


    ItemInstanceData m_ItemInstanceData { get; set; }


    public void SetItemData(ItemInstanceData itemInstanceData, InvenSlotInfo invenSlotInfo)
    {//when this prefab born
        m_HeroManager = HeroManager.GetInstance;
        m_ItemManager = ItemManager.GetInstance;

        m_HeroInfo = m_HeroManager.m_HeroHaveInfo.m_HeroInfo;
        m_InvenSlotInfo = invenSlotInfo;

        SetItemInstanceData(itemInstanceData);

        SetOnClickEquipInvenPanel();
    }

    void SetItemInstanceData(ItemInstanceData itemInstanceData)
    {
        m_ItemInstanceData = itemInstanceData;

        _iconShowUi.ShowIcon(m_ItemManager.GetItemData(itemInstanceData.m_ItemInstanceIndex).m_ElementIcon);

       

        CheckItemHaveIcon();


        m_ItemInstanceData.AddOnEquip(EquipItem);
        m_ItemInstanceData.AddOnUnEquip(UnEquipItem);
    }

    void CheckItemHaveIcon( )
    {
        Debug.Log("2");
        if (m_ItemInstanceData.m_EquippedHeroInstanceIndex!=-1)
        {
            HideInvenSlot();
        }
       
    }

    void ShowInvenSlot()
    {
        Debug.Log("Show!");
        gameObject.SetActive(true);
    }
    void HideInvenSlot()
    {
        gameObject.SetActive(false);
    }

    private void SetOnClickEquipInvenPanel()
    {
        _iconShowUi.AddAction(OnClick);
    }

    public void EquipItem()
    {
        HideInvenSlot();
    }
 

    public void UnEquipItem()
    {
        m_HeroInfo.UnEquipItem(m_InvenSlotInfo.m_CurrentSelectedItemIndex);
        ShowInvenSlot();
    }


    public void OnClick()
    {
        int CurrentHeroIndex = m_HeroInfo.m_CurrentHeroData.m_HeroIndex;
        int HeroSlot = m_InvenSlotInfo.m_CurrentSelectedItemIndex;

        if(m_HeroInfo.m_ItemSlots[HeroSlot].m_SlotItem!=null)
             m_HeroInfo.m_ItemSlots[HeroSlot].UnEquipItem();

        m_ItemInstanceData.Equip(CurrentHeroIndex, HeroSlot);
        m_HeroInfo.EquipItem(m_ItemInstanceData);
    }

    
}
