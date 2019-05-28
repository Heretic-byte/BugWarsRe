using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Skills
{
    [CreateAssetMenu(menuName = "SkillData/DragSkill")]
    public class DragSkill : SkillBase
    {
        [SerializeField]
        UnityEvent _OnBeginDrag;
        [SerializeField]
        UnityEvent _OnMoveDrag;
        [SerializeField]
        UnityEvent _OnEndDrag;

        public UnityEvent m_OnBeginDrag { get => _OnBeginDrag; }
        public UnityEvent m_OnMoveDrag { get => _OnMoveDrag; }
        public UnityEvent m_OnEndDrag { get => _OnEndDrag; }


    }
}