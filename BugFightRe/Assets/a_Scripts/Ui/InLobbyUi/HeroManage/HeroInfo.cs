using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using HeroSelectUI;
using UnityEngine.Events;
public class HeroInfo : MonoBehaviour
{
    [SerializeField]
    Text _heroName;
    Text m_HeroName { get => _heroName; }

    [SerializeField]
    ItemSlot[] _itemSlots;
    public ItemSlot[] m_ItemSlots { get => _itemSlots; }

    public HeroData m_CurrentHeroData { get; set; }
    StatDataBase.StatValue m_MyHeroStatValue;//output heroStat

    HeroManager m_HeroManage { get; set; }
    ItemManager m_ItemManager { get; set; }

    UnityAction<StatDataBase.StatValue> m_OnEquip { get; set; }
    UnityAction<StatDataBase.StatValue> m_OnUnEquip { get; set; }
    public void OnStart()
    {
        m_HeroManage = HeroManager.GetInstance;
        m_ItemManager = ItemManager.GetInstance;

        for(int i=0; i<m_ItemSlots.Length;i++)
        {
            m_ItemSlots[i].OnStart(i);
        }
    }

    public void SetHeroData(StatDataBase.StatValue statValue, HeroData heroData)
    {
        m_CurrentHeroData = heroData;
        SetHeroName(m_HeroManage.GetHeroName(heroData.m_HeroIndex));
        m_MyHeroStatValue = statValue;

        ItemChangeUpdate();
       
    }

    void ItemChangeUpdate()
    {
        for (int i = 0; i < 6; i++)
        {
            if (m_CurrentHeroData.m_ItemSaveData[i]!=null && m_CurrentHeroData.m_ItemSaveData[i].m_ItemSlot != -1 )
                EquipItem(m_CurrentHeroData.m_ItemSaveData[i]);
           
        }
        m_OnEquip?.Invoke(m_MyHeroStatValue);
    }

    void SetHeroName(string name)
    {
        m_HeroName.text = name;
    }

    public void OnEquipCallback(UnityAction<StatDataBase.StatValue> unityAction)
    {
        m_OnEquip += unityAction;
    }
    public void OnUnEquipCallback(UnityAction<StatDataBase.StatValue> unityAction)
    {
        m_OnUnEquip += unityAction;
    }

    
   
    public void EquipItem(ItemInstanceData itemInstanceData)//6개중 어디인지
    {
        ItemData result = m_ItemManager.GetItemData(itemInstanceData.m_ItemInstanceIndex);

        if (result == null)
        {
            Debug.Log("Equip해당아이템 업어");
            return;
        }

        Debug.Log("아이템슬롯:"+itemInstanceData.m_ItemSlot);
        Debug.Log("아이템이름:"+result.m_ElementName);


        m_CurrentHeroData.m_ItemSaveData[itemInstanceData.m_ItemSlot] = itemInstanceData;

        result.EquipStatBonus(ref m_MyHeroStatValue);

        EquipInSlot(itemInstanceData);

        m_OnEquip?.Invoke(m_MyHeroStatValue);

        //save
    }

    void EquipInSlot(ItemInstanceData itemData)
    {
        m_ItemSlots[itemData.m_ItemSlot].EquipItem(itemData);
    }

    public void UnEquipItem(int itemSlotIndex)//6개중 어디인지
    {

        var result = m_ItemManager.GetItemData(m_CurrentHeroData.m_ItemSaveData[itemSlotIndex].m_ItemInstanceIndex);
        Debug.Log("4");
        if (result == null)
        {
            Debug.Log("UnEquip해당아이템 업어");
            return;
        }

        m_CurrentHeroData.m_ItemSaveData[itemSlotIndex] = null;

        result.UnEquipStatBonus(ref m_MyHeroStatValue);

         m_OnUnEquip?.Invoke(m_MyHeroStatValue);
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
