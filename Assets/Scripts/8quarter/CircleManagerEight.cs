using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManagerEight : MonoBehaviour
{
    [SerializeField] private SpriteRenderer right, left, button,top;
    
    void Update()
    {
        if(WorldsManagerEight.onBottom)
        {
            button.color = Color.black;
            right.color = Color.white;
            left.color = Color.white;
            top.color = Color.white;
        }
        else if(WorldsManagerEight.onLeft)
        {
            left.color = Color.black;
            right.color = Color.white;
            button.color = Color.white;
            top.color = Color.white;
        }
        else if(WorldsManagerEight.onRight)
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
