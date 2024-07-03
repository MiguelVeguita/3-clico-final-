using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraPingPong : MonoBehaviour
{
    public Transform[] waypoints; // Array de puntos de referencia
    public float moveDuration = 2.0f; // Duración para mover entre puntos
    public Ease moveEase = Ease.Linear; // Tipo de suavizado para el movimiento

    private int currentWaypointIndex = 0; // Índice del waypoint actual
    private bool movingForward = true; // Dirección de movimiento

    void Start()
    {
        if (waypoints.Length > 0)
        {
            // Iniciar el movimiento a través de los waypoints
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

        // Mueve la cámara al siguiente waypoint
        transform.DOMove(waypoints[currentWaypointIndex].position, moveDuration)
            .SetEase(moveEase)
            .OnComplete(OnWaypointReached); // Al completar, llama a OnWaypointReached
    }

    void OnWaypointReached()
    {
        // Verificar si estamos en el último waypoint moviéndonos hacia adelante
        if (movingForward)
        {
            if (currentWaypointIndex < waypoints.Length - 1)
            {
                currentWaypointIndex++;
            }
            else
            {
                // Cambiar la dirección a hacia atrás
                movingForward = false;
                currentWaypointIndex--;
            }
        }
        else // Si estamos moviéndonos hacia atrás
        {
            if (currentWaypointIndex > 0)
            {
                currentWaypointIndex--;
            }
            else
            {
                // Cambiar la dirección a hacia adelante
                movingForward = true;
                currentWaypointIndex++;
            }
        }

        // Llamar de nuevo para mover al siguiente waypoint
        MoveToNextWaypoint();
    }

    // Método opcional para detener el movimiento de la cámara
    public void StopCameraMovement()
    {
        DOTween.Kill(transform);
    }

    // Método opcional para iniciar el movimiento desde el principio
    public void RestartCameraMovement()
    {
        currentWaypointIndex = 0; // Reiniciar el índice al primer waypoint
        movingForward = true; // Reiniciar la dirección a hacia adelante
        MoveToNextWaypoint(); // Llamar para iniciar el movimiento
    }
}
