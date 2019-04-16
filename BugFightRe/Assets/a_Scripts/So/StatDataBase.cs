using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Stat", menuName = "StatDataBase")]
public class StatDataBase :ScriptableObject {

   // public const int MaxLevel = 100;
   
    [Header("BaseStats")]
    public float m_BaseHealth = 800; 
    public float m_BaseAttackSpeed =1;
    public float m_BaseDamage = 50;
    public float m_BaseSpellAmply = 1;
    public float m_BaseArmor=0;
    public float m_BaseMagicArmor=0;
    public float m_BaseMovementSpeed = 10f;
    public float m_BaseAttackRange = 5f;
    public float m_BaseAllyBlockRange = 5f;
   
    public int m_BaseManaReward=100;

    //[Header("LevelUpPerStat")]
    //public float m_LevelPerHp =40f; 
    //public float m_LevelPerAttackSpeed =2f;
    //public float m_LevelPerDamage =2f;
    //public float m_LevelPerSpellDmg = 1f;
    //public float m_LevelPerArmor = 0.3f;
    //public float m_LevelPerSpellArmor = 0.3f;








}
