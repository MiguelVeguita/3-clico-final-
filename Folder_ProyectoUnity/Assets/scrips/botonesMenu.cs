using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class botonesMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleAmount = 1.2f;  // La cantidad en la que se escalará el botón
    public float animationDuration = 0.2f;  // La duración de la animación

    private Vector3 originalScale;  // Para almacenar la escala original del botón

    void Start()
    {
        // Almacenar la escala original del botón
        originalScale = transform.localScale;
    }

    // Este método se llama cuando el puntero entra en el área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Animar la escala del botón para hacerlo más grande
        transform.DOScale(originalScale * scaleAmount, animationDuration).SetEase(Ease.OutBounce);
    }

    // Este método se llama cuando el puntero sale del área del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        // Animar la escala del botón para que vuelva a su tamaño original
        transform.DOScale(originalScale, animationDuration).SetEase(Ease.OutBounce);
    }
    public void loadjuego()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
