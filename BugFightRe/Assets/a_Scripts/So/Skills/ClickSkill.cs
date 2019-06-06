using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using DG.Tweening;
namespace Skills
{
    [CreateAssetMenu(menuName = "SkillData/ClickSkill")]
    public class ClickSkill : SkillBase
    {//공유불가능
        [SerializeField]
        SkillEvent[] _OnBeginClick;
        public SkillEvent[] m_OnBeginClickEventArrays { get => _OnBeginClick; }
        
        [Header("Default is Right")]
        [SerializeField]
        Vector3 _Offset;
        public Vector3 m_Offset { get => _Offset; }

        Vector3 GetOffset(UnitHero unitHero)
        {
            Vector3 newVector3 = m_Offset;
            if (!unitHero.myIsFacingRight)
            {
                newVector3.x *= -1;
                return newVector3;
            }

            return newVector3;
        }

        public void OnBeginClick(UnitHero unitHero)
        {
            Vector3 skillCoord;
            skillCoord = unitHero.myTrans.position + GetOffset(unitHero);
            SkillEvent.InitSkillChains(m_OnBeginClickEventArrays,skillCoord, unitHero);
            var FirstSeq = SkillEvent.SetTailSkillChains(m_OnBeginClickEventArrays);
            FirstSeq.Execute();
        }

        public override void CreateSkillButton(UnitHero unitHero ,GameObject targetObj)
        {
            var clickSkillBtn = targetObj.AddComponent<ClickSkillButton>();
            clickSkillBtn.SetInstance(unitHero,this);
            clickSkillBtn.SetClickSkill(OnBeginClick);
        }
    }

    [System.Serializable]
    public class EventVector3Hero : UnityEvent<Vector3, UnitHero> { }
  
    [System.Serializable]
    public class SkillEvent
    {
        public EventVector3Hero m_EventVector3Hero;
        public UnityEvent m_EventVoid;
        public float m_Delay;
        Sequence m_CastSeq;
        Vector3 m_TargetCoord;
       
        public Vector3 m_OffsetCoord;
        UnitHero m_Hero { get; set; }

        Vector3 GetOffset(UnitHero unitHero)
        {
            Vector3 newVector3 = m_OffsetCoord;
            if (!unitHero.myIsFacingRight)
            {

                newVector3.x *= -1;
                return newVector3;
            }

            return newVector3;
        }

        public void Init(Vector3 coord,UnitHero unitHero)
        {
            m_CastSeq = DOTween.Sequence();
            m_Hero = unitHero;
            m_TargetCoord = coord+ GetOffset(m_Hero);
        }

        public void AddTailSeqToPreSeq(SkillEvent preSeq)
        {
            preSeq.m_CastSeq.OnComplete(Execute);
        }

        public void Execute()
        {
            m_CastSeq.SetDelay(m_Delay)
                .AppendCallback(delegate { m_EventVoid?.Invoke(); })
                .AppendCallback(delegate { m_EventVector3Hero?.Invoke(m_TargetCoord, m_Hero); });
            Debug.Log("Ex");
        }

        public static SkillEvent SetTailSkillChains(SkillEvent[] skillEvents)
        {
            var FirstSkillSeq= skillEvents[0];

            for(int i=1; i<skillEvents.Length;i++)
            {
                skillEvents[i].AddTailSeqToPreSeq(FirstSkillSeq);
                FirstSkillSeq = skillEvents[i];
            }

            return skillEvents[0];
        }

        public static void InitSkillChains(SkillEvent[] skillEvents, Vector3 coord, UnitHero unitHero)
        {
            foreach (var AA in skillEvents)
            {
                AA.Init(coord, unitHero);
            }
        }
    }

}