using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDebg : MonoBehaviour
{

    public HeroInfo HeroInfo;

    public void ASDSDA()
    {

        if (HeroInfo.m_ItemSlots[0].m_SlotItem.m_OnUnEquip == null)
        {
            Debug.Log("NULL");
        }
        else
        {
            Debug.Log("OK");
        }

    }

    public void TIck()
    {

        //HeroInfo.m_ItemSlots[0].UnEquipItem();
    }
}
