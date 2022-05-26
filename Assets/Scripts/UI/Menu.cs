using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void RestartLevel1()
    {
        SceneManager.LoadScene("Level0");
    }
    
    public void RestartLevel2()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void RestartLevel3()
    {
        SceneManager.LoadScene("Level2");
    }

    public void RestartLevel4()
    {
        SceneManager.LoadScene("Level3");
    }
    
    public void RestartLevel6()
    {
        SceneManager.LoadScene("Level6");
    }
    
    public void RestartLevel8()
    {
        SceneManager.LoadScene("Level8");
    }
    public void MenuLevels()
    {
        SceneManager.LoadScene("LevelsMenu");
    }
    
    public void MenuLevelsFromMenu()
    {
        DontDestroyOnLoad(GetComponent<AudioSource>());
        SceneManager.LoadScene("LevelsMenu");
    }
}
