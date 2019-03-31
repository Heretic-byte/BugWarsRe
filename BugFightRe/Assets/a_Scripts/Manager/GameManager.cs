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

    event deleUpdateTick _myDeleUpdateTick;

    UnityAction _onGameEnd;
    public UnityAction myOnGameEnd { get => _onGameEnd; set => _onGameEnd = value; }
  
  
    public void AddTickToManager(deleUpdateTick _deleUpdateTick)
    {
        _myDeleUpdateTick += _deleUpdateTick;
    }

    public void RemoveTickFromManager(deleUpdateTick _deleUpdateTick)
    {
        _myDeleUpdateTick-= _deleUpdateTick;
    }

    private void FixedUpdate()
    {
        float tick = Time.fixedDeltaTime;
        _myDeleUpdateTick?.Invoke(tick);

    }

    private void OnLevelWasLoaded(int level)
    {
        HeroCreateManager.myInstance.SetHeroToUi();  
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
