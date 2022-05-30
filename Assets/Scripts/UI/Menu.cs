using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void RestartLevel1()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level-1"));
    }
    
    public void RestartLevel2()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level-2"));
    }
    
    public void RestartLevel3()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level2"));
    }

    public void RestartLevel4()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level3"));
    }
    
    public void RestartLevel6()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level6"));
    }
    
    public void RestartLevel8()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("Level8"));
    }
    public void MenuLevels()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitForOpen("LevelsMenu"));
    }

    public void MenuLevelsFromStart()
    {
        DontDestroyOnLoad(GetComponent<AudioSource>());
        StartCoroutine(WaitForOpen("LevelsMenu"));
    }


    IEnumerator WaitForOpen(string scene)
    {   
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(scene);
    }
}
