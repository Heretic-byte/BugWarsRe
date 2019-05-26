using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class JungleLane : MonoBehaviour
{
    [Header("왼쪽에 룬이 안생기는지점")]
    [SerializeField]
    float _LeftIgnoreSize=2;
    [Header("오른쪽에 룬이 안생기는지점")]
    [SerializeField]
    float _RightIgnoreSize=2;
    [Header("룬생성 주기")]
    [SerializeField]
    float _RuneRegenTime=14;
    [Header("한번에 생기는 최대 룬 갯수")]
    [SerializeField]
    int _RuneMaxGenAtOnceCount=3;
    [Header("룬사이사이 거리")]
    [SerializeField]
    int _RuneBetweenDist=1;
    [SerializeField]
    GameObject _RunePrefab;
    [SerializeField]
    int _RunePoolCount = 10;

    LaneRoad _myLaneRoad { get; set; }
    public float myLeftIgnoreSize { get => _LeftIgnoreSize;  }
    public float myRightIgnoreSize { get => _RightIgnoreSize;  }
    public float myRuneRegenTime { get; set; }
    public int myRuneMaxGenAtOnceCount { get => _RuneMaxGenAtOnceCount;  }
    public int myRuneBetweenDist { get => _RuneBetweenDist;}

    Vector3 MinPoint;
    Vector3 MaxPoint;

    Sequence myRegenSeq { get; set; }
    TimeManager myTimeManage { get; set; }
    public GameObject myRunePrefab { get => _RunePrefab; }
    public int myRunePoolCount { get => _RunePoolCount; }
    int _Index { get; set; }
    GameObject[] _RunePool { get; set; }

    void Start()
    {
        _myLaneRoad = GetComponent<LaneRoad>();
        GetColliderSize();
        SetPoint();
        myTimeManage = TimeManager.myInstance;
        myRegenSeq = DOTween.Sequence();
        myRuneRegenTime = _RuneRegenTime;
        CreateRunePool();
        RuneRegenStart();
    }

    private void CreateRunePool()
    {
        _RunePool = new GameObject[myRunePoolCount];
        var parentObj = new GameObject(_myLaneRoad.name+"'s RuneP");
        for (int i=0;i< _RunePool.Length; i++)
        {
            _RunePool[i] = Instantiate(myRunePrefab, parentObj.transform);
            //_RunePool[i].transform.localScale = Vector3.one;
            _RunePool[i].SetActive(false);
        }
        _Index = 0;
    }
   void ShowRune(Vector3 runePos)
    {
        _RunePool[_Index].transform.position = runePos;
        _RunePool[_Index].SetActive(true);
        _Index++;

        if(_Index>= _RunePool.Length)
        {
            _Index = 0;
        }
    }

    private Sequence RuneRegenStart()
    {
        return myRegenSeq.SetLoops(-1)
            .AppendCallback(
            delegate
            {
                SetCreepLoopTimeScale(myTimeManage.GetTimeScaleOnly);
            }
            )
            .AppendCallback(
            RegenRune
            )
            .AppendInterval(myRuneRegenTime);
    }

    private void SetCreepLoopTimeScale(float _v)
    {
        myRegenSeq.timeScale = _v;
    }

    private void SetPoint()
    {
        MinPoint = GetColliderRealMinPoint();
        MinPoint.x += myLeftIgnoreSize;
        MaxPoint = _myLaneRoad.myColl2D.bounds.max;
        MaxPoint.x -= myRightIgnoreSize;

        float half = MaxPoint.y + MinPoint.y;
        half /= 2f;
        MaxPoint.y = half;
        MinPoint.y = half;
    }
    void Update()
    {
        Debug.DrawLine(GetColliderRealMinPoint(), MinPoint, Color.blue);
        Debug.DrawLine(_myLaneRoad.myColl2D.bounds.max, MaxPoint, Color.blue);
    }

    Vector3 GetColliderSize()
    {
       return _myLaneRoad.myColl2D.bounds.size;
    }
    
    Vector3 GetColliderRealMinPoint()
    {
        return _myLaneRoad.myColl2D.bounds.max - GetColliderSize();
    }
    void HideAllRune()
    {
        foreach(var r in _RunePool)
        {
            r.SetActive(false);
        }
    }

    void RegenRune()
    {
        Debug.Log("RegenRune!");
        float Xpos = 0;
        int RegenRuneCount = GetRuneCount();
        Vector3 newRunePos = MinPoint;
        HideAllRune();

        for (int i=0; i< RegenRuneCount; i++)
        {
            Xpos = Random.Range(newRunePos.x, MaxPoint.x);
            newRunePos.x +=Mathf.Abs( Xpos);

            if (newRunePos.x >= MaxPoint.x)
            {
                newRunePos.x = MaxPoint.x - Random.Range(0f, myRuneBetweenDist);
                ShowRune(newRunePos);
                Debug.Log(string.Format("룬생성개수는 {0}개 였어 ", RegenRuneCount));
                break;
            }

            ShowRune(newRunePos);
            newRunePos.x += myRuneBetweenDist;
        }
    }

    private int GetRuneCount()
    {
        return Random.Range(1, _RuneMaxGenAtOnceCount);
    }
}
