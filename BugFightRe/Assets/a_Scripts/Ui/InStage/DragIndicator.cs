using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragIndicator : MonoBehaviour
{
    GameObject myObj { get; set; }
    Transform myTrans { get; set; }
     SpriteRenderer myImage { get => _myImage;  }

    [SerializeField]
    SpriteRenderer _myImage;

    void Awake()
    {
        myObj = gameObject;
        myTrans = transform;
        SetDefault();
    }

    public void SetIcon(Sprite selectedIconSprite)
    {
        myImage.sprite = selectedIconSprite;
    }
    public void SetDefault()
    {
        myImage.sprite = null;
    }
    public void SetActive(bool v)
    {
        myObj.SetActive(v);
    }
}
