using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;


public class SpawnTemple : MonoBehaviour, IgameEnd
{
    #region VarAndProperties

    [Header("Need Drag")]
    public Text CountT;//debug
    [SerializeField]
    private GameObject[] _lineCreepCheckUiObj;
    public GameObject[] myLineCreepCheckUiObj { get => _lineCreepCheckUiObj; set => _lineCreepCheckUiObj = value; }

    [SerializeField]
    private Nexus[] _creepNexus;//5
    public Nexus[] myCreepNexus { get => _creepNexus; set => _creepNexus = value; }

    [SerializeField]
    private float _EachSpawnDelay = 7f;
    public float myEachSpawnDelay { get => _EachSpawnDelay; set => _EachSpawnDelay = value; }

    [SerializeField]
    private GameObject _creepPrefab;
    public GameObject CreepPrefab { get => _creepPrefab; set => _creepPrefab = value; }

    [SerializeField]
    private int _CreepPoolCount;
    public int myCreepPoolCount { get => _CreepPoolCount; set => _CreepPoolCount = value; }

    [SerializeField]
    private int _CreepSpawnCountAtOnce;
    public int myCreepSpawnCountAtOnce { get => _CreepSpawnCountAtOnce; set => _CreepSpawnCountAtOnce = value; }

    [SerializeField]
    private float _uiShowDelay = 2f;
    public float myUiShowDelay { get => _uiShowDelay; set => _uiShowDelay = value; }

    [SerializeField]
    private int _nexusLifeCount = 3;
    public int myNexusLifeCount { get => _nexusLifeCount; set => _nexusLifeCount = value; }

    [SerializeField]
    private Image _spawnTermUi;
    public Image mySpawnTermUi { get => _spawnTermUi; set => _spawnTermUi = value; }

  

    int[] _creepLineShuffleArray;
    public int[] myCreepLineShuffleArray { get => _creepLineShuffleArray; set => _creepLineShuffleArray = value; }

  
    Queue<GameObject> _creepPoolQue = new Queue<GameObject>();
    public Queue<GameObject> myCreepPoolQue { get => _creepPoolQue; set => _creepPoolQue = value; }


    Dictionary<int, UnitCreep> _creepEntityDic = new Dictionary<int, UnitCreep>();
    public Dictionary<int, UnitCreep> myCreepEntityDic { get => _creepEntityDic; set => _creepEntityDic = value; }


    GameObject myObj { get; set; }
    Transform myTrans { get; set; }

    Dictionary<int, Nexus> _laneAndNexus = new Dictionary<int, Nexus>();
    public Dictionary<int, Nexus> myLaneAndNexus { get => _laneAndNexus; set => _laneAndNexus = value; }

    bool _isILose = false;
    public bool myIsILose { get => _isILose; set => _isILose = value; }

    Sequence _spawnCreepSeq;
    public Sequence mySpawnCreepSeq { get => _spawnCreepSeq; private set => _spawnCreepSeq = value; }
   
    Sequence _spawnCreepUiShowSeq;
    public Sequence mySpawnCreepUiShowSeq { get => _spawnCreepUiShowSeq; set => _spawnCreepUiShowSeq = value; }
    


    #endregion
    void Start()
    {
        myObj = gameObject;
        myTrans = transform;
       
        CreatePoolObj();
        SetDeleOnGameEnd();
        SetShuffleArray();
        SetLaneLoadAndNexus();
        SetNexusKillDelegate();
      

        mySpawnCreepSeq = DOTween.Sequence();
        mySpawnCreepUiShowSeq = DOTween.Sequence();

        CreepSpawnLoops();
        CreepSpawnUiShowLoops();
        mySpawnTermUi.fillAmount = 0f;
    }

    private void SetShuffleArray()
    {

        int count = myCreepNexus.Length;
        myCreepLineShuffleArray = new int[count];
        for (int i = 0; i < count; i++)
        {
            myCreepLineShuffleArray[i] = i;
        }
    }
    void ShuffleArrayForRandomLine(int loopCount)
    {
        var count = myCreepNexus.Length;
        for (int j = 0; j < loopCount; j++)
        {
            for (int i = 0; i < count; i++)
            {
                var rand = Random.Range(0, myCreepNexus.Length);
                var temp = myCreepLineShuffleArray[count - 1];
                myCreepLineShuffleArray[count - 1] = myCreepLineShuffleArray[rand];
                myCreepLineShuffleArray[rand] = temp;
            }
        }
    }
    void CreatePoolObj()
    {
     
        GameObject CreepHolder = new GameObject(myObj.name + "'s CreepHolder");

        
        for (int i = 0; i < myCreepPoolCount; i++)
        {
            var CreatedObj = Instantiate(CreepPrefab, CreepHolder.transform);
            CreatedObj.name += ".No_" + i;
           
            var CreatedEntity = CreatedObj.GetComponent<UnitCreep>();
            myCreepEntityDic.Add(CreatedObj.GetInstanceID(), CreatedEntity);

            myCreepPoolQue.Enqueue(CreatedObj);

            CreatedEntity.OnEnqueActionObj += EnqueueCreep;
            CreatedObj.SetActive(false);
        }

       
    }

