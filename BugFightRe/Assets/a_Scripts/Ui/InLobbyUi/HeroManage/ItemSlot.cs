using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    public ItemInstanceData m_SlotItem { get; set; }
    [SerializeField]
    IconShowUi _iconShowUi;

    //터치시띄워줄화면

    [SerializeField]
    GameObject _itemDescPanelObj;
    [SerializeField]
    Text _itemStatBonusDesc;
    [SerializeField]
    Text _itemDesc;
    [SerializeField]
    [ColorUsage(true)]
    Color _color;
    Color m_BaseColor { get; set; }
    Image m_MyIconImage { get; set; }

    UnityAction m_OnEquip { get; set; }
    UnityAction m_OnUnEquip { get; set; }
    public int m_MySlotIndex { get; private set; }
     GameObject m_InvenPanel { get => _invenPanel;}

    //터치시 현재 아이템정보 띄워줘야함
    //또한 이는 인벤토리에서 그대로 쓸수있어야함
    [SerializeField]
    GameObject _invenPanel;


    Toggle toggle { get; set; }
    public void OnStart(int index)
    {
        m_MySlotIndex = index;

         toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(OnClickShowInfo);
        m_MyIconImage = GetComponent<Image>();
        m_BaseColor = m_MyIconImage.color;
    }

    public void EquipItem(ItemInstanceData itemData)
    {
        SetItemData(itemData);
        m_OnEquip?.Invoke();
    }

    public void UnEquipItem( )
    {
        m_OnUnEquip?.Invoke();
        SetNullItemData();
    }

    public void AddOnEquip(UnityAction unityAction)
    {
        m_OnEquip += unityAction;
    }


    public void AddOnUnEquip(UnityAction unityAction)
    {//아이템낄때마다 델리게이트바뀜
        m_OnUnEquip = unityAction;
    }

   

    void SetItemData(ItemInstanceData itemData)
    {
        m_SlotItem = itemData;
        _iconShowUi.ShowIcon(ItemManager.GetInstance.GetItemData(itemData.m_ItemInstanceIndex).m_ElementIcon);
        SetTextBonusDesc();
        SetTextItemDesc();
        AddOnUnEquip(itemData.UnEquip);
    }

    public void SetNullItemData()
    {
        m_SlotItem = null;
        _iconShowUi.HideIcon();
    }

    void OnClickShowInfo(bool onClick)
    {
        Debug.Log("CLICK:" + onClick);
        //일단
        //누루면 인벤패널이 켜짐
        //아이템을 가진채 다시누루면 아이템이빠짐

        if (m_SlotItem != null)
        {
            SetTextBonusDesc();
            SetTextItemDesc();
        }
        else
        {
            SetTextDescNull();
        }


        Debug.Log("55");
        ItemFocus(onClick);
    }

   void ItemFocus(bool onClick)
    {
        
        _itemDescPanelObj.SetActive(onClick);
        m_InvenPanel.SetActive(onClick);
        if (onClick)
        {
            m_MyIconImage.color = _color;
            Debug.Log("66");
        }
        else
        {
            m_MyIconImage.color = m_BaseColor;
            Debug.Log("77");
        }
    }

    void SetTextBonusDesc()
    {
        _itemStatBonusDesc.text = ItemManager.GetInstance.GetItemData(m_SlotItem.m_ItemInstanceIndex).m_ItemStatGive.GetStatString();
    }

    void SetTextItemDesc()
    {
        _itemDesc.text = ItemManager.GetInstance.GetItemData(m_SlotItem.m_ItemInstanceIndex).m_ItemStatGive.m_Description;
    }

    void SetTextDescNull()
    {
        _itemStatBonusDesc.text = "\r\n" + "아이템을 장착해주세요";
        _itemDesc.text = m_MySlotIndex + "번 슬롯 아이템없음";
    }


}
