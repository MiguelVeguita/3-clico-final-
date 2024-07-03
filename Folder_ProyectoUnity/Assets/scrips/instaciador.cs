using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciador : MonoBehaviour
{
    public GameObject objetoPrefab; // Objeto que quieres instanciar
    public Transform puntoInstancia; // Punto desde donde quieres instanciar el objeto
    public float intervalo = 5f; // Intervalo en segundos entre cada instancia

    void Start()
    {
        // Verificar que el objeto prefab y el punto de instancia no sean nulos
        if (objetoPrefab != null && puntoInstancia != null)
        {
            // Llamar al método InstanciarCada5Segundos repetidamente cada 'intervalo' segundos
            InvokeRepeating("InstanciarCada5Segundos", 0f, intervalo);
        }
        else
        {
            Debug.LogError("Prefab o punto de instancia no asignados en el inspector.");
        }
    }

    void InstanciarCada5Segundos()
    {
        // Instanciar el objeto prefab en la posición y rotación del punto de instancia
        GameObject instancia = Instantiate(objetoPrefab, puntoInstancia.position, puntoInstancia.rotation);

        // Opcional: Puedes hacer algo con la instancia, como modificar propiedades o almacenarla
        // Por ejemplo:
        // instancia.GetComponent<MiComponente>().ConfigurarAlgo();
    }
}
