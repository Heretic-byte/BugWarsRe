using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyMarmot.Tools;
namespace Skills
{
    [CreateAssetMenu(menuName = "SkillData/Method/SpellCast")]
    public class SpellCast : ScriptableObject
    {
        [Header("원 충돌 반지름")]
        public float m_Radius = 1;
        [Header("사거리")]
        public float m_Dist = 1;
        [Header("캐스팅된 유닛 이벤트")]
        public EventDamageable m_EventDamageable;
        [Header("레이 캐스트 레이어 마스크")]
        public LayerMask m_WhatToHit;
        RaycastHit2D[] m_HitArray =new RaycastHit2D[100];
        //데미지에이블에게 데미지 주거나, 회복시키거나, 버프걸거나,이펙트도 추가

        //이벤트
        //원형캐스팅
        public void CastCircle(Vector3 coord, UnitHero hero)
        {
            var Hitten = Physics2D.OverlapCircleAll(coord, m_Radius,m_WhatToHit);

            var Damageables = ColliderDicSingletone.GetInstance.GetDamageAbleArry(Hitten);

            foreach (var AA in Damageables)
            {
                m_EventDamageable?.Invoke(AA, hero);
            }
        }
        //직선캐스팅
        public void CastLine(Vector3 coord, UnitHero hero)
        {
            var Hitten = Physics2D.Raycast(coord, hero.GetMyDir, m_Dist,m_WhatToHit);
            var Damageable = ColliderDicSingletone.GetInstance.GetDamageAble(Hitten.collider);
            m_EventDamageable?.Invoke(Damageable, hero);
        }

        public void CastAllLine(Vector3 coord, UnitHero hero)
        {
          var iter= Physics2D.RaycastNonAlloc(coord, hero.GetMyDir, m_HitArray,m_Dist,m_WhatToHit);

            List<RaycastHit2D> SortedHittenList = new List<RaycastHit2D>();
            SortedHittenList.Capacity = iter;
            for (int i=0; i< iter;i++)
            {
                SortedHittenList.Add( m_HitArray[i]);
            }

            SortedHittenList.Sort(new RayCastXCompare());

            var RayArray = SortedHittenList.ToArray();

            Collider2D[] collider2Ds = new Collider2D[RayArray.Length];

            for(int i=0; i< iter; i++)
            {
                collider2Ds[i] = RayArray[i].collider;
            }

            var Damageables = ColliderDicSingletone.GetInstance.GetDamageAbleArry(collider2Ds);

            foreach (var AA in Damageables)
            {
                m_EventDamageable?.Invoke(AA,hero);
            }

        }

        public class RayCastXCompare : IComparer<RaycastHit2D>
        {
            // Compares by Height, Length, and Width.
            public int Compare(RaycastHit2D x, RaycastHit2D y)
            {
                if (x.point.x > y.point.x)
                {
                    return 1;
                }
                if (x.point.x < y.point.x)
                {
                    return -1;
                }

                return 0;
            }
        }

    }

    [System.Serializable]
    public class EventDamageable : UnityEvent<DamageAble,Unit> { }
}
