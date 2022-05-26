using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;

public class CircleColorManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer right, left, button;

    // Update is called once per frame
    void Update()
    {
        if(WorldManagerThird.onButtom)
        {
            button.color = Color.black;
            right.color = Color.white;
            left.color = Color.white;
        }
        else if(WorldManagerThird.onLeft)
        {
           left.color = Color.black;
           right.color = Color.white;
           button.color = Color.white;
        }
        else
        {
            right.color = Color.black;
            left.color = Color.white;
            button.color = Color.white;
        }
        
    }
}
