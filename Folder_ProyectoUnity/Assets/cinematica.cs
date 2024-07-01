using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class cinematica : MonoBehaviour
{
    private AudioSource au;
    public AudioClip clip;
    public float cambio=0;
    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        au.clip = clip;

    }

    // Update is called once per frame
    void Update()
    {
        cambio=cambio+Time.deltaTime;
        if (cambio >= 10.5)
        {
            cambiar();
        }
    }
    public void cambiar()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
