using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Skills
{
    [CreateAssetMenu(menuName = "SkillData/ClickSkill")]
    public class ClickSkill : SkillBase
    {
        [SerializeField]
        SkillEffectEvent _OnBeginClick;
        [SerializeField]
        SkillEffectEvent _OnEndClick;

        public SkillEffectEvent m_OnBeginClick { get => _OnBeginClick; }
        public SkillEffectEvent m_OnEndClick { get => _OnEndClick; }


        public void OnBeginClick()
        {
            m_OnBeginClick?.Invoke();
            //스킬을 어디에 뿌릴지는
            //장착한 영웅만 알고있음

            //오프셋 개념을 설정해야함
            //드래그 또한 두가지이다.
            //라인 선택인지 실제 위치 지점인지

            //클릭또한 영웅주변 좌표 가능
            //혹은 영웅의 라인
            //혹은 영웅 자신
            //즉 클릭은 영웅을 기준으로 가짐
        }
    }
}