using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneForceMove : MonoBehaviour
{
    public int SceneIndex = 2;
    public mySceneManager mySceneManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            mySceneManager.MoveScene(SceneIndex);
        }
    }
}
