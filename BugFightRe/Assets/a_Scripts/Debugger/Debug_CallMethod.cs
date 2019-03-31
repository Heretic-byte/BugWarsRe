using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Debug_CallMethod : MonoBehaviour
{
    [SerializeField]
    DebugDataMethod[] debugDataMethods;

    public int CheckUnitHash = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       foreach(var DebugMethod in debugDataMethods)
        {
            DebugMethod.InvokeMyMethod();
        }
    }

    public void CheckHashInColliderDic()
    {
        Debug.Log("Hash:" + ColliderDicSingletone.myInstance.myColliderDamageAble[CheckUnitHash].name);
    }


    public void ChangeScaleTime(float _v)
    {
        TimeManager.myInstance.SetCustomTimeScale = _v;

        TempleManager.myInstance.myMonsterTemple.SetCreepLoopTimeScale(_v);
        TempleManager.myInstance.myPlayerTemple.SetCreepLoopTimeScale(_v);
    }
}


[System.Serializable]
public class DebugDataMethod
{
    public KeyCode InputButton;
    public UnityEvent WantToCall;

    public void InvokeMyMethod()
    {
        if(Input.GetKeyDown(InputButton))
        {
            WantToCall?.Invoke();
        }
    }
}
