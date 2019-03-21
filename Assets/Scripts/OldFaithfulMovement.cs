using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldFaithfulMovement : MonoBehaviour
{

    public float Tforce;
    public float speed;
    public float maxTorque;
    public Object bullet;
    public Transform projectileSpawn;
    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * speed);
            anim.SetBool("Thrust", true);
        }
        else
        {
            anim.SetBool("Thrust", false);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, projectileSpawn.position, projectileSpawn.rotation);
        }
    }
}
