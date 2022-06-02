using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private GameObject background;
    
    public static bool fromRestart;
    public static bool first = true;

    private void Awake()
    {
        if (fromRestart && level == 0)
            background.SetActive(false);
    }

    private void Update()
    {
        // fromRestart = level == 0;
       /*switch (level)
       {
           case 1:
               RestartLevel1();
               break;
           case 2:
               RestartLevel2();
               break;
           case 3:
               RestartLevel3();
               break;
           case 4:
               RestartLevel4();
               break;
           case 5:
               RestartLevel5();
               break;
       }*/
    }

    public void RestartLevel1()
    {
        fromRestart = true;
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level-1"));
    }
    
    public void RestartLevel2()
    {
        fromRestart = true;
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level-2"));
    }
    
    public void RestartLevel3()
    {
        fromRestart = true;
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level-3"));
    }

    public void RestartLevel4()
    {
        fromRestart = true;
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level-4"));
    }
    public void RestartLevel5()
    {
        fromRestart = true;
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level-5"));
    }
    
    public void RestartLevel6()
    {
        fromRestart = true;
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level6"));
    }
    
    public void RestartLevel8()
    {
        fromRestart = true;
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level8"));
    }
    public void MenuLevels()
    {
        fromRestart = false;
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("LevelsMenu"));
    }

    public void MenuLevelsFromStart()
    {
        fromRestart = false;
        DontDestroyOnLoad(GetComponent<AudioSource>());
        StartCoroutine(WaitForOpen("LevelsMenu"));
    }


    IEnumerator WaitForOpen(string scene)
    {   
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(scene);
    }
    
    IEnumerator WaitForLevel1(string scene)
    {   
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scene);
    }
}
