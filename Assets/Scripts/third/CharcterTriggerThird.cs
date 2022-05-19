using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterTriggerThird : MonoBehaviour
{
    // [SerializeField] private Transform _transform;
    private bool _key;

    private void Update()
    {
        
        var pos = transform.position;
        if (pos.x <= 0f &&(pos.y >0 || pos.y <0 &&pos.x<pos.y-1f) ) 
        { 
            print("left");
            WorldManagerThird.onLeft = true;
            WorldManagerThird.onRight = false;
            WorldManagerThird.onButtom = false;
        }
        else if(pos.y<0 && ((pos.x<0 && pos.x >pos.y-1.7f)|| pos.x>0 && pos.x<Mathf.Abs(pos.y)+1f))
        {    print("down");
            WorldManagerThird.onButtom = true;
            WorldManagerThird.onLeft = false;
            WorldManagerThird.onRight = false;
        }
        else if (pos.x > 0f && (pos.y >0 || pos.y <0 &&pos.x>Mathf.Abs(pos.y)+1f))
        {
            print("right");
            WorldManagerThird.onRight = true;
            WorldManagerThird.onLeft = false;
            WorldManagerThird.onButtom = false;
        }
      
        
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     print("triggerEnter");
    //     if (other.CompareTag("Door"))
    //     {
    //         var pos = other.transform.position;
    //         if (pos.x <= 0f &&(pos.y >0 || pos.y <0 &&pos.x<pos.y) && WorldManagerThird.onLeft && _key)
    //         {
    //             print("win");
    //             _key = false;
    //         }
    //     }
    //
    //     if (other.CompareTag("Key"))
    //     {
    //         print("key0");
    //         var pos = other.transform.position;
    //         if (pos.x > 0f && (pos.y >0 || pos.y <0 &&pos.x>Mathf.Abs(pos.y)) && WorldManagerThird.onRight)
    //         {
    //             print("key") ;
    //             _key = true;
    //         }
    //     }
    // }
}

