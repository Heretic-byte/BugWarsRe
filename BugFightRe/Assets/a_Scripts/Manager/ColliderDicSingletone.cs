using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDicSingletone : Singleton<ColliderDicSingletone>
{
    private Dictionary<int, DamageAble> m_ColliderDamageAble = new Dictionary<int, DamageAble>();
    public Dictionary<int, DamageAble>  myColliderDamageAble { get => m_ColliderDamageAble; set => m_ColliderDamageAble = value; }


}
