using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMarmot.Tools;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    //진화석 강화석 등 인벤따로 만들자
    [SerializeField]
    List<ItemInstanceData> _itemInstanceDatas;
    public List<ItemInstanceData> m_ItemInstanceDatas { get => _itemInstanceDatas; private set=>_itemInstanceDatas=value; }

    public const string PlayerItemSaveFileName = "PlayerEquipmentItem";

    [SerializeField]
    int _maxInven=64;
    public int m_MaxInven { get => _maxInven; private set => _maxInven = value; }

    ItemManager m_ItemManage { get; set; }
    [SerializeField]
    GameObject _invenSlotUiPrefab;
    public GameObject m_InvenSlotUiPrefab { get => _invenSlotUiPrefab; }

    [SerializeField]
    Transform _invenSlotUiGridParent;
    public Transform m_InvenSlotUiGridParent { get => _invenSlotUiGridParent; }

    [SerializeField]
    UnityEvent _onItemChanged;
    public UnityEvent m_OnItemChanged { get => _onItemChanged; }

    [SerializeField]
    InvenSlotInfo _invenSlotInfo;
    public InvenSlotInfo m_InvenSlotInfo { get => _invenSlotInfo;}

    void Start()
    {
        m_ItemManage = ItemManager.GetInstance;

        LoadItemInven();

        SetItemInven();
    }


    [ContextMenu("SaveItem")]
    public void SaveItemInven()
    {
        foreach(var AA in m_ItemInstanceDatas)
        {
            AA.SetNullDelegate();
        }

        SaveLoadManager.Save(m_ItemInstanceDatas, PlayerItemSaveFileName);
    }

    [ContextMenu("LoadItem")]
    public void LoadItemInven()
    {
        var loadedItemData = SaveLoadManager.Load(PlayerItemSaveFileName) as List<ItemInstanceData>;

        if (loadedItemData != null)
        {
            m_ItemInstanceDatas = loadedItemData;
        }

    }

    public void AddMaxInven(int addAmount)
    {
        m_MaxInven += addAmount;
    }

    public bool AddItem(int itemIndex)
    {
        ItemInstanceData itemInstanceData = new ItemInstanceData();
        itemInstanceData.SetItemInstanceIndex(itemIndex);

        if(m_ItemInstanceDatas.Count<=m_MaxInven)
        {
            m_ItemInstanceDatas.Add(itemInstanceData);
            return true;
        }

        return false;
    }

    public bool RemoveItem(ItemInstanceData itemInstanceData)
    {
       return m_ItemInstanceDatas.Remove(itemInstanceData);
    }

    InvenSlotUi CreateInvenSlot()
    {
        var invenSlotObj = Instantiate(m_InvenSlotUiPrefab, m_InvenSlotUiGridParent).GetComponent<InvenSlotUi>();
        return invenSlotObj;
    }

    void SetItemInven()
    {
        for (int i = 0; i < m_ItemInstanceDatas.Count; i++)
        {
            InvenSlotUi Obj = CreateInvenSlot();
            ItemData itemData = m_ItemManage.GetItemData(m_ItemInstanceDatas[i].m_ItemInstanceIndex);

            if (itemData != null)
            {
                Obj.SetItemData(m_ItemInstanceDatas[i], m_InvenSlotInfo);
            }
        }
        m_OnItemChanged?.Invoke();
    }

}
