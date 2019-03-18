using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldFaithfulMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Tforce;
    public float speed;
    public float maxTorque;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * speed);
        }
        rb.AddTorque(Input.GetAxis("Horizontal") * Tforce);
        if(rb.angularVelocity > maxTorque)
        {
            rb.angularVelocity = maxTorque;
        }
        if(rb.angularVelocity < -maxTorque)
        {
            rb.angularVelocity = -maxTorque;
        }
        if(transform.position.y <= -6.35)
        {
            transform.position = new Vector3(transform.position.x, 6.2f, transform.position.z);
        }
        if(transform.position.y >= 6.35)
        {
            transform.position = new Vector3(transform.position.x, -6.2f, transform.position.z);
        }
        if(transform.position.x >= 10.9)
        {
            transform.position = new Vector3(-10.8f, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= -10.9)
        {
            transform.position = new Vector3(+10.8f, transform.position.y, transform.position.z);
        }
    }
}
