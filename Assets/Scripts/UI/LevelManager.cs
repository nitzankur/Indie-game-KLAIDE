using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int totalLevel = 3;
    public static int unlockedLevel = 0;
    private ButtonLevel[] levelButtons;

    private void Start()
    {
        Refresh();
    }

    
    private void OnEnable()
    {
        levelButtons = GetComponentsInChildren<ButtonLevel>();
    }

    public void Refresh()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            var level = i;
            if (level <= totalLevel)
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].SetUp(level, level<=unlockedLevel);
            }
            else
                levelButtons[i].gameObject.SetActive(false);
        }
    }

}
