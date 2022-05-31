using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManagerQuarter : MonoBehaviour
{
     [SerializeField] private SpriteRenderer right, left, button,top;
    
        void Update()
        {
            if(WorldsManager.onButtom)
            {
                button.color = Color.black;
                right.color = Color.white;
                left.color = Color.white;
                top.color = Color.white;
            }
            else if(WorldsManager.onLeft)
            {
               left.color = Color.black;
               right.color = Color.white;
               button.color = Color.white;
               top.color = Color.white;
            }
            else if(WorldsManager.onRight)
            {
                right.color = Color.black;
                left.color = Color.white;
                button.color = Color.white;
                top.color = Color.white;
            }
            else
            {
                top.color = Color.black;
                right.color = Color.white;
                left.color = Color.white;
                button.color = Color.white;
                
            }
            
        }
}
