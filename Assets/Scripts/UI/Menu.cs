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
        LevelManager.Level = 4;
        fromRestart = true;
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level-4"));
    }
    public void RestartLevel5()
    {
        LevelManager.Level = 5;
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
