using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursor : MonoBehaviour
{
    private Animator mouseAnimator;

    void Start()
    {
        Cursor.visible = false;
        mouseAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        print(WorldsRotateEight.StartDrag + " start drag");
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
        if(WorldsRotateEight.StartDrag)
        {
            print("cursor drag");
            mouseAnimator.SetTrigger("Start Drag");
        }
        else 
        {
            mouseAnimator.SetTrigger("End Drag");
            print("cursor walking");
            if (Input.GetMouseButton(0))
                mouseAnimator.SetTrigger("Press");
           
        }
        
        

     

        
        
    }
}
