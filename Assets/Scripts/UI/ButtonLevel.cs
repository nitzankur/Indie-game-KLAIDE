using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    [SerializeField] private Sprite unlockSprite;
    [SerializeField] private Sprite lockSprite;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private GameObject player;
    private int level = 0;
    private bool isUnlock;
    private Button button;
    private Image image;

    private void OnEnable()
    {
        if (player.activeSelf && !Menu.first)
            player.SetActive(false);
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }
    
    public void SetUp(int level, bool isUnlock)
    {
        this.level = level;
        if (isUnlock)
        {
            this.isUnlock = true;
            image.sprite = unlockSprite;
            button.enabled = true;
        }
        else
        {
            image.sprite = lockSprite;
            button.enabled = false;
        }
    }

    public void OnClick()
    {
        if (!isUnlock) return;
        if (level == 0 && Menu.first)
        {
            player.GetComponent<Animator>().SetBool("start", true);
            //button.GetComponent<Animator>().SetTrigger("open");
            Menu.first = false;
            StartCoroutine(WaitForLevel1());
            return;
        }
        GetComponent<AudioSource>().Play();
        StartCoroutine(WaitAndLoad());
    }
    
    IEnumerator WaitAndLoad()
    {   
        yield return new WaitForSeconds(0.6f);
        image.sprite = openSprite;
        yield return new WaitForSeconds(1f);
        int nextLevel;
        nextLevel = level + 1;
        SceneManager.LoadScene("Level-" + nextLevel);
    }
    IEnumerator WaitForLevel1()
    {   
        yield return new WaitForSeconds(1.8f);
        GetComponent<AudioSource>().Play();
        image.sprite = openSprite;
        yield return new WaitForSeconds(0.02f);
        int nextLevel;
        nextLevel = level + 1;
        LevelManager.Level = nextLevel switch
        {
            4 => 4,
            5 => 5,
            _ => LevelManager.Level
        };
        SceneManager.LoadScene("Level-" + nextLevel);
    }
}
