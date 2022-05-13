using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    public void PlayGame()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
