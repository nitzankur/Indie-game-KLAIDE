
using System;
using Unity.Mathematics;
using UnityEngine;

public class CharacterTrigger : MonoBehaviour
{
   

   private void Update()
   {
       var pos = transform.position;
       if (pos.y > 0 && pos.y >= Mathf.Abs(pos.x))
       {
           WorldsManager.onTop = true;
           WorldsManager.onLeft = false;
           WorldsManager.onRight = false;
   //        print("top");
       }
      
       else if (pos.y < 0 &&  Mathf.Abs(pos.x) <= Mathf.Abs(pos.y))
       {
           WorldsManager.onButtom = true;
           WorldsManager.onLeft = false;
           WorldsManager.onRight = false;
   //      print("down");
       }

       else if (pos.x > 0 && pos.x > Mathf.Abs(pos.y))
       {
           WorldsManager.onRight = true;
           WorldsManager.onTop = false;
           WorldsManager.onButtom = false;
    //      print("right");   
       }
       else if(pos.x<0 && Mathf.Abs(pos.x) > Mathf.Abs(pos.y))
       {
           WorldsManager.onLeft = true;
           WorldsManager.onTop = false;
           WorldsManager.onButtom = false;
     //      print("LEFt"); 
       }
       

       
       

   }

}
