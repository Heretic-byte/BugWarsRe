using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class DamageAble : MonoBehaviour
{
    public delegate void OnDamageFloatDele(float _damageTaken);
    public delegate void OnDamageBySomeone(Unit _attacker,float _damageGiven);
    public delegate void OnKillFromAttackerDele(Unit _attacker);

    public Transform myTrans { get ; set ; }
    public GameObject myObj { get ; set ; }
    public Collider2D myCollider2D { get ; set ; }
    public event UnityAction myOnDamageAction;
    public UnityAction myOnHealAction { get; set; }
    public event OnDamageFloatDele myOnDamageFloat;
    public event OnDamageBySomeone myOnDamageBySomeone;
    public event OnKillFromAttackerDele myOnKillFromAttacker;
    public event UnityAction myOnKillAction;
  

    public bool myIsDead { get => _isDead; set => _isDead = value; }
    protected bool _isDead = false;
    [SerializeField]
    protected bool _IsFacingRight = true;
    public bool myIsFacingRight { get => _IsFacingRight; set => _IsFacingRight = value; }
    public StatDataBase myStat { get => _stat; set => _stat = value; }
    public StatDataBase.StatValue myRealStat { get; protected set; }
    public ColliderDicSingletone myManagerColider { get => _managerColider; set => _managerColider = value; }
    public float GetCurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public Vector2 GetMyDir { get => myDir;  }

    [SerializeField]
    protected StatDataBase _stat;

    public abstract float GetMaxHealth();
    public abstract float GetAttackSpeed();
    public abstract float GetAttackDamage();
    public abstract float GetSpellDamagePercent();
    public abstract float GetArmor();
    public abstract float GetSpellArmorPercent();

    private float _currentHealth = 0f;
    private Vector2 myDir = Vector2.right;

    protected ColliderDicSingletone _managerColider;
  

    protected virtual void MainSetInstance()
    {
        myTrans = transform;
        myObj = gameObject;
        myCollider2D = GetComponent<Collider2D>();

        myManagerColider = ColliderDicSingletone.GetInstance;
        SetColliderDic();
      
    }
    public void SetColliderDic()
    {
        myManagerColider.AddDamageAble(myCollider2D.GetInstanceID(), this);
    }
    public void RemoveColliderDic()
    {
        myManagerColider.RemoveDamageAble(myCollider2D.GetInstanceID());
    }
    public virtual void SetHpToMax()
    {
        GetCurrentHealth = GetMaxHealth();
    }

    public virtual void TakePhysicalDamage(float _damageTaken, Unit _attacker)
    {
        myOnDamageBySomeone?.Invoke(_attacker,_damageTaken);


        float myArmor = GetArmor();

        float ratio = 1 - ((0.052f * myArmor) / (0.9f + 0.048f * Mathf.Abs(myArmor)));

        _damageTaken *= ratio;

        GetCurrentHealth -= _damageTaken;
        //
        myOnDamageAction?.Invoke();    
        myOnDamageFloat?.Invoke(_damageTaken);

        if (GetCurrentHealth <= 0)
        {
            myOnKillFromAttacker?.Invoke(_attacker);
            _attacker.AttackTargetDead();
            TakeKill();
        }
    }
    
    public void TakeMagicalDamage(float _damageTaken, Unit _attacker)
    {
      
        myOnDamageBySomeone?.Invoke(_attacker,_damageTaken);

        float mySpellArmor = GetSpellArmorPercent();

        _damageTaken *= 1 - (mySpellArmor / 100f);

        GetCurrentHealth -= _damageTaken;
        //
        myOnDamageAction?.Invoke();
        myOnDamageFloat?.Invoke(_damageTaken);

        if (GetCurrentHealth <= 0)
        {
            myOnKillFromAttacker?.Invoke(_attacker);
            //_attacker.AttackTargetDead();
            TakeKill();
        }
    }
    public virtual void GetHeal(float _HealValue)
    {
        GetCurrentHealth += _HealValue;

        myOnHealAction?.Invoke();
        if (GetCurrentHealth > GetMaxHealth())
        {
            SetHpToMax();
        }
    }
    public virtual void TakeKill()
    {
        myOnKillAction?.Invoke();

        

        myIsDead = true;

        myCollider2D.enabled = false;
    }

    public void SetDir()
    {
        Vector3 myScaleTemp = myTrans.localScale;
        myScaleTemp.x = Mathf.Abs(myScaleTemp.x);

        if (myIsFacingRight)
        {

            myTrans.localScale = myScaleTemp;
            myDir = Vector2.right;
        }
        else
        {
            myScaleTemp.x = -myScaleTemp.x;
            myTrans.localScale = myScaleTemp;
            myDir = Vector2.left;
        }
    }
    public void SetDir(Vector3 dir)
    {
        Vector3 myScaleTemp = myTrans.localScale;
        myScaleTemp.x = Mathf.Abs(myScaleTemp.x);

        if (dir.x > 1)
        {
            myIsFacingRight = true;
        }
        else 
        {
            myIsFacingRight = false;
        }
        
    }
}
