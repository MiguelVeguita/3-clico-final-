using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balacontroler : MonoBehaviour
{
    Rigidbody balarig;
    public float balapoder = 0f;
    public float life = 4f;
    float time = 0f;
    void Start()
    {
        balarig = GetComponent<Rigidbody>();
        balarig.velocity = this.transform.forward * balapoder;
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time >= life)
        {
            Destroy(this.gameObject);
        }
    }
}
