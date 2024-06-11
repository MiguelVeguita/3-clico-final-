using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text texto; // Referencia al componente de texto que deseas animar (TextMeshPro)
    [SerializeField] private float escalaMaxima = 1.5f; // Escala m�xima a la que se agrandar� el texto
    [SerializeField] private float duracionAgrandar = 1f; // Duraci�n de la animaci�n de agrandar
    [SerializeField] private float duracionEncoger = 1f; // Duraci�n de la animaci�n de encoger

    private void Start()
    {
        // Iniciamos la animaci�n de agrandar y encoger
        AgrandarYEncoger();
    }

    private void AgrandarYEncoger()
    {
        // Animaci�n de agrandar el texto
        texto.transform.DOScale(escalaMaxima, duracionAgrandar)
            .SetEase(Ease.OutQuad) // Cambia el Ease si prefieres otro tipo de interpolaci�n
            .SetUpdate(true) // Aseguramos que la animaci�n se actualice incluso cuando el Time.timeScale es 0
            .OnComplete(() =>
            {
                // Una vez que se completa la animaci�n de agrandar, iniciamos la animaci�n de encoger
                EncogerTexto();
            });
    }

    private void EncogerTexto()
    {
        // Animaci�n de encoger el texto
        texto.transform.DOScale(1f, duracionEncoger)
            .SetEase(Ease.InQuad) // Cambia el Ease si prefieres otro tipo de interpolaci�n
            .SetUpdate(true) // Aseguramos que la animaci�n se actualice incluso cuando el Time.timeScale es 0
            .OnComplete(() =>
            {
                // Una vez que se completa la animaci�n de encoger, iniciamos la animaci�n de agrandar nuevamente
                AgrandarYEncoger();
            });
    }
}
