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
    [SerializeField]
    private Text _manaValueText;
    [SerializeField]
    private Text _manaMaxValueText;
    [SerializeField]
    private float _TweenDur=0.2f;
    private float _TextTweenDur;

    private Tween _playerManaFillTween;

    private Dictionary<LayerMask, DeleGiveMana> _PlayerAndEnemyManaDeleDic = new Dictionary<LayerMask, DeleGiveMana>();

    public int myPlayerMaxMana { get => _nPlayerMaxMana; private set => _nPlayerMaxMana = value; }
    public int myPlayerCurrentMana { get => _nPlayerCurrentMana; private set => _nPlayerCurrentMana = value; }
    public int myMonsterMaxMana { get => _nMonsterMaxMana; private set => _nMonsterMaxMana = value; }
    public int myMonsterCurrentMana { get => _nMonsterCurrentMana; private set => _nMonsterCurrentMana = value; }
    public Dictionary<LayerMask, DeleGiveMana> myPlayerAndEnemyManaDeleDic { get => _PlayerAndEnemyManaDeleDic; set => _PlayerAndEnemyManaDeleDic = value; }
    public LayerMask myPlayerLayer { get => _playerLayer; set => _playerLayer = value; }
    public LayerMask myMonsterLayer { get => _monsterLayer; set => _monsterLayer = value; }
    public Image myManaBarBackGround { get => _manaBarBackGround;  }
    public Image myManaBarForeGround { get => _manaBarForeGround;  }
    public Text myManaValueText { get => _manaValueText; }
    public Text myManaMaxValueText { get => _manaMaxValueText;  }
    public float myTweenDur { get => _TweenDur;  }
    public float myTextTweenDur { get => _TextTweenDur; set => _TextTweenDur = value; }

    protected override void Awake()
    {
        base.Awake();
        SetInstance();
        myTextTweenDur = myTweenDur * 2f;
    }

    private void SetInstance()
    {
        myPlayerAndEnemyManaDeleDic.Add(myPlayerLayer, AddManaToPlayer);
        myPlayerAndEnemyManaDeleDic.Add(myMonsterLayer, AddManaToMonster);
        SetPlayerManaBar();
        SetPlayerManaText();
        myPlayerCurrentMana = 0;
    }

    public void AddManaToPlayer(int manaValue)
    {
        myPlayerCurrentMana += manaValue;

      
        if (myPlayerMaxMana < myPlayerCurrentMana)
        {
            myPlayerCurrentMana = myPlayerMaxMana;
        }

        UpdatePlayerManaBar();
        UpdatePlayerCurrentManaText();

    }

    public void AddManaToMonster(int manaValue)
    {
        myMonsterCurrentMana += manaValue;

        Debug.Log("MonsterGotMana:" + manaValue);

        if (myMonsterMaxMana < myMonsterCurrentMana)
        {
            myMonsterCurrentMana = myMonsterMaxMana;
        }
    }

    public bool SubstractManaFromPlayer(int manaValue)
    {

        if(manaValue> myPlayerCurrentMana)
        {
            return false;
        }

        myPlayerCurrentMana -= manaValue;
        UpdatePlayerManaBar();
        UpdatePlayerCurrentManaText();

        return true;
    }
    public bool SubstractManaFromMonster(int manaValue)
    {

        if (manaValue > myMonsterCurrentMana)
        {
            return false;
        }

        myMonsterCurrentMana -= manaValue;
        return true;
    }
    void SetPlayerManaText()
    {
        myManaValueText.text = "0";
        myManaMaxValueText.text ="/"+ myPlayerMaxMana.ToString();
    }

    void UpdatePlayerCurrentManaText()
    {
        myManaValueText.DOText(myPlayerCurrentMana.ToString(), myTextTweenDur, false, ScrambleMode.Numerals);
    }
    void UpdatePlayerMaxManaText()
    {
        myManaValueText.DOText("/" + myPlayerMaxMana.ToString(), myTextTweenDur, false, ScrambleMode.Numerals);
    }

    void SetPlayerManaBar()
    {
        myManaBarBackGround.fillAmount = 1;
        myManaBarForeGround.fillAmount = 0;
    }
    void UpdatePlayerManaBar()
    {
        _playerManaFillTween?.Complete();
        float percent = (float)myPlayerCurrentMana / (float)myPlayerMaxMana;
           _playerManaFillTween = myManaBarForeGround.DOFillAmount(percent, myTweenDur);
    }

}
