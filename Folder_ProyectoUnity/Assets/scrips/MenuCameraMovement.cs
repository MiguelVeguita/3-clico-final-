using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;

using DG.Tweening;
public class MenuCameraMovement : MonoBehaviour
{
    public Transform[] waypoints; 
    public float transitionDuration = 5f; 
    public float delayBetweenPoints = 2f; 
    public bool loop = true; 

    private int currentWaypointIndex = 0; 

    void Start()
    {
        
        MoveToNextWaypoint();
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

       
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        
        transform.DOMove(waypoints[currentWaypointIndex].position, transitionDuration)
                 .SetEase(Ease.InOutSine)
                 .OnComplete(() =>
                 {
                     if (loop || currentWaypointIndex < waypoints.Length - 1)
                     {
                         MoveToNextWaypoint();
                     }
                 });
    }
}