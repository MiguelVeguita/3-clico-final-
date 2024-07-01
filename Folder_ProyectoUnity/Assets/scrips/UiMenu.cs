using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UiMenu : MonoBehaviour
{
    public GameObject fadeObject1;
    public GameObject fadeObject2;
    public GameObject MENU;
    public GameObject OPT;  
    public GameObject CREDI;
    public float fadeDuration = 1.0f;
    public float fadeInDuration = 1.0f;

    public RectTransform mainContainer; // Cambiado a RectTransform
    public Vector2 targetYposi;  // Cambiado a Vector2
    public Vector2 targetYnega;  // Cambiado a Vector2
    public Vector2 targetXposi;  // Cambiado a Vector2
    public Vector2 targetXnega;  // Cambiado a Vector2

    public float duration = 1f;
    public Ease easeType = Ease.InOutQuad;

    void Start()
    {
        fadeObject1.SetActive(true);
        fadeObject2.SetActive(false);
        OPT.SetActive(false);
        CREDI.SetActive(false);
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
       CREDI.SetActive(true);
        MENU.SetActive(false);
    }

    public void regrecredi()
    {
        CREDI.SetActive(false);
        MENU.SetActive(true);
    }

    public void ShowOptions()
    {
       OPT.SetActive(true);
        MENU.SetActive(false);
    }

    public void HideOptions()
    {
        OPT.SetActive(false);
        MENU.SetActive(true);

    }

    public void salirrr()
    {
        Application.Quit();
    }
}
