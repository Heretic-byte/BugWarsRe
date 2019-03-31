using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastTester : MonoBehaviour
{
    public LayerMask myWhatToHit;

    public RaycastHit2D RaycastGround()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 1000f, myWhatToHit);



     

        if (hit.collider ==null)
        {
            Debug.Log("hit isNull");

        }
        else
        {
            Debug.Log("Hit:" + hit.collider.name);
        }
        
        return hit;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastGround();
           
        }
    }
}
