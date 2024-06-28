using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontrolador : MonoBehaviour
{
   
    public float hori, veti, speed,vida;

    private Rigidbody rig;
    Transform player;
 
    //private float rotY = 0f;
   // private float rotX = 0f;


    public float camararotacionspeed = 200f;
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public float caramaspeed = 200f;

    public GameObject bala;
    public Transform spaw;
    public float fuerzabala = 1500f;
    public float ratio = 0.5f;

    private float shottime = 0f;

    // Start is called before the first frame update
    void Start()
    {

        rig = GetComponent<Rigidbody>();
        player = this.transform;
     
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > shottime)
            {
                GameObject balanueva;
                balanueva=Instantiate(bala,spaw.position,spaw.rotation);
                balanueva.GetComponent<Rigidbody>().AddForce(spaw.forward * fuerzabala);
                shottime = Time.time+ratio;
                Destroy(balanueva, 2);

            }
        }

      /*  if (Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }*/
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
        if (collision.gameObject.tag == "zombi")
        {
            vida = vida - 1;
           /* if (vida < 0)
            {
                
            }*/
        }
    }

}
