using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DoubleClick : MonoBehaviour,IPointerClickHandler
{

    public UnityEvent OnImageDoubleClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
      if(  eventData.clickCount>=2)
        {
           
            OnImageDoubleClicked?.Invoke();
        }

    }
}
