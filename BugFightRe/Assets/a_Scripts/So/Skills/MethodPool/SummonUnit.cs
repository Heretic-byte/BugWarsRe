using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMarmot.Tools;
[CreateAssetMenu(menuName = "SkillData/Method/SummonUnit")]
public class SummonUnit : ScriptableObject
{
    public GameObject m_SummonUnitPrefab;
    //유닛소환
    public void SummonUnitInCoord(Vector3 coord, UnitHero hero)
    {
        Unit unit = Instantiate(m_SummonUnitPrefab, coord,Quaternion.identity).GetComponent<Unit>();
        unit.SetDir(hero.GetMyDir);
        unit.GoRushBattleField(coord);
    }

    public void SummonUnitLine(Vector3 coord, UnitHero hero)
    {
        var AA = MethodTimer.StartTimer();
        var Pos = StageMapManager.GetInstance.GetIndexLine(hero.GetCurrentLaneNumber - 1).myLeftLaneStartPos.GetSpawnPos();
        Unit unit = Instantiate(m_SummonUnitPrefab, Pos, Quaternion.identity).GetComponent<Unit>();
        unit.SetDir(hero.GetMyDir);
        unit.GoRushBattleField(Pos);
        MethodTimer.StopTimer(AA);
    }
}
