using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public delegate void deleUpdateTick(float tick);

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private SpawnTemple _playerTemple;
    public SpawnTemple myPlayerTemple { get => _playerTemple; set => _playerTemple = value; }

    [SerializeField]
    private SpawnTemple _enemyTemple;
    public SpawnTemple myEnemyTemple { get => _enemyTemple; set => _enemyTemple = value; }

    event deleUpdateTick _myDeleScaledUpdateTick;
    event deleUpdateTick _myDeleUnScaledUpdateTick;
    event deleUpdateTick _myDeleScaleOnlyUpateTick;
    UnityAction _onGameEnd;
    public UnityAction myOnGameEnd { get => _onGameEnd; set => _onGameEnd = value; }


    protected override void Awake()
    {
        base.Awake();
        //프레임?
    }

    public void AddScaledTickToManager(deleUpdateTick _deleUpdateTick)
    {
        _myDeleScaledUpdateTick += _deleUpdateTick;
    }

    public void RemoveScaledTickFromManager(deleUpdateTick _deleUpdateTick)
    {
        _myDeleScaledUpdateTick-= _deleUpdateTick;
    }

    public void AddUnScaledTickToManager(deleUpdateTick _deleUpdateTick)
    {
        _myDeleUnScaledUpdateTick += _deleUpdateTick;
    }

    public void RemoveUnScaledTickFromManager(deleUpdateTick _deleUpdateTick)
    {
        _myDeleUnScaledUpdateTick -= _deleUpdateTick;
    }
    public void AddOnlyScaleTickToManager(deleUpdateTick _deleUpdateTick)
    {
        _myDeleScaleOnlyUpateTick += _deleUpdateTick;
    }

    public void RemoveOnlyScaleTickFromManager(deleUpdateTick _deleUpdateTick)
    {
        _myDeleScaleOnlyUpateTick -= _deleUpdateTick;
    }

    private void FixedUpdate()
    {
        float ScaleOnly = TimeManager.GetInstance.GetTimeScaleOnly;
        _myDeleScaleOnlyUpateTick?.Invoke(ScaleOnly);
        float ScaledTick = TimeManager.GetInstance.GetScaledDeltaTimeTick;
        _myDeleScaledUpdateTick?.Invoke(ScaledTick);
        float unScaledTick = TimeManager.GetInstance.GetUnScaledDeltaTimeTick;
        _myDeleUnScaledUpdateTick?.Invoke(unScaledTick);
    }

    private void OnLevelWasLoaded(int level)
    {
        HeroCreateManager.GetInstance.SetHeroToUi();  
    }

    public void FinishGame()
    {
        myOnGameEnd?.Invoke();

        if(myPlayerTemple.myIsILose)
        {
            EnemyWinGame();
        }
        else
        {
            PlayerWinGame();
        }


        myOnGameEnd = null;
    }
    public void PlayerWinGame()
    {
        Debug.Log("Player Win");
    }
    public void EnemyWinGame()
    {
        Debug.Log("Enemy Win");
    }
}
