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
    private int level = 0;
    private bool isUnlock;
    private Button button;
    private Image image;

    private void OnEnable()
    {
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
        image.sprite = openSprite;
        StartCoroutine(WaitAndLoad());
    }
    
    IEnumerator WaitAndLoad()
    {   
        yield return new WaitForSeconds(0.7f);
        int nextLevel;
        nextLevel = level + 1;
        SceneManager.LoadScene("StartLevel" + nextLevel);
    }
}