    public Sequence CreepSpawnLoops()
    {

        return mySpawnCreepSeq.SetLoops(-1)
            .AppendCallback(
              delegate
              {
                  SetCreepLoopTimeScale(TimeManager.myInstance.GetTimeScaleOnly);
              })
               .AppendCallback(
              delegate
              {
                  ShuffleArrayForRandomLine(3);
              })
              .Append(

                  ShowSpawnTermUi()
              )
             
             .AppendCallback(
            delegate
            {
                for (int i = 0; i < myCreepSpawnCountAtOnce; i++)
                {
                    DequeueCreep(myCreepNexus[myCreepLineShuffleArray[i]].GetSpawnPos());
                }
            })
             .AppendCallback(
            delegate
            {
                HideAllSpawnCreepUi();
            });       
    }
     Sequence CreepSpawnUiShowLoops()
    {
        return mySpawnCreepUiShowSeq.SetLoops(-1).PrependInterval(_uiShowDelay)
            .AppendCallback(
            delegate
            {
                for (int i = 0; i < myCreepSpawnCountAtOnce; i++)
                {
                    myLineCreepCheckUiObj[myCreepLineShuffleArray[i]].SetActive(true);
                }
            })
            .AppendInterval(_EachSpawnDelay-_uiShowDelay);
        //둘이 딜레이가 달라서 생기는일
    }

    public void SetCreepLoopTimeScale(float _v)
    {
        mySpawnCreepSeq.timeScale = _v;
        mySpawnCreepUiShowSeq.timeScale = _v;
    }


    void HideAllSpawnCreepUi()
    {
        for (int i = 0; i < myCreepNexus.Length; i++)
        {
            myLineCreepCheckUiObj[i].SetActive(false);
        }
    }
    void DequeueCreep(Vector3 _spawnPos)
    {

        if (myCreepPoolQue.Count< myCreepSpawnCountAtOnce)
        {
            return;
        }
      

        var DequeuedObj = myCreepPoolQue.Dequeue();
           
        var DequeueEntity = myCreepEntityDic[DequeuedObj.GetInstanceID()];
   
        DequeueEntity.myTrans.position = _spawnPos;

        DequeueEntity.OnDequeue();
        DequeuedObj.SetActive(true);   
    }
    void EnqueueCreep(GameObject unitSelf)
    {
        myCreepPoolQue.Enqueue(unitSelf);
      
    }
    public void SetDeleOnGameEnd()
    {
        //GameManager.myInstance.myOnGameEnd += delegate { StopCoroutine("SpawnCreep"); };
        GameManager.myInstance.myOnGameEnd += delegate { mySpawnCreepSeq.Kill();mySpawnCreepUiShowSeq.Kill(); };
    }
    void SetLaneLoadAndNexus()
    {
        for (int i = 0; i < myCreepNexus.Length; i++)
        {
            myLaneAndNexus.Add(StageMapManager.myInstance.myStageLaneRoads[i].myColl2D.GetInstanceID(),
                myCreepNexus[i]);
        }
    }
    void SetNexusKillDelegate()
    {
        //넥서스의 죽음 이벤트를 등록
        foreach (var nexus in myCreepNexus)
        {
            nexus.myOnGetKillAction += DiscountNexus;
        }

    }
    void DiscountNexus()
    {
        myNexusLifeCount--;

        if (myNexusLifeCount < 0)
        {
            //해당템플 패배!

            myIsILose = true;
            GetLoseGame();
        }
    }
    void GetLoseGame()
    {
        Debug.Log(myObj.name + "'s Lost");
        GameManager.myInstance.FinishGame();
    }

    Tweener ShowSpawnTermUi()
    {
       return mySpawnTermUi.DOFillAmount(1, _EachSpawnDelay)
            .OnComplete
            (
            delegate {
                mySpawnTermUi.fillAmount = 0;
            }).SetEase(Ease.Linear);
    }
}
