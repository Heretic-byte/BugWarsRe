using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HeroSelectUI;

public class HeroCreateManager : Singleton<HeroCreateManager>
{

    Transform myTans;

    [SerializeField]
    private Transform _controlPanelParent;

    [SerializeField]
    private GameObject _heroDragsPrefab;
    public GameObject myHeroDragsPrefab { get => _heroDragsPrefab; }

   

    private Dictionary<int, UnitHero> _playerHeroes = new Dictionary<int, UnitHero>();

    public Dictionary<int, UnitHero> myPlayerHeroes { get => _playerHeroes; set => _playerHeroes = value; }
    public Transform m_ControlPanelParent { get => _controlPanelParent; }

    protected override void Awake()
    {
        base.Awake();
        myTans = transform;
    }



    public void CreateHeroFromSelectData()
    {
        var SelectedObj= PlayerManager.GetInstance.m_SelectedHeroes;

        for (int i = 0; i < SelectedObj.Length; i++)
        {
            if (SelectedObj[i] != null)
            {
                var CreatedHero = Instantiate(SelectedObj[i], TempleManager.GetInstance.myPlayerHeroPos[i].position, Quaternion.identity, myTans);
                UnitHero unitHero = CreatedHero.GetComponent<UnitHero>();
                AddPlayerHero(CreatedHero.GetInstanceID(), unitHero);

                var CreatedControlPanel = Instantiate(myHeroDragsPrefab, m_ControlPanelParent);
                var HeroControlPanel= CreatedControlPanel.GetComponentInChildren<HeroControlPanel>();

                HeroControlPanel.SetHero(CreatedHero, unitHero);
                //
                var HeroSkill = unitHero.GetComponent<HeroSkillUse>();

                
                HeroSkill.CreateSkillUseButton(unitHero, HeroControlPanel.m_SkillBtnArray);

                
            }
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
