using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class botonesMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleAmount = 1.2f;  // La cantidad en la que se escalar� el bot�n
    public float animationDuration = 0.2f;  // La duraci�n de la animaci�n

    private Vector3 originalScale;  // Para almacenar la escala original del bot�n

    void Start()
    {
        // Almacenar la escala original del bot�n
        originalScale = transform.localScale;
    }

    // Este m�todo se llama cuando el puntero entra en el �rea del bot�n
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Animar la escala del bot�n para hacerlo m�s grande
        transform.DOScale(originalScale * scaleAmount, animationDuration).SetEase(Ease.OutBounce);
    }

    // Este m�todo se llama cuando el puntero sale del �rea del bot�n
    public void OnPointerExit(PointerEventData eventData)
    {
        // Animar la escala del bot�n para que vuelva a su tama�o original
        transform.DOScale(originalScale, animationDuration).SetEase(Ease.OutBounce);
    }
    public void loadjuego()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
