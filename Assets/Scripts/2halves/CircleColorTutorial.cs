using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleColorTutorial : MonoBehaviour
{
    [SerializeField] private SpriteRenderer right, left;

    // Update is called once per frame
    void Update()
    {
      
        if(WorldsManagerToturial.onLeft)
        {
            left.color = Color.black;
            right.color = Color.white;
        }
        else
        {
            right.color = Color.black;
            left.color = Color.white;
        }
        
    }
}
