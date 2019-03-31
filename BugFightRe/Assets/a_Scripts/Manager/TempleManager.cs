using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleManager : Singleton<TempleManager>
{
    [SerializeField]
    private SpawnTemple _playerTemple;
    public SpawnTemple myPlayerTemple { get => _playerTemple; }
  
    [SerializeField]
    private SpawnTemple _monsterTemple;
    public SpawnTemple myMonsterTemple { get => _monsterTemple;  }

    [SerializeField]
    private Transform _playerHeroTempleTrans;
    public Transform myPlayerHeroTempleTrans { get => _playerHeroTempleTrans; }

    [SerializeField]
    private Transform _monsterHeroTempleTrans;
    public Transform myMonsterHeroTempleTrans { get => _monsterHeroTempleTrans; }

    [SerializeField]
    private Transform[] _playerHeroPos;
    public Transform[] myPlayerHeroPos { get => _playerHeroPos; }

    [SerializeField]
    private Transform[] _monsterHeroPos;
    public Transform[] myMonsterHeroPos { get => _monsterHeroPos; }



}
