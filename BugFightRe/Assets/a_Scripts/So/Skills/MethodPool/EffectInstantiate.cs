using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Skills
{
    [CreateAssetMenu(menuName = "SkillData/Method/Effect")]
    public class EffectInstantiate : ScriptableObject
    {
        public GameObject[] m_EffectPrefab;

        public void CreateEffect(SkillEffectClass skillEffect)
        {
            Instantiate(skillEffect.prefab, skillEffect.createCoord, Quaternion.identity);
        }
        
    }

    [System.Serializable]
    public class SkillEffectEvent:UnityEvent<SkillEffectClass> { }

    public struct SkillEffectClass
    {
        public Vector3 createCoord;
        public GameObject prefab;
    }

}