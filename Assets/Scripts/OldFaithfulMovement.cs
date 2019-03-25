using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldFaithfulMovement : MonoBehaviour
{

    public float tforce;                //float value that modifies the amount of torque force applied to OldFaithful
    public float speed;                 //float value that modifies the amount of force applied when OldFaithful is moving
    public float maxtorque;             //float value that modifies how fast OldFaithful can spin
    public Object bullet;               //bullet prefab
    public Transform projectilespawn;   //transform child where projectiles will spawn
    private Rigidbody2D rb;             //RigidBody2D component attached
    private Animator anim;              //animator componenet attached

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //rb is set equal to the attached Rigidbody2D component
        anim = GetComponent<Animator>();    //anim is set equal to the attached animator component
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))   //if 'UpArrow' or 'W' is pressed...
        {
            rb.AddForce(transform.up * speed);                              //Addforce in the upward direction times speed
            anim.SetBool("Thrust", true);                                   //Set the "Thrust" boolean in the anim variable to true
        }
        else                                                            //if not...
        {
            anim.SetBool("Thrust", false);                                  //Set the "Thrust" boolean in the anim variable to false
        }
        rb.AddTorque(Input.GetAxis("Horizontal") * tforce);     //Add torque to Oldfaithful equal to the horizontal input (left/right, A/D) times tforce
        if(rb.angularVelocity > maxtorque)      //if the angular velocity is greater than maxtorque...
        {
            rb.angularVelocity = maxtorque;         //set the angular velocity equal to maxtorque
        }
        if(rb.angularVelocity < -maxtorque)     //if the angular velocity is less than negative maxtorque...
        {
            rb.angularVelocity = -maxtorque;        //set the angular velocity equal to negative maxtorque
        }
        if (Input.GetKeyDown(KeyCode.Space))                                            //if the space key is pressed...
        {
            Instantiate(bullet, projectilespawn.position, projectilespawn.rotation);        //fire a bullet
        }
    }
}
