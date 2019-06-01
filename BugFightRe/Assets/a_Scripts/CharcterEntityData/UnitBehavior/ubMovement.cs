
using UnityEngine;
using UnityEngine.Events;

public class ubMovement :myUnitBehavior
{
    public float myMovementSpeed { get; set; }
 
    Vector3 _MoveDir = Vector3.right;

 

    public override void SetInstance()
    {
        myUnit = GetComponent<Unit>();
        myTrans = myUnit.myTrans;
        SetTranslateDir();
        myMovementSpeed = myUnit.myStat.m_BaseMovementSpeed;

    }
    public void SetTranslateDir()
    {
        if (!myUnit.myIsFacingRight)
        {
            _MoveDir = Vector3.left;
        }
        else
        {
            _MoveDir = Vector3.right;
        }
    }

    public override void FixedTickFloat(float _tick)
    {
        if (myUnit.myAttackTarget == null && myUnit.myBlockingAlly == null)
        {
            myUnit.myOnWalking?.Invoke();
            myTrans.Translate(_MoveDir * myMovementSpeed * _tick);
        }
        else
        {
            myUnit.myOnNotWalking?.Invoke();
        }
    }

    //프레임마다 계속 처넣고있음
    public override void AddTickToManager()
    {
        GameManager.GetInstance.AddScaledTickToManager(FixedTickFloat);
    }

    public override void RemoveTickFromManager()
    {
        GameManager.GetInstance.RemoveScaledTickFromManager(FixedTickFloat);
    }
}
