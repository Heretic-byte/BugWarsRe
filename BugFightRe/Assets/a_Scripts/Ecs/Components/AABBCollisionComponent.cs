using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[System.Serializable]
public struct AABB : IComponentData
{
    public float2 min;
    public float2 max;

}


public class AABBCollisionComponent : ComponentDataProxy<AABB>
{

}
