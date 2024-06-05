using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontrolador : MonoBehaviour
{
    private Animator playeranim;
    public float hori, veti,speed;

    private Rigidbody rig;
    Transform player;
    private Vector2 newdirrecion;

    public Transform camaraAXIS;
    public Transform camaraTRAK;
    private Transform thecamara;

    private float rotY = 0f;
    private float rotX = 0f;


    public float camararotacionspeed = 200f;
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public float caramaspeed=200f;

    public bool pistol=false;
   
    // Start is called before the first frame update
    void Start()
    {

        playeranim = GetComponentInChildren<Animator>();
        rig = GetComponent<Rigidbody>();
        player = this.transform;
        thecamara = Camera.main.transform;
        pistol = true;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        camaralogica();
        movimiento();
        animacionlogica();
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
    public void OnMoment(InputAction.CallbackContext context)
    {
        hori = context.ReadValue<Vector3>().x;
        veti = context.ReadValue<Vector3>().z;
        Debug.Log("me muevo");

    }
    public void movimiento()
    {
       
        Vector3 dirrecion = rig.velocity;

        float thetime = Time.deltaTime;

        newdirrecion = new Vector2(hori, veti);

        Vector3 side = speed * hori * thetime * player.right;
        Vector3 forward = speed * veti * thetime * player.forward;
        Vector3 finaldirecion = side + forward;
        rig.velocity = finaldirecion;

    }
    private void FixedUpdate()
    {
       // rig.velocity = new Vector3(hori *10, rig.velocity.y, veti * 10);
    }
    public void camaralogica()
    {
        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");
        float thetime = Time.deltaTime;

        rotY += mousey * thetime * camararotacionspeed;
        rotX = mousex * thetime * camararotacionspeed;

        player.Rotate(0, rotX, 0);

        rotY = math.clamp(rotY, minAngle, maxAngle);

        Quaternion localrotacion = Quaternion.Euler(-rotY, 0, 0);
        camaraAXIS.localRotation = localrotacion;

        thecamara.position = Vector3.Lerp(thecamara.position, camaraTRAK.position, caramaspeed * thetime);
        thecamara.rotation = Quaternion.Lerp(thecamara.rotation, camaraTRAK.rotation, caramaspeed * thetime);


    }
}
