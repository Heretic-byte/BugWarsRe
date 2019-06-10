using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HeroData 
{
    //저장되는 실제 영웅데이터
    public int m_HeroLevel;
    public HeroType m_HeroType;
    public int m_HeroRank;
    public int m_HeroIndex;

 
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
