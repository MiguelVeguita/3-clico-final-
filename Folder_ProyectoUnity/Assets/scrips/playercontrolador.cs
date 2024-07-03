using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;


public class playercontrolador : MonoBehaviour
{
    public float hori, veti, speed, vida;
    private Rigidbody rig;
    Transform player;

    public AudioClip sonbala, pasos;
    private AudioSource audioplayer;
    private bool isWalking = false;

    public float camararotacionspeed = 500f;
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public float caramaspeed = 500f;

    public GameObject bala;
    public Transform spaw;
    public float fuerzabala = 1500f;
    public float ratio = 0.5f;

    private float shottime = 0f;
    public bool listo=false;
    public static event Action morir;
    void OnEnable()
    {
        GameManager.yes += la;
    }

    void OnDisable()
    {
        GameManager.yes -= la;
    }
    void Start()
    {
        audioplayer = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody>();
        player = this.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > shottime)
            {
                audioplayer.PlayOneShot(sonbala);
                GameObject balanueva;
                balanueva = Instantiate(bala, spaw.position, spaw.rotation);
                balanueva.GetComponent<Rigidbody>().AddForce(spaw.forward * fuerzabala);
                shottime = Time.time + ratio;
                Destroy(balanueva, 2);
            }
        }

        HandleFootstepsSound();
    }
    public void la()
    {
       listo = true;
    }
    public void OnMoment(InputAction.CallbackContext context)
    {
        hori = context.ReadValue<Vector3>().x;
        veti = context.ReadValue<Vector3>().z;
        Debug.Log("me muevo");
    }

    public void movimiento()
    {
        Vector3 velocy = Vector3.zero;
        if (hori != 0 || veti != 0)
        {
            Vector3 direction = (transform.forward * veti + transform.right * hori);
            velocy = direction * speed;
        }
        velocy.y = rig.velocity.y;
        rig.velocity = velocy;
    }

    private void FixedUpdate()
    {
        movimiento();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("zombi"))
        {
            vida = vida - 1;
            if(vida <= 0)
            {
                //morir?.Invoke();
            }
        
        }
        if (collision.gameObject.CompareTag("carro"))
        {
            if (listo==true)
            {
                Cursor.lockState = CursorLockMode.Confined;
                SceneManager.LoadScene("ganaste");
            }
        }

    }

    private void HandleFootstepsSound()
    {
        bool isMoving = (hori != 0 || veti != 0);

        if (isMoving && !isWalking)
        {
            isWalking = true;
            audioplayer.clip = pasos;
            audioplayer.loop = true;
            audioplayer.Play();
        }
        else if (!isMoving && isWalking)
        {
            isWalking = false;
            audioplayer.Stop();
        }
    }
}
