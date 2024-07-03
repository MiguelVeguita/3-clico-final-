using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraPingPong : MonoBehaviour
{
    public Transform[] waypoints; // Array de puntos de referencia
    public float moveDuration = 2.0f; // Duraci�n para mover entre puntos
    public Ease moveEase = Ease.Linear; // Tipo de suavizado para el movimiento

    private int currentWaypointIndex = 0; // �ndice del waypoint actual
    private bool movingForward = true; // Direcci�n de movimiento

    void Start()
    {
        if (waypoints.Length > 0)
        {
            // Iniciar el movimiento a trav�s de los waypoints
            MoveToNextWaypoint();
        }
        else
        {
            Debug.LogWarning("No se han asignado waypoints.");
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        // Mueve la c�mara al siguiente waypoint
        transform.DOMove(waypoints[currentWaypointIndex].position, moveDuration)
            .SetEase(moveEase)
            .OnComplete(OnWaypointReached); // Al completar, llama a OnWaypointReached
    }

    void OnWaypointReached()
    {
        // Verificar si estamos en el �ltimo waypoint movi�ndonos hacia adelante
        if (movingForward)
        {
            if (currentWaypointIndex < waypoints.Length - 1)
            {
                currentWaypointIndex++;
            }
            else
            {
                // Cambiar la direcci�n a hacia atr�s
                movingForward = false;
                currentWaypointIndex--;
            }
        }
        else // Si estamos movi�ndonos hacia atr�s
        {
            if (currentWaypointIndex > 0)
            {
                currentWaypointIndex--;
            }
            else
            {
                // Cambiar la direcci�n a hacia adelante
                movingForward = true;
                currentWaypointIndex++;
            }
        }

        // Llamar de nuevo para mover al siguiente waypoint
        MoveToNextWaypoint();
    }

    // M�todo opcional para detener el movimiento de la c�mara
    public void StopCameraMovement()
    {
        DOTween.Kill(transform);
    }

    // M�todo opcional para iniciar el movimiento desde el principio
    public void RestartCameraMovement()
    {
        currentWaypointIndex = 0; // Reiniciar el �ndice al primer waypoint
        movingForward = true; // Reiniciar la direcci�n a hacia adelante
        MoveToNextWaypoint(); // Llamar para iniciar el movimiento
    }
}
