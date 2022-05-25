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
        if (Input.GetMouseButton(0))
            mouseAnimator.SetTrigger("Press");
        
    }
}
