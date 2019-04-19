using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class DamageAble : MonoBehaviour
{
    public delegate void OnDamageFloatDele(float _damageTaken);
    public delegate void OnDamageBySomeone(Unit _attacker);
    public delegate void OnKillFromAttackerDele(Unit _attacker);

    public Transform myTrans { get ; set ; }
    public GameObject myObj { get ; set ; }
    public Collider2D myCollider2D { get ; set ; }
    public event UnityAction myOnDamageAction;
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
    public ColliderDicSingletone myManagerColider { get => _managerColider; set => _managerColider = value; }
    public float GetCurrentHealth { get => _currentHealth; set => _currentHealth = value; }

    [SerializeField]
    protected StatDataBase _stat;

    public abstract float GetMaxHealth();
    public abstract float GetAttackSpeed();
    public abstract float GetAttackDamage();
    public abstract float GetSpellDamagePercent();
    public abstract float GetArmor();
    public abstract float GetSpellArmorPercent();

    private float _currentHealth = 0f;
    protected Vector2 myDir = Vector2.right;

    protected ColliderDicSingletone _managerColider;
  

    protected virtual void MainSetInstance()
    {
        myTrans = transform;
        myObj = gameObject;
        myCollider2D = GetComponent<Collider2D>();

        myManagerColider = ColliderDicSingletone.myInstance;
        SetColliderDic();
      
    }
    public void SetColliderDic()
    {
        myManagerColider.myColliderDamageAble.Add(myCollider2D.GetInstanceID(), this);
    }
    public void RemoveColliderDic()
    {
        myManagerColider.myColliderDamageAble.Remove(myCollider2D.GetInstanceID());
    }
    public virtual void SetHpToMax()
    {
        GetCurrentHealth = GetMaxHealth();
        
    }

    public  void GetPhysicalDamage(float _damageTaken, Unit _attacker)
    {
        myOnDamageBySomeone?.Invoke(_attacker);


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
            GetKill();
        }
    }
    
    public void GetMagicalDamage(float _damageTaken, Unit _attacker)
    {
      
        myOnDamageBySomeone?.Invoke(_attacker);

        float mySpellArmor = GetSpellArmorPercent();

        _damageTaken *= 1 - (mySpellArmor / 100f);

        GetCurrentHealth -= _damageTaken;
        //
        myOnDamageAction?.Invoke();
        myOnDamageFloat?.Invoke(_damageTaken);

        if (GetCurrentHealth <= 0)
        {
            myOnKillFromAttacker?.Invoke(_attacker);
            _attacker.AttackTargetDead();
            GetKill();
        }

    }
  
    public virtual void GetKill()
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
}
