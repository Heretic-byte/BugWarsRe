
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraMover : MonoBehaviour, IfixedTickFloat
{
    [SerializeField]
    private float _CamMoveSpeed = 5f;

    [SerializeField]
    Transform _camRightBoundTrans;

    [SerializeField]
    private float _fastCamMoveSpeedTime = 0.5f;

    Vector3 _camLeftBound;
    Vector3 _camRightBound;
    Tween _CamMoveTween { get; set; }
    float myFastCamMoveSpeed { get => _fastCamMoveSpeedTime; }
    Vector3 _Input;
    Transform myTrans { get; set; }
    GameObject myObj { get; set; }
    public GameManager myManagerGame { get; set; }
    public Transform myCamRightBoundTrans { get => _camRightBoundTrans; set => _camRightBoundTrans = value; }
    public Vector3 myCamLeftBound { get => _camLeftBound; set => _camLeftBound = value; }
    public Vector3 myCamRightBound { get => _camRightBound; set => _camRightBound = value; }

  
    void Start()
    {
        myTrans = transform;
        myObj = gameObject;
        myCamLeftBound = myTrans.position;
        myCamRightBound = myCamRightBoundTrans.position;

        AddTickToManager();
    }
    
    public void FixedTickFloat(float _tick)
    {
        myTrans.Translate(_Input * _CamMoveSpeed * _tick);

        if (myTrans.position.x > myCamRightBound.x)
        {
            myTrans.position = myCamRightBound;
        }
        if(myTrans.position.x< myCamLeftBound.x)
        {
            myTrans.position = myCamLeftBound;
        }
    }

    public void CamMoveRight()
    {
        _Input.x = 1;
    }

    public void CamMoveLeft()
    {
        _Input.x = -1;
    }

    public void CamMoveStop()
    {
        _Input.x = 0;
    }

    public void MoveCamLeftTemple()
    {
        _CamMoveTween?.Kill();
        _CamMoveTween = myTrans.DOMove(myCamLeftBound, myFastCamMoveSpeed);
    }
    public void MoveCamRightTemple()
    {
        _CamMoveTween?.Kill();
        _CamMoveTween = myTrans.DOMove(myCamRightBound, myFastCamMoveSpeed);
    }

    public void AddTickToManager()
    {
        GameManager.GetInstance.AddUnScaledTickToManager(FixedTickFloat);
    }

    public void RemoveTickFromManager()
    {
        GameManager.GetInstance.RemoveUnScaledTickFromManager(FixedTickFloat);
    }

  
    public void DragCameraMove()
    {
        Vector3 CurrentViewPortCoord = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if(CurrentViewPortCoord.y<0.35f)
        {
            return;
        }

        if (CurrentViewPortCoord.x < 0.2f)
        {

            CamMoveLeft();
        }
        else if (CurrentViewPortCoord.x > 0.8f)
        {

            CamMoveRight();
        }
        else
        {
            CamMoveStop();

        }
    }
}
