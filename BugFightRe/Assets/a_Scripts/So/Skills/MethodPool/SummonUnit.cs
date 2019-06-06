using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMarmot.Tools;
using DG.Tweening;
[CreateAssetMenu(menuName = "SkillData/Method/SummonUnit")]
public class SummonUnit : ScriptableObject
{
    public GameObject m_SummonUnitPrefab;
    [Header("Not ForUnit")]
    public float m_EffectSpeed = 10f;
    
    public float m_EffectLifeTime = 0f;
    //유닛소환
    public void SummonUnitInCoord(Vector3 coord, UnitHero hero)
    {
        Unit unit = Instantiate(m_SummonUnitPrefab, coord,Quaternion.identity).GetComponent<Unit>();
        unit.SetDir(hero.GetMyDir);
        unit.GoRushBattleField(coord);
    }

    public void SummonUnitLine(Vector3 coord, UnitHero hero)
    {
        
        var Pos = StageMapManager.GetInstance.GetIndexLine(hero.GetCurrentLaneNumber - 1).myLeftLaneStartPos.GetSpawnPos();
        Unit unit = Instantiate(m_SummonUnitPrefab, Pos, Quaternion.identity).GetComponent<Unit>();
        unit.SetDir(hero.GetMyDir);
        unit.GoRushBattleField(Pos);
      
    }

    public void SummonEffectInCoord(Vector3 coord, UnitHero hero)
    {
        var Obj = Instantiate(m_SummonUnitPrefab, coord, Quaternion.identity);
        Destroy(Obj, m_EffectLifeTime);
        Sequence sequence = DOTween.Sequence();
        Debug.Log("ADS");
         sequence.SetLoops(-1).AppendCallback(delegate {  Obj.transform.Translate(hero.GetMyDir * Time.deltaTime * m_EffectSpeed);});
        Sequence Timer = DOTween.Sequence();
        Timer.SetDelay(m_EffectLifeTime).OnComplete(delegate { sequence.Kill(); });
    }
}
