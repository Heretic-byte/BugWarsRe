
using UnityEngine;
using UnityEngine.UI;
public class CameraMover : MonoBehaviour, IfixedTickFloat
{
    [SerializeField]
    private float _CamMoveSpeed = 5f;
    Transform myTrans { get; set; }
    GameObject myObj { get; set; }
    public GameManager myManagerGame { get ; set; }
    public Transform myCamRightBoundTrans { get => _camRightBoundTrans; set => _camRightBoundTrans = value; }
    public Vector3 myCamLeftBound { get => _camLeftBound; set => _camLeftBound = value; }
    public Vector3 myCamRightBound { get => _camRightBound; set => _camRightBound = value; }

    [SerializeField]
    Transform _camRightBoundTrans;

    Vector3 _camLeftBound;
    Vector3 _camRightBound;

    void Start()
    {   
        myTrans = transform;
        myObj = gameObject;
        myCamLeftBound = myTrans.position;
        myCamRightBound = myCamRightBoundTrans.position;

        AddTickToManager();
    }

    Vector3 input;
    public void FixedTickFloat(float _tick)
    {
        
        //input.x = Input.GetAxisRaw("Horizontal");
        myTrans.Translate(input * _CamMoveSpeed * _tick);

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
        input.x = 1;
       
    }
    public void CamMoveLeft()
    {
        input.x = -1;

    }
    public void CamMoveStop()
    {

        input.x = 0;
    }
    
   
    public void AddTickToManager()
    {
        GameManager.myInstance.AddUnScaledTickToManager(FixedTickFloat);
    }

    public void RemoveTickFromManager()
    {
        GameManager.myInstance.RemoveUnScaledTickFromManager(FixedTickFloat);
    }

}
