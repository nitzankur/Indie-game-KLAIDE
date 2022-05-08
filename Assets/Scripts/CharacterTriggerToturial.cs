using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriggerToturial : MonoBehaviour
{
    private void Update()
    {
        var pos = transform.position;
        if (pos.x > 0)
        {
            WorldsManagerToturial.onRight = true;
            WorldsManagerToturial.onLeft = false;
            print("right");
        }
        else if (pos.x <= 0)
        {
            WorldsManagerToturial.onLeft = true;
            WorldsManagerToturial.onRight = false;
            print("Left");
        }
    }

}
