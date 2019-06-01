using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMapManager : Singleton<StageMapManager>
{
    [SerializeField]
    private LaneRoad[] _stageLaneRoads;

    public LaneRoad[] myStageLaneRoads { get => _stageLaneRoads; set => _stageLaneRoads = value; }
    
    private Dictionary<int, LaneRoad> _laneAndCollDic = new Dictionary<int, LaneRoad>();
    public Dictionary<int, LaneRoad> myLaneAndCollDic { get => _laneAndCollDic; set => _laneAndCollDic = value; }

    private void Start()
    {
        SetStageCollDic();
    }

    private void SetStageCollDic()
    {
       foreach(var laneRoad in myStageLaneRoads)
        {
            myLaneAndCollDic.Add(laneRoad.myColl2D.GetInstanceID(), laneRoad);
        }

    }
    public LaneRoad GetIndexLine(int index)
    {
        return myStageLaneRoads[index]; 
    }
}
