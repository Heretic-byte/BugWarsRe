using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/ItemData")]

public class ItemData : StageDataElement
{
    public enum ItemType
    {
        E_ATTACK,
        E_DEFENSE,
        E_UTILITY
    }
    public ItemType m_ItemType;

    [Header("BaseStat")]
    public StatDataBase m_ItemStatGive;

    public void SetStatBonus(StatDataBase.StatValue heroStatInstance)
    {
        heroStatInstance.SetModifyStat(m_ItemStatGive);
    }


    //아이템자체의 부가효과는 어떻게하냐
    //퍼센트수치는?



}
