using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public delegate void DeleGiveMana(int manaValue);

public class ManaManager : Singleton<ManaManager>
{
    [SerializeField]
    private int _nPlayerMaxMana=1000;
    private int _nPlayerCurrentMana=0;
    [SerializeField]
    private int _nMonsterMaxMana=1000;
    private int _nMonsterCurrentMana=0;

    [SerializeField]
    private LayerMask _playerLayer;
    [SerializeField]
    private LayerMask _monsterLayer;
    [SerializeField]
    private Image _manaBarBackGround;
    [SerializeField]
    private Image _manaBarForeGround;

    private Dictionary<LayerMask, DeleGiveMana> _PlayerAndEnemyManaDeleDic = new Dictionary<LayerMask, DeleGiveMana>();

    public int myPlayerMaxMana { get => _nPlayerMaxMana; private set => _nPlayerMaxMana = value; }
    public int myPlayerCurrentMana { get => _nPlayerCurrentMana; private set => _nPlayerCurrentMana = value; }
    public int myMonsterMaxMana { get => _nMonsterMaxMana; private set => _nMonsterMaxMana = value; }
    public int myMonsterCurrentMana { get => _nMonsterCurrentMana; private set => _nMonsterCurrentMana = value; }
    public Dictionary<LayerMask, DeleGiveMana> myPlayerAndEnemyManaDeleDic { get => _PlayerAndEnemyManaDeleDic; set => _PlayerAndEnemyManaDeleDic = value; }
    public LayerMask myPlayerLayer { get => _playerLayer; set => _playerLayer = value; }
    public LayerMask myMonsterLayer { get => _monsterLayer; set => _monsterLayer = value; }
    public Image myManaBarBackGround { get => _manaBarBackGround; set => _manaBarBackGround = value; }
    public Image myManaBarForeGround { get => _manaBarForeGround; set => _manaBarForeGround = value; }

    protected override void Awake()
    {
        base.Awake();
        SetInstance();
    }

    private void SetInstance()
    {
        myPlayerAndEnemyManaDeleDic.Add(myPlayerLayer, AddManaToPlayer);
        myPlayerAndEnemyManaDeleDic.Add(myMonsterLayer, AddManaToMonster);
        SetPlayerManaBar();
    }

    public void AddManaToPlayer(int manaValue)
    {
        myPlayerCurrentMana += manaValue;
        UpdatePlayerManaBar(manaValue);

        Debug.Log("PlayerGotMana:" + manaValue);
        if(myPlayerMaxMana > myPlayerCurrentMana)
        {
            myPlayerCurrentMana = myPlayerMaxMana;
        }
    }

    public void AddManaToMonster(int manaValue)
    {
        myMonsterCurrentMana += manaValue;
        Debug.Log("MonsterGotMana:" + manaValue);
        if (myMonsterMaxMana > myMonsterCurrentMana)
        {
            myMonsterCurrentMana = myMonsterMaxMana;
        }
    }
    void SetPlayerManaBar()
    {
        myManaBarBackGround.fillAmount = 1;
        myManaBarForeGround.fillAmount = 0;
    }
    void UpdatePlayerManaBar(int manaValue)
    {
        float fillTemp = myManaBarForeGround.fillAmount;

        myManaBarForeGround.DOFillAmount(fillTemp + ((myPlayerMaxMana / manaValue)/100f), 0.3f);
    }

}
