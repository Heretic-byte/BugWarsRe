using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mySceneManager : Singleton<mySceneManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        InitScene();
    }
    public void InitScene()
    {
        SceneManager.LoadScene(1);
    }
    public void MoveScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
