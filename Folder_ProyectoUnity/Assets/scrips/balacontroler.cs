using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balacontroler : MonoBehaviour
{
    Rigidbody balarig;
    public float balapoder = 0f;
    public float life = 4f;
    float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        balarig = GetComponent<Rigidbody>();
        balarig.velocity = this.transform.forward * balapoder;
    }

    // Update is called once per frame
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
