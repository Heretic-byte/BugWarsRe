using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Debug_CallMethod : MonoBehaviour
{
    [SerializeField]
    DebugDataMethod[] debugDataMethods;

    public int CheckUnitHash = 0;

    [SerializeField]
    Font wantChangeFont;
  
    [ContextMenu("FindAndChangeFont")]
    public void SetGlobalFont()
    {
        var texts = Resources.FindObjectsOfTypeAll(typeof(Text));
        Debug.Log("TEXTCOUNT:" + texts.Length);

        for(int i=0; i<texts.Length;i++)
        {
            var TextReal = texts[i] as Text;
            TextReal.font = wantChangeFont;
        }
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
        Debug.Log("Hash:" + ColliderDicSingletone.GetInstance.GetDamageAble(CheckUnitHash).name);
    }


    public void ChangeScaleTime(float _v)
    {
        TimeManager.GetInstance.SetCustomTimeScale = _v;

        TempleManager.GetInstance.myMonsterTemple.SetCreepLoopTimeScale(_v);
        TempleManager.GetInstance.myPlayerTemple.SetCreepLoopTimeScale(_v);
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
