using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    private  float _TimeCustomScale = 1f;

    public float GetScaledDeltaTimeTick
    {
        get
        {
            return _TimeCustomScale * Time.fixedDeltaTime;
        }
    }
    public float GetUnScaledDeltaTimeTick
    {
        get
        {
            return  Time.fixedDeltaTime;
        }
    }
    public float SetCustomTimeScale
    {
        set
        {
            _TimeCustomScale = value;
            
        }
    }
    public float GetTimeScaleOnly
    {
        get
        {
            return _TimeCustomScale;
        }
    }
    public float SetFullTimeScale
    {
        set
        {
            Time.timeScale = value;
        }
    }


}
