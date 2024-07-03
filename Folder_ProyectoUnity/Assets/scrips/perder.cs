using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class perder : MonoBehaviour
{
    public Image fadeImage;      
    public TextMeshProUGUI gameOverText;   
    public float fadeDuration = 2f; 

    private bool isGameOver = false;
    private float fadeTimer = 0f;
    void OnEnable()
    {
        GameManager.morir += mm;
    }

    void OnDisable()
    {
        GameManager.morir -= mm;
    }
    void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);

        Color textColor = gameOverText.color;
        textColor.a = 0f;
        gameOverText.color = textColor;
    }

    void Update()
    {
       
        if (isGameOver)
        {
            fadeTimer += Time.deltaTime;

            float progress = fadeTimer / fadeDuration;

            fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, progress);

            Color textColor = gameOverText.color;
            textColor.a = Mathf.Lerp(0f, 1f, progress);
            gameOverText.color = textColor;

            if (progress >= 1f)
            {
                isGameOver = false;
            }
        }
    }
    void mm()
    {
        isGameOver = true;
        fadeTimer = 0f;
        Time.timeScale = 0f;
    }

   
}
