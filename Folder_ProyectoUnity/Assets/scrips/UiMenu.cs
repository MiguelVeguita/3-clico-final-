using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UiMenu : MonoBehaviour
{
    public GameObject fadeObject1;
    public GameObject fadeObject2;
    public float fadeDuration = 1.0f;
    public float fadeInDuration = 1.0f;

    public RectTransform mainContainer; // Contenedor principal que contiene todos los paneles
    public float transitionDuration = 1f; // Duración de la transición en segundos

    private Vector2 mainContainerStartPos; // Posición inicial del contenedor principal
    private Vector2 creditsTargetPosition; // Posición objetivo para el desplazamiento de créditos
    private Vector2 optionsTargetPosition;
    void Start()
    {
        mainContainerStartPos = mainContainer.anchoredPosition;

        // Calcula la posición objetivo para el panel de créditos (sube una pantalla completa)
        creditsTargetPosition = new Vector2(mainContainerStartPos.x, mainContainerStartPos.y + Screen.height);

        // Calcula la posición objetivo para el panel de opciones (mueve hacia la izquierda una pantalla completa)
        optionsTargetPosition = new Vector2(mainContainerStartPos.x - Screen.width, mainContainerStartPos.y);
    

    fadeObject1.SetActive(true);
        fadeObject2.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StartCoroutine(FadeTransition());
        }
    }

    IEnumerator FadeTransition()
    {
        CanvasGroup canvasGroup1 = fadeObject1.GetComponent<CanvasGroup>();
        if (canvasGroup1 == null)
        {
            canvasGroup1 = fadeObject1.AddComponent<CanvasGroup>();
        }
        canvasGroup1.alpha = 1f;

        canvasGroup1.DOFade(0f, fadeDuration)
            .OnComplete(() =>
            {
                fadeObject1.SetActive(false);
                fadeObject2.SetActive(true);

                CanvasGroup canvasGroup2 = fadeObject2.GetComponent<CanvasGroup>();
                if (canvasGroup2 == null)
                {
                    canvasGroup2 = fadeObject2.AddComponent<CanvasGroup>();
                }
                canvasGroup2.alpha = 0f; 

                canvasGroup2.DOFade(1f, fadeInDuration); 
            });

        yield return new WaitForSeconds(fadeDuration);
    }

    public void credi()
    {
        mainContainer.DOAnchorPos(creditsTargetPosition, transitionDuration)
                     .SetEase(Ease.InOutSine).SetUpdate(true);
    }

    public void regrecredi()
    {
        mainContainer.DOAnchorPos(mainContainerStartPos, transitionDuration)
                     .SetEase(Ease.InOutSine).SetUpdate(true);
    }
    public void ShowOptions()
    {
        // Mueve el contenedor principal hacia la izquierda para mostrar el panel de opciones
        mainContainer.DOAnchorPos(optionsTargetPosition, transitionDuration)
                     .SetEase(Ease.InOutSine).SetUpdate(true);
    }

    // Método para ocultar el panel de opciones
    public void HideOptions()
    {
        // Mueve el contenedor principal de vuelta a su posición inicial para mostrar el menú principal
        mainContainer.DOAnchorPos(mainContainerStartPos, transitionDuration)
                     .SetEase(Ease.InOutSine).SetUpdate(true);
    }
    public void salirrr()
    {
        Application.Quit();
    }
}