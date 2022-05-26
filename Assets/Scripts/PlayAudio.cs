using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();
    }
    
}
