using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public abstract class ubRangeAttack : ubAttackBase
{
    [SerializeField]
    private GameObject _bulletPrefab;
    public GameObject myBulletPrefab { get => _bulletPrefab; }

    [Header("오른쪽이 디폴트")]
    [SerializeField]
    private Vector3 _bulletShootingPos;
    public Vector3 myBulletShootingPos { get => _bulletShootingPos; set => _bulletShootingPos = value; }

    int _currentBulletIndex = 0;
    public int myCurrentBulletIndex { get => _currentBulletIndex; set => _currentBulletIndex = value; }

    [SerializeField]
    private  int _bulletMaxCount=10;
    public int myBulletMaxCount { get => _bulletMaxCount; set => _bulletMaxCount = value; }

    GameObject[] _bulletsArray;
    public GameObject[] myBulletsArray { get => _bulletsArray; set => _bulletsArray = value; }

    Bullet[] _bullets;
    public Bullet[] myBullets { get => _bullets; set => _bullets = value; }
    //
    
    public override void SetInstance()
    {
        base.SetInstance();
        CreateBullet();
        //
    
    }
    protected override void SetTranslateDir()
    {
        if (!myUnit.myIsFacingRight)
        {
            _attackDir = Vector3.left;
            _bulletShootingPos.x *= -1;
        }
        else
        {
            _attackDir = Vector3.right;

        }
    }

    private void CreateBullet()
    {
        var _bulletHolder = new GameObject(myUnit.myObj.name + "'s BulletHolder");
        myBulletsArray = new GameObject[myBulletMaxCount];
        myBullets = new Bullet[myBulletMaxCount];

        for (int i=0; i< myBulletsArray.Length; i++)
        {
            myBulletsArray[i] = Instantiate(myBulletPrefab, myTrans.position, Quaternion.identity, _bulletHolder.transform);
            myBullets[i]= myBulletsArray[i].GetComponent<Bullet>();
            myBullets[i].SetInstance(myUnit,OnBulletDealDamage);
            myBulletsArray[i].SetActive(false);
        }
    }

    protected override void TryAttack()
    {
        rayOrigin = myTrans.position + myUnit.myRayCastOffset;
        targetHitten = Physics2D.Raycast(rayOrigin, _attackDir, myAttackRange, myUnit.myTargetLayers);
        Debug.DrawRay(rayOrigin, _attackDir * myAttackRange, Color.red, 1f);
        
        if (targetHitten.collider != null)
        {
       
            myUnit.myAttackTarget = myManagerCollDic.myColliderDamageAble[targetHitten.collider.GetInstanceID()];
           
        }
        else
        {

            myUnit.myAttackTarget = null;
            return;
        }


        if (_attackTimer > myAttackSpeed)
        {
         
            _attackTimer = 0f;

            myUnit.myOnAttack?.Invoke();

        }

    }
    public override void Attack()
    {

        if (myUnit.myAttackTarget == null)
        {

            return;
        }
        ShootBullet();
    }
 
    void ShootBullet()
    {
       myBulletsArray[myCurrentBulletIndex].SetActive(true);
       myBullets[myCurrentBulletIndex].myTrans.position = myTrans.TransformPoint(  myBulletShootingPos);
        myBullets[myCurrentBulletIndex].BulletShooting(myUnit.myAttackTarget);


        myCurrentBulletIndex++;
        if(myCurrentBulletIndex >= myBulletMaxCount)
        {
            myCurrentBulletIndex = 0;
        }
    }

    void OnBulletDealDamage()
    {
        if (myUnit.myAttackTarget==null)
        {
            return;
        }
        myUnit.myAttackTarget.GetPhysicalDamage(myUnit.myStat.m_BaseDamage, myUnit);
    }

}
