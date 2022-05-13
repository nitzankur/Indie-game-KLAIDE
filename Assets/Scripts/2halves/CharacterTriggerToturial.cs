using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterTriggerToturial : MonoBehaviour
{
    
    private void Update()
    {
        var pos = transform.position;
        if (pos.x > 0f)
        {
            WorldsManagerToturial.onRight = true;
            WorldsManagerToturial.onLeft = false;
        }
        else if (pos.x <= 0f)
        {
            WorldsManagerToturial.onLeft = true;
            WorldsManagerToturial.onRight = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            if (other.transform.position.x > 0 && WorldsManagerToturial.onRight)
            {
                print("win");
            }
            else if (other.transform.position.x <= 0 && WorldsManagerToturial.onLeft)
            {
                print("win");
            }
        }
    }
}
