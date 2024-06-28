using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PAUSA : MonoBehaviour
{
    public GameObject PAUS, listaene,sonidos;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        PAUS.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                desactivar();
            }
            else
            {
                activar();
            }
        }
    }
    public void activar()
    {
        Time.timeScale = 0f;
        PAUS.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void desactivar()
    {
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        PAUS.SetActive(false);

    }
    public void listaenemys()
    {
        sonidos.SetActive(false);
        listaene.SetActive(true);
        
    }
    public void song()
    {
        sonidos.SetActive(true);
        listaene.SetActive(false);
    }
    public void inicio()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("inicio");
    }
}
