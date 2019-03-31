using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ubRangeAttack : ubAttackBase
{
    [SerializeField]
    private GameObject _bulletPrefab;
    public GameObject myBulletPrefab { get => _bulletPrefab; }
   

    //총알생성 및 오브젝트풀링필요
    [SerializeField]
    private Vector3 _bulletShootingPos;
    public Vector3 BulletShootingPos { get => _bulletShootingPos; }

    [SerializeField]
    private  int _bulletMaxCount=10;

    GameObject[] _bulletsArray;

    public override void SetInstance()
    {
        base.SetInstance();
        CreateBullet();
    }

    private void CreateBullet()
    {
        var _bulletHolder = new GameObject(myUnit.myObj.name + "'s BulletHolder");
        _bulletsArray = new GameObject[_bulletMaxCount];

        for(int i=0; i< _bulletsArray.Length; i++)
        {
            _bulletsArray[i] = Instantiate(myBulletPrefab, myTrans.position, Quaternion.identity);
        }

    }

    protected override void TryAttack()
    {
       


    }
}
