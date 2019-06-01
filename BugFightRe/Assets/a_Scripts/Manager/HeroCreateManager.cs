using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCreateManager : Singleton<HeroCreateManager>
{

    Transform myTans;

    [SerializeField]
    private HeroDrag[] _heroDrags;
    public HeroDrag[] myHeroDrags { get => _heroDrags; }

    [SerializeField]
    private GameObject[] _heroSkillButtons;
    public GameObject[] myHeroSkillButtons { get => _heroSkillButtons; }

    private Dictionary<int, UnitHero> _playerHeroes = new Dictionary<int, UnitHero>();

    public Dictionary<int, UnitHero> myPlayerHeroes { get => _playerHeroes; set => _playerHeroes = value; }

    protected override void Awake()
    {
        base.Awake();
        myTans = transform;
    }

    public void CreateHeroFromSelectData()
    {
        for (int i = 0; i < HeroStageManager.GetInstance.mySelectedHero.Length; i++)
        {
            var CreatedHero = Instantiate(HeroStageManager.GetInstance.mySelectedHero[i], TempleManager.GetInstance.myPlayerHeroPos[i].position,Quaternion.identity, myTans);
            var HeroUnit = CreatedHero.GetComponent<UnitHero>();
            AddPlayerHero(CreatedHero.GetInstanceID(), HeroUnit);

            myHeroDrags[i].SetHero(CreatedHero, HeroUnit);
            //
            var HeroSkill = HeroUnit.GetComponent<HeroSkillUse>();
            HeroSkill.CreateSkillUseButton(HeroUnit,myHeroSkillButtons[i]);
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
