using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class perder : MonoBehaviour
{
    public Image fadeImage;       // La imagen que cambiará de transparente a negro.
    public TextMeshProUGUI gameOverText;    // El texto que mostrará "Perdiste".
    public float fadeDuration = 2f; // Duración de la transición.

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
        // Asegúrate de que la imagen comience transparente.
        fadeImage.color = new Color(0, 0, 0, 0); // Color negro con alfa 0 (totalmente transparente).

        // Inicialmente, el texto es transparente.
        Color textColor = gameOverText.color;
        textColor.a = 0f;
        gameOverText.color = textColor;
    }

    void Update()
    {
        // Si el juego está en estado de 'game over', inicia la transición.
        if (isGameOver)
        {
            // Incrementar el temporizador de la transición.
            fadeTimer += Time.deltaTime;

            // Calcular el progreso de la transición.
            float progress = fadeTimer / fadeDuration;

            // Interpolación de transparente a negro.
            fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, progress);

            // Hacer visible el texto "Perdiste" gradualmente.
            Color textColor = gameOverText.color;
            textColor.a = Mathf.Lerp(0f, 1f, progress);
            gameOverText.color = textColor;

            // Detener la transición cuando se complete.
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
