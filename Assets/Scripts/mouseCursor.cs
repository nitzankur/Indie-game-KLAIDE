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
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
        if (WorldsRotateEight.StartDrag)
        {
            Cursor.visible = true;
            gameObject.SetActive(false);
        }
        else if(WorldsRotateEight.EndDrag)
        {
            Cursor.visible = false;
            gameObject.SetActive(true);
            if (Input.GetMouseButton(0))
                mouseAnimator.SetTrigger("Press");
        }

     

        
        
    }
}
