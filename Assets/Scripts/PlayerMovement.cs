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

            float angle = Mathf.Atan2
                (targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * 180 / Mathf.PI;
            
            print(angle);
            
            if (angle < 60 && angle > -60)
            {
                print("moveRight");
            }
            else if (angle >= 60 && angle < 120)
            {
                print("moveUp");
            }
            else if (angle <= -60 && angle > -120)
            {
               print("moveDown");
            }
            else if ((angle <= -120) || (angle > 120))
            {
               print("moveLeft");
            }
           
        }
    }
    
}
