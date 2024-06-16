using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiMenu : MonoBehaviour
{
    public GameObject fadeObject1;
    public GameObject fadeObject2;
    public float fadeDuration = 1.0f;
    public float fadeInDuration = 1.0f; 

    void Start()
    {
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
}