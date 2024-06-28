using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraPingPong : MonoBehaviour
{
    public Transform pointA; // Punto A
    public Transform pointB; // Punto B
    public float transitionDuration = 5f; // Duración del movimiento entre puntos
    public float delayAtPoints = 2f; // Retraso en cada punto

    void Start()
    {
        if (pointA != null && pointB != null)
        {
            StartCoroutine(MoveCameraLoop()); // Inicia la coroutine del movimiento en bucle
        }
        else
        {
            Debug.LogError("Por favor, asigna los puntos A y B en el inspector.");
        }
    }

    IEnumerator MoveCameraLoop()
    {
        while (true) // Bucle infinito
        {
            // Mueve la cámara de A a B
            transform.DOMove(pointB.position, transitionDuration)
                     .SetEase(Ease.InOutSine); // Movimiento suave

            yield return new WaitForSeconds(transitionDuration + delayAtPoints); // Espera el tiempo del movimiento + retraso

            // Mueve la cámara de B a A
            transform.DOMove(pointA.position, transitionDuration)
                     .SetEase(Ease.InOutSine); // Movimiento suave

            yield return new WaitForSeconds(transitionDuration + delayAtPoints); // Espera el tiempo del movimiento + retraso
        }
    }
}
