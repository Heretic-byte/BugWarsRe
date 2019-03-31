using UnityEngine;



public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _Instance;

    public static T myInstance
    {
        get
        {
          
                return _Instance;
            
        }
    }



    protected virtual void Awake()
    {
        _Instance = this as T;
    }

}