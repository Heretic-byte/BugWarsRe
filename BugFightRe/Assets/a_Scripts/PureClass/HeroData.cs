using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HeroData 
{
    //저장되는 실제 영웅데이터
    public int m_HeroLevel;
    public int m_HeroRank;
    public int m_HeroIndex;
    public bool m_Ihave;
    public ItemSaveData m_ItemSaveData;//Maxis 6
 
    public string GetLevelText()
    {
        return "Lv" + m_HeroLevel.ToString();
    }

    public enum HeroType
    {
        Dyna,
        Stag,
        Wasp,
        Moth
    }
}
