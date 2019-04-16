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
  private bool _myIsHeroInLine { get; set; }
    public void Init()
    {
        myDragButtonIcon = GetComponent<Image>();
        myDragIndicator = Instantiate(_dragIndicatorP, Vector3.zero, Quaternion.identity);
        myDragIndicator.SetActive(false);
        _myIsHeroInLine = false;
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
     
     var hittenLog=   GetCollRaycast();

        if(hittenLog==null)
        {
            return;
        }
      
        _myIsHeroInLine = true;
        //유닛히어로 출격시작

      var summonNexus=  StageMapManager.myInstance.myLaneAndCollDic[hittenLog.GetInstanceID()].myLeftNexus;

        var tempV3 = summonNexus.mySpawnPointArray[0];
        tempV3.z -= 0.1f;
        _myHeroUnit.GoRushBattleField(summonNexus.myTrans.position+ tempV3);
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
    void CheckCanHeroGoBattle()
    {
        if (_myIsHeroInLine)
        {
            return;
        }
        if(_myHeroUnit.myIsDead)
        {
            return;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CheckCanHeroGoBattle();

        var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myDragIndicator.transform.position = MousePos;
        myDragIndicator.SetActive(true);
        _myDragIndicatorTrans = myDragIndicator.transform;
        SetDraggedPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        CheckCanHeroGoBattle();

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
        CheckCanHeroGoBattle();

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

            _myHeroUnit.GoBackToTemple();
            //템플이 우물회복기능 가지게하고
            //귀환시 우물회복 틱 붙여줌
            //출격시 틱 없애줌
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clidc");
       if(!_myHeroUnit.myIsDead &&_myIsHeroInLine)
        {
            HeroRecall();
        }
    }
}
