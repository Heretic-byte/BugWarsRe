using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IlinePosHolder 
{

    Vector3[] myPosArray { get; set; }

    Vector3 GetSpawnPos();
    Vector3 GetSpawnPos(int index);

}
