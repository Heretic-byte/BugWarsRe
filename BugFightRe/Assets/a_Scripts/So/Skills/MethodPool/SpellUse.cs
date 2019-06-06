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
        [Header("대상 스킬 이펙트 오프셋")]
        public Vector3 m_TargetEffectOffset;
        [Header("캐스터 스킬 이펙트")]
        public GameObject m_CasterSkillEffect;
        [Header("캐스터 스킬 이펙트 오프셋")]
        public Vector3 m_CasterEffectOffset;
        Sequence m_Timer { get; set; }
        Sequence m_Seq { get; set; }

        Vector3 GetCasterOffset(DamageAble caster)
        {
            Vector3 newVector3 = m_CasterEffectOffset;
            if (!caster.myIsFacingRight)
            {
                newVector3.x *= -1;
                return newVector3;
            }

            return newVector3;
        }
        Vector3 GetTargetOffset(DamageAble target)
        {
            Vector3 newVector3 = m_TargetEffectOffset;
            if (!target.myIsFacingRight)
            {
                newVector3.x *= -1;
                return newVector3;
            }

            return newVector3;
        }


        public void MagicalDamageObj(DamageAble target,Unit caster)
        {
            var FinalPower = m_SpellPower;
            var per = FinalPower / 100f;
            ShowCasterEffect( caster);
            ShowTargetEffect(target);
            FinalPower += (per* caster.GetSpellDamagePercent());
            target.TakeMagicalDamage(FinalPower, caster);
        }

        public void PhysicalDamageObj(DamageAble target, Unit caster)
        {
            var FinalPower = m_SpellPower;
            var per = FinalPower / 100f;
            ShowCasterEffect( caster);
            ShowTargetEffect(target);
            FinalPower += (per * caster.GetSpellDamagePercent());
            target.TakePhysicalDamage(FinalPower, caster);
        }

        void MagicalDamageObjNoCasterEffect(DamageAble target, Unit caster)
        {
            var FinalPower = m_SpellPower;
            var per = FinalPower / 100f;

            ShowTargetEffect( caster);
            FinalPower += (per * caster.GetSpellDamagePercent());
            target.TakeMagicalDamage(FinalPower, caster);
        }

        void PhysicalDamageObjNoCasterEffect(DamageAble target, Unit caster)
        {
            var FinalPower = m_SpellPower;
            var per = FinalPower / 100f;

            ShowTargetEffect( caster);
            FinalPower += (per * caster.GetSpellDamagePercent());
            target.TakePhysicalDamage(FinalPower, caster);
        }

        public void PhysicalOnHitDamageObj(DamageAble target, Unit caster)
        {//이경우 스펠파워는 퍼센트
            var FinalPower = m_SpellPower * caster.GetAttackDamage();
            var per = FinalPower / 100f;
            ShowCasterEffect( caster);
            ShowTargetEffect(target);
            FinalPower += (per * caster.GetSpellDamagePercent());
            target.TakePhysicalDamage(FinalPower, caster);
        }

        public void HealObj(DamageAble target, Unit caster)
        {
            var FinalPower = m_SpellPower;
            var per = FinalPower / 100f;
            ShowCasterEffect( caster);
            ShowTargetEffect(target);
            FinalPower += (per * caster.GetSpellDamagePercent());
            target.GetHeal(FinalPower);
        }
        public void HealObjNoCasterEffect(DamageAble target, Unit caster)
        {
            var FinalPower = m_SpellPower;
            var per = FinalPower / 100f;
            ShowTargetEffect( caster);
            FinalPower += (per * caster.GetSpellDamagePercent());
            target.GetHeal(FinalPower);
        }

        public void DotMagicalDamageObj(DamageAble target, Unit caster)
        {
            ShowCasterEffect( caster);
            m_Seq = DOTween.Sequence();
            m_Seq.SetLoops(-1).AppendInterval(m_SpellTick).AppendCallback(delegate { MagicalDamageObjNoCasterEffect(target, caster); });
            SeqDeadTimer(m_Seq);
        }

        public void DotPhysicalDamageObj(DamageAble target, Unit caster)
        {
            ShowCasterEffect( caster);
            m_Seq = DOTween.Sequence();
            m_Seq.SetLoops(-1).AppendInterval(m_SpellTick).AppendCallback(delegate { PhysicalDamageObjNoCasterEffect(target, caster); });
            SeqDeadTimer(m_Seq);
        }

        public void DotHealObj(DamageAble target, Unit caster)
        {
            ShowCasterEffect( caster);
            m_Seq = DOTween.Sequence();
            m_Seq.SetLoops(-1).AppendInterval(m_SpellTick).AppendCallback(delegate { HealObjNoCasterEffect(target, caster); });
            SeqDeadTimer(m_Seq);
        }

        Sequence SeqDeadTimer(Sequence sequence)
        {
            m_Timer = DOTween.Sequence();
          return  m_Timer.SetDelay(m_SpellDuration).OnComplete(delegate { sequence.Kill(); });
        }

        void ShowTargetEffect(DamageAble target)
        {
            if(m_TargetSkillEffect!=null)
            {
                var AA= Instantiate(m_TargetSkillEffect, target.myTrans.position+GetTargetOffset(target), Quaternion.identity);
                Destroy(AA, 2f);
            }
        }

        void ShowCasterEffect( Unit caster)
        {
            if (m_CasterSkillEffect != null)
            {
                var BB = Instantiate(m_CasterSkillEffect, caster.myTrans.position+GetCasterOffset(caster), Quaternion.identity);
                Destroy(BB, 2f);
            }
        }
        //버프있어야함
        void BuffDamage(DamageAble target, Unit caster)
        {
            ShowCasterEffect( caster);
            ShowTargetEffect(target);
            m_Seq = DOTween.Sequence();
            target.myRealStat.BonusPlusDamage(m_SpellPower);
            SeqDeadTimer(m_Seq).OnComplete(delegate { target.myRealStat.BonusMinusDamage(m_SpellPower); });
        }

        void BuffMoveSpeed(DamageAble target, Unit caster)
        {
            ShowCasterEffect(caster);
            ShowTargetEffect(target);
            m_Seq = DOTween.Sequence();
            target.myRealStat.BonusPlusMoveSpeed(m_SpellPower);
            SeqDeadTimer(m_Seq).OnComplete(delegate { target.myRealStat.BonusMinusMoveSpeed(m_SpellPower); });
        }

        void BuffAttackSpeed(DamageAble target, Unit caster)
        {
            ShowCasterEffect(caster);
            ShowTargetEffect(target);
            m_Seq = DOTween.Sequence();
            target.myRealStat.BonusPlusAttackSpeed(m_SpellPower);
            SeqDeadTimer(m_Seq).OnComplete(delegate { target.myRealStat.BonusMinusAttackSpeed(m_SpellPower); });
        }

        void BuffArmor(DamageAble target, Unit caster)
        {
            ShowCasterEffect(caster);
            ShowTargetEffect(target);
            m_Seq = DOTween.Sequence();
            target.myRealStat.BonusPlusArmor(m_SpellPower);
            SeqDeadTimer(m_Seq).OnComplete(delegate { target.myRealStat.BonusMinusArmor(m_SpellPower); });
        }
        //debuff
        void DeBuffDamage(DamageAble target, Unit caster)
        {
            ShowCasterEffect(caster);
            ShowTargetEffect(target);
            m_Seq = DOTween.Sequence();
            target.myRealStat.BonusMinusDamage(m_SpellPower);
            SeqDeadTimer(m_Seq).OnComplete(delegate { target.myRealStat.BonusPlusDamage(m_SpellPower); });
        }

        void DeBuffMoveSpeed(DamageAble target, Unit caster)
        {
            ShowCasterEffect(caster);
            ShowTargetEffect(target);
            m_Seq = DOTween.Sequence();
            target.myRealStat.BonusMinusMoveSpeed(m_SpellPower);
            SeqDeadTimer(m_Seq).OnComplete(delegate { target.myRealStat.BonusPlusMoveSpeed(m_SpellPower); });
        }

        void DeBuffAttackSpeed(DamageAble target, Unit caster)
        {
            ShowCasterEffect(caster);
            ShowTargetEffect(target);
            m_Seq = DOTween.Sequence();
            target.myRealStat.BonusMinusAttackSpeed(m_SpellPower);
            SeqDeadTimer(m_Seq).OnComplete(delegate { target.myRealStat.BonusPlusAttackSpeed(m_SpellPower); });
        }

        void DeBuffArmor(DamageAble target, Unit caster)
        {
            ShowCasterEffect(caster);
            ShowTargetEffect(target);
            m_Seq = DOTween.Sequence();
            target.myRealStat.BonusMinusArmor(m_SpellPower);
            SeqDeadTimer(m_Seq).OnComplete(delegate { target.myRealStat.BonusPlusArmor(m_SpellPower); });
        }
    }
}
