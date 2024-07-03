using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // El prefab del objeto que quieres instanciar
    public Transform respawnPoint; // El punto de respawn en la escena

    public float spawnInterval = 5f; // Intervalo de tiempo entre cada respawn
    private float nextSpawnTime;

    void Start()
    {
        // Inicializa el tiempo para el primer respawn
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        // Verifica si es el momento de instanciar un nuevo objeto
        if (Time.time >= nextSpawnTime)
        {
            // Instancia el objeto en el punto de respawn
            SpawnObject();

            // Actualiza el tiempo para el próximo respawn
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnObject()
    {
        if (objectToSpawn != null && respawnPoint != null)
        {
            // Instancia el objeto en la posición y rotación del punto de respawn
            Instantiate(objectToSpawn, respawnPoint.position, respawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Faltan asignar el objeto a instanciar o el punto de respawn.");
        }
    }
}
