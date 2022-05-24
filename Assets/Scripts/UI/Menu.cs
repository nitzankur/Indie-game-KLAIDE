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
    public void MenuLevels()
    {
        SceneManager.LoadScene("LevelsMenu");
    }
}
