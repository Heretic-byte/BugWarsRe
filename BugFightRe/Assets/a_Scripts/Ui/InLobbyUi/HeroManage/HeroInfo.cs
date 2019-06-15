using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class HeroInfo : MonoBehaviour
{
    [SerializeField]
    Text _heroName;
    Text m_HeroName { get => _heroName; }

    [SerializeField]
    ItemSlot[] _itemSlots;
    ItemSlot[] m_ItemSlots { get => _itemSlots; }


    public void SetHeroName(string name)
    {
        m_HeroName.text = name;
    }

    public void SetItemData(ItemSaveData itemDatas)
    {
        ItemManager itemManager = ItemManager.GetInstance;
        for (int i = 0; i < itemDatas.m_ItemIndex.Length; i++)
        {
            var result = itemManager.GetItemData(itemDatas.m_ItemIndex[i]);
            if (result != null)
            {
                m_ItemSlots[i].SetItemData(result);
            }
        }
    }
    public void SetNullHeroName()
    {
        m_HeroName.text = null;
    }

    public void SetNullItemData()
    {
        for (int i = 0; i < m_ItemSlots.Length; i++)
        {
            m_ItemSlots[i].SetNullItemData();
        }
    }
}
