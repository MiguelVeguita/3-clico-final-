using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objetosgirar : MonoBehaviour
{
    public float duracionEncogimiento = 1f;
    public float duracionRotacion = 0.5f; 
    public Vector3 escalaFinal = new Vector3(0.5f, 0.5f, 0.5f); 
    public Ease easeType = Ease.OutQuad; 

    private Vector3 escalaInicial; 
    void Start()
    {
        escalaInicial = transform.localScale; 
        InvokeRepeating("EjecutarAnimacion", 0f, 2f); 
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
