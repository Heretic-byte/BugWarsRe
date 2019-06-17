using UnityEngine;
using UnityEngine.Events;
using MyMarmot.Tools;
public class ItemManager : Singleton<ItemManager>
{
    [SerializeField]
    ItemData[] _itemDatas;
    ItemData[] m_ItemDatas { get => _itemDatas; }

    public ItemData GetItemData(int index)
    {
        if (index == 0)//기본값
        {
            return null;
        }

        return m_ItemDatas[index];
    }


}
