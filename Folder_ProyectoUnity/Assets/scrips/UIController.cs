using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text texto; // Referencia al componente de texto que deseas animar (TextMeshPro)
    [SerializeField] private float escalaMaxima = 1.5f; // Escala máxima a la que se agrandará el texto
    [SerializeField] private float duracionAgrandar = 1f; // Duración de la animación de agrandar
    [SerializeField] private float duracionEncoger = 1f; // Duración de la animación de encoger

    private void Start()
    {
        // Iniciamos la animación de agrandar y encoger
        AgrandarYEncoger();
    }

    private void AgrandarYEncoger()
    {
        // Animación de agrandar el texto
        texto.transform.DOScale(escalaMaxima, duracionAgrandar)
            .SetEase(Ease.OutQuad) // Cambia el Ease si prefieres otro tipo de interpolación
            .SetUpdate(true) // Aseguramos que la animación se actualice incluso cuando el Time.timeScale es 0
            .OnComplete(() =>
            {
                // Una vez que se completa la animación de agrandar, iniciamos la animación de encoger
                EncogerTexto();
            });
    }

    private void EncogerTexto()
    {
        // Animación de encoger el texto
        texto.transform.DOScale(1f, duracionEncoger)
            .SetEase(Ease.InQuad) // Cambia el Ease si prefieres otro tipo de interpolación
            .SetUpdate(true) // Aseguramos que la animación se actualice incluso cuando el Time.timeScale es 0
            .OnComplete(() =>
            {
                // Una vez que se completa la animación de encoger, iniciamos la animación de agrandar nuevamente
                AgrandarYEncoger();
            });
    }
}
