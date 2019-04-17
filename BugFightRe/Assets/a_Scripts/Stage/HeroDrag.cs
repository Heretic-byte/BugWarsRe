using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HeroDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField]
    private float GroundPosZ = 20f;
    [SerializeField]
    private GameObject _dragIndicatorP;
    public GameObject myDragIndicator { get; set; }

    [SerializeField]
    private LayerMask _whatToHit;
    public LayerMask myWhatToHit { get => _whatToHit; set => _whatToHit = value; }

    [SerializeField]
    private int _recallManaCost;
    public int myRecallManaCost { get => _recallManaCost;  }

    public Image myDragButtonIcon { get; set; }
    private GameObject _myHeroObj { get; set; }
    private UnitHero _myHeroUnit { get; set; }
    private LaneRoad _myPreLaneRoad { get; set; }
    private Transform _myDragIndicatorTrans { get; set; }
   
    [SerializeField]
    private float _recallDelay = 3f;
    public float myRecallDelay { get => _recallDelay; }
    [SerializeField]
    private float _healTickValue = 200;
    public float myHealTickValue { get => _healTickValue;  }

    Sequence _myHeroHealSeq { get; set; }
  
    public void Init()
    {
        myDragButtonIcon = GetComponent<Image>();
        myDragIndicator = Instantiate(_dragIndicatorP, Vector3.zero, Quaternion.identity);
        myDragIndicator.SetActive(false);      
    }

    public void SetHero(GameObject heroObj, UnitHero heroUnit)
    {
        Init();
        _myHeroObj = heroObj;
        _myHeroUnit = heroUnit;
        myDragButtonIcon.sprite = heroUnit.myPortrait;
        //ui표시 등등
    }

    public void GoBattleField()
    {
        var hittenLog = GetCollRaycast();

        if (hittenLog == null)
        {
            return;
        }
        Debug.Log("GOBATTLE");

        var summonNexus = StageMapManager.myInstance.myLaneAndCollDic[hittenLog.GetInstanceID()].myLeftNexus;

        var tempV3 = summonNexus.mySpawnPointArray[0];
        tempV3.z -= 0.1f;

        _myHeroHealSeq?.Kill();

        _myHeroUnit.GoRushBattleField(summonNexus.myTrans.position + tempV3);
    }

    public RaycastHit2D RaycastGround()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 100f, myWhatToHit);

        return hit;
    }

    public Collider2D GetCollRaycast()
    {
        var hitten = RaycastGround();

        return hitten.collider;
    }

    bool CheckCanHeroGoBattle()
    {
        if (_myHeroUnit._myIsHeroInLine)
        {
            return false;
        }

        if(_myHeroUnit.myIsDead)
        {
            return false;
        }

        return true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!CheckCanHeroGoBattle())
        {
            return;
        }

        var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myDragIndicator.transform.position = MousePos;
        myDragIndicator.SetActive(true);
        _myDragIndicatorTrans = myDragIndicator.transform;
        SetDraggedPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!CheckCanHeroGoBattle())
        {
            return;
        }

        SetDraggedPosition();

        var coll = GetCollRaycast();
        if (coll != null)
        {   
            HidePreLaneRoadFeedBack();
            _myPreLaneRoad = StageMapManager.myInstance.myLaneAndCollDic[coll.GetInstanceID()];
            _myPreLaneRoad.ShowFeedBackLaneRoad();
        }
        else
        {
            HidePreLaneRoadFeedBack();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!CheckCanHeroGoBattle())
        {
            return;
        }

        HidePreLaneRoadFeedBack();
        myDragIndicator.SetActive(false);
        GoBattleField();
    }

    private void SetDraggedPosition( )
    {
        var rt = _myDragIndicatorTrans;
        var posTemp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posTemp.z = 0f;
        rt.position = posTemp;
    }

    void HidePreLaneRoadFeedBack()
    {
        if (_myPreLaneRoad != null)
        {
            _myPreLaneRoad.HideFeedBackLaneRoad();
        }
    }
    public void HeroRecall()
    {
        if (ManaManager.myInstance.SubstractManaFromPlayer(myRecallManaCost))
        {        
            _myHeroUnit.GoBackToTemple(myRecallDelay).onComplete+= TempleHeroHealSequence;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       if(!_myHeroUnit.myIsDead && _myHeroUnit._myIsHeroInLine)
        {
            //귀환시퀀스
            print("RecallClicked");
            HeroRecall();
            //완료후 회복시퀀스
        }
    }

    void TempleHeroHealSequence()
    {
        _myHeroHealSeq = DOTween.Sequence();
        _myHeroHealSeq.SetLoops(-1).PrependInterval(0.5f).PrependCallback(HealHero);
    }
    void HealHero()
    {
        _myHeroUnit.GetHeal(myHealTickValue);
    }
}
