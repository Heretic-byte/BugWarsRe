
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


public class CannonBall : Projectile
{
    public DamageAble myTargetUnit { get; set; }
   
   

    public Unit myTheBulletCaster { get; set; }

    public event UnityAction _OnBulletTrigger;

    public Sequence myTimerCounter { get; set; }

    [SerializeField]
    private float _bulletMaxLifeDur = 2f;
    public float myBulletMaxLifeDur { get => _bulletMaxLifeDur; set => _bulletMaxLifeDur = value; }
    public float m_CannonHeight { get => _cannonHeight;  }
    float m_CannonBallDamage { get; set; }
    public float m_Radius { get => _radius; }

    ColliderDicSingletone m_CollManage { get; set; }
    public UnityEvent m_OnExplosion { get => _OnExplosion; }

    [SerializeField]
    float _cannonHeight = 1.5f;
    [SerializeField]
    float _radius = 1f;

    [SerializeField]
    UnityEvent _OnExplosion;

    float m_EraseTime { get; set; } = 2f;

    public override void AddTickToManager()
    {
       
    }

    public override void FixedTickFloat(float _tick)
    {

    }

    public override void RemoveTickFromManager()
    {
     
    }


    public override void SetInstance(Unit _myCaster, UnityAction _onBulletTriggered)
    {
        myTheBulletCaster = _myCaster;

        m_CannonBallDamage = _myCaster.GetAttackDamage();

        m_CollManage = ColliderDicSingletone.GetInstance;

        myTheBulletCaster.myOnAttackTargetDead += OnEnqueue;

       
    }
    public override void ShootProjectile(DamageAble _targetUnit)
    {
        transform.DOJump(_targetUnit.myTrans.position, m_CannonHeight, 1, myBulletMaxLifeDur).OnComplete(TryExplode).SetEase(Ease.InCubic);
    }
    void TryExplode()
    {
        var hitten = Physics2D.OverlapCircleAll(transform.position, m_Radius, myTheBulletCaster.myTargetLayers);

        m_OnExplosion?.Invoke();

        if (hitten.Length > 0)
        {
          foreach(var AA in  m_CollManage.GetDamageAbleArry(hitten))
            {
                AA.TakePhysicalDamage(m_CannonBallDamage, myTheBulletCaster);
            }
        }
        Sequence sequence = DOTween.Sequence();
        sequence.SetDelay(m_EraseTime+ myBulletMaxLifeDur).OnComplete(OnEnqueue);
    }

    void OnEnqueue()
    {
        myObj.SetActive(false);

        myTargetUnit = null;

    }
}
