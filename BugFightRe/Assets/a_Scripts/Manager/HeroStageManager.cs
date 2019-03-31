using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStageManager : Singleton<HeroStageManager>
{

    //예비
    [SerializeField]
    private GameObject[] _selectedHero;

    public GameObject[] mySelectedHero { get => _selectedHero; set => _selectedHero = value; }

    public void SelectStageMobileHero()
    {

    }
}
