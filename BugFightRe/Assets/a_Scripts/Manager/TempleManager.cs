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
    private Transform _playerHeroTemple;
    public Transform myPlayerHeroTemple { get => _playerHeroTemple; }

    [SerializeField]
    private Transform _monsterHeroTemple;
    public Transform myMonsterHeroTemple { get => _monsterHeroTemple; }

    [SerializeField]
    private Transform[] _playerHeroTemplePos;
    public Transform[] myPlayerHeroTemplePos { get => _playerHeroTemplePos; }

    [SerializeField]
    private Transform[] _monsterHeroTemplePos;
    public Transform[] myMonsterHeroTemplePos { get => _monsterHeroTemplePos; }



}
