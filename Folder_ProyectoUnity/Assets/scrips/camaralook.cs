using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class camaralook : MonoBehaviour
{
    public float mouseSensi = 150;

    public Transform playerr;

    float xrotacion = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousex=Input.GetAxis("Mouse X")*mouseSensi*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensi * Time.deltaTime;
        xrotacion -= mouseY;
        xrotacion=Mathf.Clamp(xrotacion, -90, 90);
        transform.localRotation = Quaternion.Euler(xrotacion, 0f, 0f);
        playerr.Rotate(Vector3.up * mousex);



    }
}
