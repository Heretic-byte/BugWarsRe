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
    [SerializeField]
    private Text _HeroRespawnTimeText;
    public Text myHeroRespawnTimeText { get => _HeroRespawnTimeText; }
    [SerializeField]
    private Image _HeroRespawnCDImage;
    public Image myHeroRespawnCDImage { get => _HeroRespawnCDImage; }
    [SerializeField]
    private float _recallDelay = 3f;
    public float myRecallDelay { get => _recallDelay; }
    [SerializeField]
    private float _recallCoolDown = 20f;
    public float myRecallCoolDown { get => _recallCoolDown; }
    [SerializeField]
    private float _healTickValue = 200;
    public float myHealTickValue { get => _healTickValue; }

    private Image _myDragButtonIcon { get; set; }   
    private Text _myHeroPosText { get; set; }  
    private GameObject _myHeroObj { get; set; }
    private UnitHero _myHeroUnit { get; set; }
    private LaneRoad _myPreLaneRoad { get; set; }
    private Transform _myDragIndicatorTrans { get; set; }
    private bool _IsRecallCd { get; set; }
    private Sequence _myHeroHealSeq { get; set; } 
    private float myRespawnTimer { get; set; }
    private Sequence _myRespawnTimerSeq { get; set; }
    private Tween _myRespawnImageTween { get; set; }
    private Sequence _myRespawnImageSeq { get; set; }

    public void Init()
    {
        _myDragButtonIcon = GetComponent<Image>();
        myDragIndicator = Instantiate(_dragIndicatorP, Vector3.zero, Quaternion.identity);
        myDragIndicator.SetActive(false);
        _IsRecallCd = false;
        _myHeroPosText = GetComponentInChildren<Text>();
    }

    public void SetHero(GameObject heroObj, UnitHero heroUnit)
    {
        Init();
        _myHeroObj = heroObj;
        _myHeroUnit = heroUnit;
        _myDragButtonIcon.sprite = heroUnit.myPortrait;

        heroUnit.myOnRecall += HideHeroBattleFieldLineNumber;

        heroUnit.myOnRespawnCountDown += ShowHeroDeathRespawnTime;
        heroUnit.myOnRespawn += HideHeroDeathRespawnTime;

        heroUnit.myOnRespawnCountDown += ShowHeroDeathRespawnImage;
        heroUnit.myOnRespawn += HideHeroDeathRespawnImage;

        HideHeroBattleFieldLineNumber();
        HideHeroDeathRespawnTime();
        //ui표시 등등
    }

    public void GoBattleField()
    {
        var hittenLog = GetCollRaycast();

        if (hittenLog == null)
        {
            return;
        }
     
        var SummonLine= StageMapManager.myInstance.myLaneAndCollDic[hittenLog.GetInstanceID()];
       
        ShowHeroBattleFieldLineNumber(SummonLine.myLaneNumber);
        var SummonNexus = SummonLine.myLeftNexus;
        var TempV3 = SummonNexus.mySpawnPointArray[0];
        TempV3.z -= 0.1f;

        _myHeroHealSeq?.Kill();

        _myHeroUnit.GoRushBattleField(SummonNexus.myTrans.position + TempV3);
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

    public void BuyBack()
    {
        _myHeroUnit.Respawn();
    }

    public void HeroRecall()
    {
        if(_IsRecallCd)
        {
            return;
        }


        if (ManaManager.myInstance.SubstractManaFromPlayer(myRecallManaCost))
        {        
            _myHeroUnit.GoBackToTemple(myRecallDelay).onComplete+= TempleHeroHealSequence;
            _IsRecallCd = true;
            Sequence RecallCdSeq = DOTween.Sequence();
            RecallCdSeq.SetDelay(myRecallCoolDown).OnComplete(delegate { _IsRecallCd = false; });
            //쿨다운표시랑 리스폰쿨다운이랑 겹침
            //쿨다운 일단존재하고
            //죽으면 초상화 검게만들고 부활시간 표시
            
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
    
    void ShowHeroDeathRespawnTime(float respawnTime)
    {
        myRespawnTimer = respawnTime;
      
        _myRespawnTimerSeq?.Kill();
        _myRespawnTimerSeq = DOTween.Sequence();
        _myRespawnTimerSeq.SetEase(Ease.Linear).SetLoops(-1).AppendCallback(RespawnTimer);

    }
   
    void ShowHeroDeathRespawnImage(float respawnTime)
    {
        myHeroRespawnCDImage.fillAmount = 1;
        myHeroRespawnCDImage.DOFade(0, 0);
         _myRespawnImageSeq = DOTween.Sequence();
        _myRespawnImageSeq.Append(myHeroRespawnCDImage.DOFade(1, 3))
            .Append(_myDragButtonIcon.DOFillAmount(0, respawnTime))
            .Append(myHeroRespawnCDImage.DOFade(0, 0.1f)).SetEase(Ease.Linear);

    }
    void RespawnTimer()
    {
        myRespawnTimer -= (Time.deltaTime/2f);

        myHeroRespawnTimeText.text = string.Format("{00:F1}", myRespawnTimer);
    }
    void HideHeroDeathRespawnTime()
    {
        _myRespawnTimerSeq?.Kill();
        myHeroRespawnTimeText.text = "";
    }
    void HideHeroDeathRespawnImage()
    {
        _myRespawnImageSeq?.Kill();
        myHeroRespawnCDImage.fillAmount = 0;
        myHeroRespawnCDImage.DOFade(0, 0);

    }
    void ShowHeroBattleFieldLineNumber(int _laneNumb)
    {
        _myHeroPosText.text = _laneNumb.ToString();
    }
    void HideHeroBattleFieldLineNumber()
    {
        _myHeroPosText.text = "In";
    }
}
