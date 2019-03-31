using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ExpTable", menuName = "LevelPerExpData")]
public class CharacterExpTable : ScriptableObject
{

    public LevelData[] levelDatas;


}
[System.Serializable]
public class LevelData
{
    public int m_LevelPerMaxExp;
}
//레벨
//  최대경험치가 있고
