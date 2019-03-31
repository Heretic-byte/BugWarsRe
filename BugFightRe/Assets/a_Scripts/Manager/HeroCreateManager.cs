using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCreateManager : Singleton<HeroCreateManager>
{

    Transform myTans;

    [SerializeField]
    private HeroDrag[] _heroDrags;

    public HeroDrag[] myHeroDrags { get => _heroDrags; }

    private Dictionary<int, UnitHero> _playerHeroes = new Dictionary<int, UnitHero>();

    public Dictionary<int, UnitHero> myPlayerHeroes { get => _playerHeroes; set => _playerHeroes = value; }

    protected override void Awake()
    {
        base.Awake();
        myTans = transform;
    }

    public void CreateHeroFromSelectData()
    {
        for (int i = 0; i < HeroStageManager.myInstance.mySelectedHero.Length; i++)
        {
            var CreatedHero = Instantiate(HeroStageManager.myInstance.mySelectedHero[i], TempleManager.myInstance.myPlayerHeroTemplePos[i].position,Quaternion.identity, myTans);
            var HeroUnit = CreatedHero.GetComponent<UnitHero>();
            AddPlayerHero(CreatedHero.GetInstanceID(), HeroUnit);

            myHeroDrags[i].SetHero(CreatedHero, HeroUnit);
         
        }
    }

    public void SetHeroToUi()
    {
        CreateHeroFromSelectData();

    }

    private void AddPlayerHero(int ObjInstanceId, UnitHero unitHero)
    {
        myPlayerHeroes.Add(ObjInstanceId, unitHero);
    }

    private bool RemovePlayerHero(int ObjInstanceId)
    {
        return myPlayerHeroes.Remove(ObjInstanceId);
    }



}
