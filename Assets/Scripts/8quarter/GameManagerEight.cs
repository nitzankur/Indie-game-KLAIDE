using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEight : MonoBehaviour
{
    [SerializeField] private GameObject winMessage;
    private static GameManagerEight _shared;

    public static bool winGame;
    // Start is called before the first frame update
    void Start()
    {
        _shared = this;
        winGame = false;
    }

    private void Update()
    {
        if (winGame)
        {
            winMessage.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
