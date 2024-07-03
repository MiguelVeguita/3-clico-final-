using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objetosgirar : MonoBehaviour
{
    public float duracionEncogimiento = 1f; // Duración del encogimiento en segundos
    public float duracionRotacion = 0.5f; // Duración de la rotación en segundos
    public Vector3 escalaFinal = new Vector3(0.5f, 0.5f, 0.5f); // Escala final a la que se encojerá
    public Ease easeType = Ease.OutQuad; // Tipo de curva de animación

    private Vector3 escalaInicial; // Escala inicial del objeto

    void Start()
    {
        escalaInicial = transform.localScale; // Guardar la escala inicial del objeto
        InvokeRepeating("EjecutarAnimacion", 0f, 2f); // Llama a EjecutarAnimacion cada 5 segundos
    }

    void EjecutarAnimacion()
    {
        // Encoger el objeto
        transform.DOScale(escalaFinal, duracionEncogimiento)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                // Rotar el objeto en su lugar
                transform.DORotate(new Vector3(0f, 360f, 0f), duracionRotacion, RotateMode.LocalAxisAdd)
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        // Restaurar la escala original del objeto
                        transform.DOScale(escalaInicial, duracionEncogimiento)
                            .SetEase(easeType);
                    });
            });
    }
}
