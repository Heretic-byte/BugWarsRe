using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    float GroundPosZ = 20f;
    [SerializeField]
    private GameObject _dragIndicatorP;
    public GameObject myDragIndicator { get; set; }
    private Transform _myDragIndicatorTrans { get; set; }

    [SerializeField]
    LayerMask _whatToHit;

    public LayerMask myWhatToHit { get => _whatToHit; set => _whatToHit = value; }


    public Image myDragButtonIcon { get; set; }

    GameObject _myHeroObj { get; set; }

    UnitHero _myHeroUnit { get; set; }

    LaneRoad _myPreLaneRoad { get; set; }

    public void Init()
    {
        myDragButtonIcon = GetComponent<Image>();

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
    public void OnBeginDrag(PointerEventData eventData)
    {
        myDragIndicator = Instantiate(_dragIndicatorP, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        _myDragIndicatorTrans = myDragIndicator.transform;
        SetDraggedPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition();

        var coll = GetCollRaycast();
        if (coll != null)
        {   
            HidePreLaneRoadFeedBack();
            _myPreLaneRoad = StageMapManager.myInstance.myLaneAndCollDic[coll.GetInstanceID()];
            _myPreLaneRoad.ShowFeedBackLaneRoad();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        HidePreLaneRoadFeedBack();
        Destroy(myDragIndicator);
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
}
