using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyMarmot.Tools;
using DG.Tweening;

namespace Skills
{
    [CreateAssetMenu(menuName = "SkillData/Method/SpellUse")]
    public class SpellUse : ScriptableObject
    {
        [Header("스킬의 파워")]
        public float m_SpellPower = 300;
        [Header("버프,도트 스킬의 지속시간")]
        public float m_SpellDuration = 10f;
        [Header("버프,도트 스킬의 작동 텀")]
        public float m_SpellTick = 1f;
        [Header("대상 스킬 이펙트")]
        public GameObject m_TargetSkillEffect;
        [Header("캐스터 스킬 이펙트")]
        public GameObject m_CasterSkillEffect;
        Sequence m_Timer { get; set; }
        Sequence m_Seq { get; set; }

        public void MagicalDamageObj(DamageAble target,Unit caster)
        {
            var FinalPower = m_SpellPower;
            var per = FinalPower / 100f;
            ShowCasterEffect(target, caster);
            ShowTargetEffect(target, caster);
            FinalPower += (per* caster.GetSpellDamagePercent());
            target.TakeMagicalDamage(FinalPower, caster);
        }

        public void PhysicalDamageObj(DamageAble target, Unit caster)
        {
            var FinalPower = m_SpellPower;
            var per = FinalPower / 100f;
            ShowCasterEffect(target, caster);
            ShowTargetEffect(target, caster);
            FinalPower += (per * caster.GetSpellDamagePercent());
            target.TakePhysicalDamage(FinalPower, caster);
        }

        void MagicalDamageObjNoCasterEffect(DamageAble target, Unit caster)
        {
            var FinalPower = m_SpellPower;
            var per = FinalPower / 100f;

            ShowTargetEffect(target, caster);
            FinalPower += (per * caster.GetSpellDamagePercent());
            target.TakeMagicalDamage(FinalPower, caster);
        }

        void PhysicalDamageObjNoCasterEffect(DamageAble target, Unit caster)
        {
            var FinalPower = m_SpellPower;
            var per = FinalPower / 100f;

            ShowTargetEffect(target, caster);
            FinalPower += (per * caster.GetSpellDamagePercent());
            target.TakePhysicalDamage(FinalPower, caster);
        }

        public void PhysicalOnHitDamageObj(DamageAble target, Unit caster)
        {//이경우 스펠파워는 퍼센트
            var FinalPower = m_SpellPower * caster.GetAttackDamage();
            var per = FinalPower / 100f;
            ShowCasterEffect(target, caster);
            ShowTargetEffect(target, caster);
            FinalPower += (per * caster.GetSpellDamagePercent());
            target.TakePhysicalDamage(FinalPower, caster);
        }

        public void HealObj(DamageAble target, Unit caster)
        {
            var FinalPower = m_SpellPower;
            var per = FinalPower / 100f;
            ShowCasterEffect(target, caster);
            ShowTargetEffect(target, caster);
            FinalPower += (per * caster.GetSpellDamagePercent());
            target.GetHeal(FinalPower);
        }

        public void MagicalDotDamageObj(DamageAble target, Unit caster)
        {
            ShowCasterEffect(target, caster);
            m_Seq = DOTween.Sequence();
            m_Seq.SetLoops(-1).AppendInterval(m_SpellTick).AppendCallback(delegate { MagicalDamageObjNoCasterEffect(target, caster); });
            SeqDeadTimer(m_Seq);
        }

        public void PhysicalDotDamageObj(DamageAble target, Unit caster)
        {
            ShowCasterEffect(target, caster);
            m_Seq = DOTween.Sequence();
            m_Seq.SetLoops(-1).AppendInterval(m_SpellTick).AppendCallback(delegate { PhysicalDamageObjNoCasterEffect(target, caster); });
            SeqDeadTimer(m_Seq);
        }

        void SeqDeadTimer(Sequence sequence)
        {
            m_Timer = DOTween.Sequence();
            m_Timer.SetDelay(m_SpellDuration).OnComplete(delegate { sequence.Kill(); });
        }

        void ShowTargetEffect(DamageAble target, Unit caster)
        {
            if(m_TargetSkillEffect!=null)
            {
                var AA= Instantiate(m_TargetSkillEffect, target.myTrans.position, Quaternion.identity);
                Destroy(AA, 2f);
            }
        }

        void ShowCasterEffect(DamageAble target, Unit caster)
        {
            if (m_CasterSkillEffect != null)
            {
                var BB = Instantiate(m_CasterSkillEffect, caster.myTrans.position, Quaternion.identity);
                Destroy(BB, 2f);
            }
        }
    }
}
