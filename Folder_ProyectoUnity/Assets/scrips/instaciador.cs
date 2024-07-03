using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciador : MonoBehaviour
{
    public GameObject objetoPrefab;
    public Transform puntoInstancia;
    public float intervalo = 5f; 

    void Start()
    {
        if (objetoPrefab != null && puntoInstancia != null)
        {
            InvokeRepeating("InstanciarCada5Segundos", 0f, intervalo);
        }
        else
        {
            Debug.LogError("Prefab o punto de instancia no asignados en el inspector.");
        }
    }

    void InstanciarCada5Segundos()
    {
        // Instanciar el objeto prefab en la posici�n y rotaci�n del punto de instancia
        GameObject instancia = Instantiate(objetoPrefab, puntoInstancia.position, puntoInstancia.rotation);

        // Opcional: Puedes hacer algo con la instancia, como modificar propiedades o almacenarla
        // Por ejemplo:
        // instancia.GetComponent<MiComponente>().ConfigurarAlgo();
    }
}
