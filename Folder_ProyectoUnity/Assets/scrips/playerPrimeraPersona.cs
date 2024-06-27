using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerPrimeraPersona : MonoBehaviour
{
    private Animator playeranim;
    public float hori, veti, speed;

    private Rigidbody rig;
    Transform player;
    private Vector2 newdirrecion;

    public Transform camaraAXIS;  // Punto donde la c�mara est� montada (generalmente la cabeza)
    private Transform thecamara;  // La c�mara principal

    private float rotY = 0f;  // Rotaci�n vertical de la c�mara
    private float rotX = 0f;  // Rotaci�n horizontal del jugador

    public float camararotacionspeed = 200f;  // Velocidad de rotaci�n de la c�mara
    public float minAngle = -45f;  // �ngulo m�nimo de rotaci�n vertical
    public float maxAngle = 45f;   // �ngulo m�ximo de rotaci�n vertical

    public bool pistol = false;  // Estado de la animaci�n del jugador

    void Start()
    {
        playeranim = GetComponentInChildren<Animator>();
        rig = GetComponent<Rigidbody>();
        player = this.transform;
        thecamara = Camera.main.transform;
        pistol = true;
        Cursor.lockState = CursorLockMode.Locked;  // Bloquear el cursor en el centro de la pantalla
    }

    void Update()
    {
        camaralogica();
        animacionlogica();

        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rig.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }

    public void animacionlogica()
    {
        playeranim.SetFloat("x", newdirrecion.x);
        playeranim.SetFloat("y", newdirrecion.y);
        playeranim.SetBool("pistol", pistol);
        if (pistol)
        {
            playeranim.SetLayerWeight(1, 1);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        hori = context.ReadValue<Vector2>().x;
        veti = context.ReadValue<Vector2>().y;
        Debug.Log("me muevo");
    }

    public void movimiento()
    {
        Vector3 direction = (transform.forward * veti + transform.right * hori).normalized;
        Vector3 velocity = direction * speed;
        velocity.y = rig.velocity.y;  // Mantener la velocidad vertical (gravedad)
        rig.velocity = velocity;

        newdirrecion = new Vector2(hori, veti);
    }

    void FixedUpdate()
    {
        movimiento();
    }

    public void camaralogica()
    {
        // Obtener el movimiento del rat�n
        float mousex = Input.GetAxis("Mouse X") * camararotacionspeed * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * camararotacionspeed * Time.deltaTime;

        // Rotar el jugador horizontalmente (en el eje Y)
        rotX += mousex;
        player.Rotate(0, rotX, 0);

        // Rotar la c�mara verticalmente (en el eje X) con l�mites
        rotY -= mousey;
        rotY = Mathf.Clamp(rotY, minAngle, maxAngle);

        camaraAXIS.localRotation = Quaternion.Euler(rotY, 0, 0);
    }

    // M�todo para verificar si el jugador est� en el suelo
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
