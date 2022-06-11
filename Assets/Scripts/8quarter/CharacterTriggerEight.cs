
using System;
using Unity.Mathematics;
using UnityEngine;

public class CharacterTriggerEight : MonoBehaviour
{

    private const float Radius = 9.1f;

    private void Update()
   {
       var pos = transform.position;
       if (pos.y > 0 && pos.y >= Mathf.Abs(pos.x) && Vector3.Distance(pos, Vector3.zero) >= Radius)
       {
           WorldsManagerEight.onTop = true;
           WorldsManagerEight.onLeft = false;
           WorldsManagerEight.onRight = false;
           WorldsManagerEight.onBottom = false;
           WorldsManagerEight.onInsideTop = false;
           WorldsManagerEight.onInsideLeft = false;
           WorldsManagerEight.onInsideRight = false;
           WorldsManagerEight.onInsideBottom = false;
       }
       
       else if (pos.y > 0 && pos.y >= Mathf.Abs(pos.x) && Vector3.Distance(pos, Vector3.zero) < Radius)
       {
           WorldsManagerEight.onInsideTop = true;
           WorldsManagerEight.onTop = false;
           WorldsManagerEight.onLeft = false;
           WorldsManagerEight.onRight = false;
           WorldsManagerEight.onBottom = false;
           WorldsManagerEight.onInsideLeft = false;
           WorldsManagerEight.onInsideRight = false;
           WorldsManagerEight.onInsideBottom = false;
       }
      
       else if (pos.y < 0 &&  Mathf.Abs(pos.x) <= Mathf.Abs(pos.y) && Vector3.Distance(pos, Vector3.zero) >= Radius)
       {
           WorldsManagerEight.onBottom = true;
           
           WorldsManagerEight.onTop = false;
           WorldsManagerEight.onLeft = false;
           WorldsManagerEight.onRight = false;
           WorldsManagerEight.onInsideTop = false;
           WorldsManagerEight.onInsideLeft = false;
           WorldsManagerEight.onInsideRight = false;
           WorldsManagerEight.onInsideBottom = false;
       }
        
       else if (pos.y < 0 &&  Mathf.Abs(pos.x) <= Mathf.Abs(pos.y) && Vector3.Distance(pos, Vector3.zero) < Radius)
       {
           WorldsManagerEight.onInsideBottom = true;
           
           WorldsManagerEight.onBottom = false;
           WorldsManagerEight.onTop = false;
           WorldsManagerEight.onLeft = false;
           WorldsManagerEight.onRight = false;
           WorldsManagerEight.onInsideTop = false;
           WorldsManagerEight.onInsideLeft = false;
           WorldsManagerEight.onInsideRight = false;
       }

       
       else if (pos.x > 0 && pos.x > Mathf.Abs(pos.y) && Vector3.Distance(pos, Vector3.zero) >= Radius)
       {
           WorldsManagerEight.onRight = true;
           
           WorldsManagerEight.onInsideBottom = false;
           WorldsManagerEight.onBottom = false;
           WorldsManagerEight.onTop = false;
           WorldsManagerEight.onLeft = false;
           WorldsManagerEight.onInsideTop = false;
           WorldsManagerEight.onInsideLeft = false;
           WorldsManagerEight.onInsideRight = false;
       }
       
       else if (pos.x > 0 && pos.x > Mathf.Abs(pos.y) && Vector3.Distance(pos, Vector3.zero) < Radius)
       {
           WorldsManagerEight.onInsideRight = true;
           
           WorldsManagerEight.onRight = false;
           WorldsManagerEight.onInsideBottom = false;
           WorldsManagerEight.onBottom = false;
           WorldsManagerEight.onTop = false;
           WorldsManagerEight.onLeft = false;
           WorldsManagerEight.onInsideTop = false;
           WorldsManagerEight.onInsideLeft = false;
       }
       
       
       else if (pos.x < 0 && Mathf.Abs(pos.x) > Mathf.Abs(pos.y) && Vector3.Distance(pos, Vector3.zero) >= Radius)
       {
           WorldsManagerEight.onLeft = true;
          
           WorldsManagerEight.onInsideRight = false;
           WorldsManagerEight.onRight = false;
           WorldsManagerEight.onInsideBottom = false;
           WorldsManagerEight.onBottom = false;
           WorldsManagerEight.onTop = false;
           WorldsManagerEight.onInsideTop = false;
           WorldsManagerEight.onInsideLeft = false;
       }
       
       else if (pos.x < 0 && Mathf.Abs(pos.x) > Mathf.Abs(pos.y) && Vector3.Distance(pos, Vector3.zero) < Radius)
       {
           WorldsManagerEight.onInsideLeft = true;
           
           WorldsManagerEight.onInsideRight = false;
           WorldsManagerEight.onRight = false;
           WorldsManagerEight.onInsideBottom = false;
           WorldsManagerEight.onBottom = false;
           WorldsManagerEight.onTop = false;
           WorldsManagerEight.onLeft = false;
           WorldsManagerEight.onInsideTop = false;
       }


   }

}
