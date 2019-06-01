using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDicSingletone : Singleton<ColliderDicSingletone>
{
    private Dictionary<int, DamageAble> m_ColliderDamageAble = new Dictionary<int, DamageAble>();
    private Dictionary<int, DamageAble>  myColliderDamageAble { get => m_ColliderDamageAble; set => m_ColliderDamageAble = value; }


    public DamageAble GetDamageAble(Collider2D colliderHash)
    {
        return myColliderDamageAble[colliderHash.GetInstanceID()];
    }

    public DamageAble GetDamageAble(int colliderHashKey)
    {
        return myColliderDamageAble[colliderHashKey];
    }

    public DamageAble[] GetDamageAbleArry(Collider2D[] collider2Ds)
    {
        DamageAble[] damageAbles = new DamageAble[collider2Ds.Length];

        for(int i=0; i< damageAbles.Length;i++)
        {
            damageAbles[i] = GetDamageAble(collider2Ds[i].GetInstanceID());
        }


        return damageAbles;
    }

    public void AddDamageAble(int collHashKey,DamageAble value)
    {
        myColliderDamageAble.Add(collHashKey, value);
    }

    public void RemoveDamageAble(int collHashKey)
    {
        myColliderDamageAble.Remove(collHashKey);
    }
}
