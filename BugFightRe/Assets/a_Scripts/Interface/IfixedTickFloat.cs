using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface IfixedTickFloat 
{
    void FixedTickFloat(float _tick);
    void AddTickToManager();
    void RemoveTickFromManager();
    GameManager myManagerGame { get; set; }
}
