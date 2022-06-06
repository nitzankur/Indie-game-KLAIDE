using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleSystemScript : MonoBehaviour
{
   private void Start()
   {
      var part =  GetComponent<ParticleSystem>();
      part.Play();
   }

   // void PlayAndDestroy(ParticleSystem part)
   // {
   //    part =
   //    part.Play();
   // }
}
