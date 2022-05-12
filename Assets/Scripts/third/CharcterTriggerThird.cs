using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterTriggerThird : MonoBehaviour
{
    // [SerializeField] private Transform _transform;

    private void Update()
    {
       
        
        var pos = transform.position;
        if (pos.y<0 && ((pos.x<0 && (pos.x >pos.y))|| pos.x>0 && (pos.x<Mathf.Abs(pos.y))))
        {
            print("buttom");
            WorldManagerThird.onButtom = true;
            WorldManagerThird.onLeft = false;
            WorldManagerThird.onRight = false;
        }
        else if (pos.x > 0f && (pos.y >0 || pos.y <0 &&pos.x>Mathf.Abs(pos.y)))
        {
            print("right");
            WorldManagerThird.onRight = true;
            WorldManagerThird.onLeft = false;
            WorldManagerThird.onButtom = false;
        }
        else if (pos.x <= 0f &&(pos.y >0 || pos.y <0 &&pos.x<pos.y) )
        {
            print("left");
            WorldManagerThird.onLeft = true;
            WorldManagerThird.onRight = false;
            WorldManagerThird.onButtom = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            if (other.transform.position.x > 0 && WorldManagerThird.onRight)
            {
                print("win");
            }
            else if (other.transform.position.x <= 0 && WorldManagerThird.onLeft)
            {
                print("win");
            }
        }
    }
}

