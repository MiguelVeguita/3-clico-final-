using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistolcontroler : MonoBehaviour
{
    public Transform shootspaw;
    public bool shoting = false;
    public GameObject balaprefat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(shootspaw.position, shootspaw.forward * 10f, Color.blue);
        Debug.DrawLine( Camera.main.transform.position, Camera.main.transform.forward * 10f, Color.red);

        RaycastHit camerahit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out camerahit))
        {
            Vector3 shotdirecion = camerahit.point - shootspaw.position;
            shootspaw.rotation = Quaternion.LookRotation(shotdirecion);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                shottt();
            }
        }
    }
    public void shottt()
    {
        Instantiate(balaprefat, shootspaw.position, shootspaw.rotation);
    }
}
