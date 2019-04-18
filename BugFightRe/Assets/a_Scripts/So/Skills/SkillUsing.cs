using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SkillUsing : ScriptableObject
{


    public UnityAction CastSkill { get; set; }

}
//skillUsing:
//-드래그
//--사거리
//-차지
//---최대시간
//-클릭


//skillObjectCreate:
//>>풀링하기위해 미리만들것
//-내위치,상대위치,offset
//>>언제?

//skillLogic
//-내버프
//-상대디버프
//-범위공격
//-단일공격
//-유닛소환


//-소환
//--몇개
//-범위
//-단일
//--버프,디버프
//---능력치증가,특수능력치
//--데미지
//---회복
//--상태이상