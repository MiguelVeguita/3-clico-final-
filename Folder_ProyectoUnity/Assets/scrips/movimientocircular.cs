using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientocircular : MonoBehaviour
{
    [SerializeField] private float radio = 5f; 
    [SerializeField] private float velocidadAngular = 1f; 

    private Vector3 posicionInicial;

    private void Start()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        float x = Mathf.Cos(Time.time * velocidadAngular) * radio;
        float z = Mathf.Sin(Time.time * velocidadAngular) * radio;

        transform.position = posicionInicial + new Vector3(x, 0, z);
    }
}
