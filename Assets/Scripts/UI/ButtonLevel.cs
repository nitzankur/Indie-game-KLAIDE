using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    [SerializeField] private Sprite lockSprite;
    [SerializeField] private TextMeshProUGUI levelText;
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
        levelText.text = level.ToString();
        if (isUnlock)
        {
            this.isUnlock = true;
            image.sprite = null;
            button.enabled = true;
            levelText.gameObject.SetActive(true);
        }
        else
        {
            image.sprite = lockSprite;
            button.enabled = false;
            levelText.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        if (isUnlock)
        {
            int nextLevel;
            nextLevel = level + 1;
            SceneManager.LoadScene("StartLevel" + nextLevel);
        }

       
    }
}
