using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class perder : MonoBehaviour
{
    public Image fadeImage;       // La imagen que cambiar� de transparente a negro.
    public TextMeshProUGUI gameOverText;    // El texto que mostrar� "Perdiste".
    public float fadeDuration = 2f; // Duraci�n de la transici�n.

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
        // Aseg�rate de que la imagen comience transparente.
        fadeImage.color = new Color(0, 0, 0, 0); // Color negro con alfa 0 (totalmente transparente).

        // Inicialmente, el texto es transparente.
        Color textColor = gameOverText.color;
        textColor.a = 0f;
        gameOverText.color = textColor;
    }

    void Update()
    {
        // Si el juego est� en estado de 'game over', inicia la transici�n.
        if (isGameOver)
        {
            // Incrementar el temporizador de la transici�n.
            fadeTimer += Time.deltaTime;

            // Calcular el progreso de la transici�n.
            float progress = fadeTimer / fadeDuration;

            // Interpolaci�n de transparente a negro.
            fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, progress);

            // Hacer visible el texto "Perdiste" gradualmente.
            Color textColor = gameOverText.color;
            textColor.a = Mathf.Lerp(0f, 1f, progress);
            gameOverText.color = textColor;

            // Detener la transici�n cuando se complete.
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
