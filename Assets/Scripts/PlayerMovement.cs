using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxUp = 1;
    private void FixedUpdate()
    { 
        Vector3 targ = Vector3.zero; 
        Vector3 objectPos = transform.position; 
        targ.x = targ.x - objectPos.x; 
        targ.y = targ.y - objectPos.y; 
        float angle = (Mathf.Atan2(targ.y, targ.x) - 80)* Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (targetPosition.y > transform.position.y)
            {
                //UP
                
                if (targetPosition.x - transform.position.x < maxUp)
                {
                    print("up");
                }
                else
                {
                    IndicateRightOrLeft(targetPosition);
                }
            }
            else if (targetPosition.y < transform.position.y)
            {
                //DOWN
                
                if (transform.position.x - targetPosition.x < maxUp)
                {
                    print("down");
                }
                else
                {
                    IndicateRightOrLeft(targetPosition);
                }
            }
        }
    }

    void IndicateRightOrLeft(Vector3 target)
    {
        print(target.x > transform.position.x ? "right" : "left");
    }
}
