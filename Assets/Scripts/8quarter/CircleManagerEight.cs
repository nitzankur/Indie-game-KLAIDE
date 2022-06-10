using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManagerEight : MonoBehaviour
{
    [SerializeField] private SpriteRenderer right, left, button,top;
    
    void Update()
    {
        if(WorldsManagerEight.onBottom || WorldsManagerEight.onInsideBottom)
        {
            button.color = Color.black;
            right.color = Color.white;
            left.color = Color.white;
            top.color = Color.white;
        }
        else if(WorldsManagerEight.onLeft || WorldsManagerEight.onInsideLeft)
        {
            left.color = Color.black;
            right.color = Color.white;
            button.color = Color.white;
            top.color = Color.white;
        }
        else if(WorldsManagerEight.onRight||WorldsManagerEight.onInsideRight)
        {
            right.color = Color.black;
            left.color = Color.white;
            button.color = Color.white;
            top.color = Color.white;
        }
        else if(WorldsManagerEight.onTop || WorldsManagerEight.onInsideTop)
        {
            top.color = Color.black;
            right.color = Color.white;
            left.color = Color.white;
            button.color = Color.white;
                
        }
            
    }
}
