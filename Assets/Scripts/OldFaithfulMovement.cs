using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldFaithfulMovement : MonoBehaviour
{

    public float tforce;                //float value that modifies the amount of torque force applied to OldFaithful
    public float speed;                 //float value that modifies the amount of force applied when OldFaithful is moving
    public float maxtorque;             //float value that modifies how fast OldFaithful can spin
    public bool damageseq;              //determines if the ship is in the damage sequence
    public int hp;                      //the amount of health that the OldFaithful has
    public Object bullet;               //bullet prefab
    public Object destruction;          //the particle effect on destruction of the ship
    public Transform projectilespawn;   //transform child where projectiles will spawn
    private Rigidbody2D rb;             //RigidBody2D component attached
    private Animator anim;              //animator componenet attached
    private AudioSource shoot;          //the attached audiosource component for the shoot sound
    private AudioSource explode;        //the attached audiosource component for exploding

    // Damage is called when ship takes damage
    public IEnumerator Damage()
    {
        damageseq = true;                                                                   //damageseq is set to true to show ship is in damage sequence
        hp -= 1;                                                                            //health is decremented by 1
        Instantiate(destruction, transform.position, transform.rotation);                   //the destruction particle is instanced at the current position
        explode.Play();                                                                     //play the explode sound
        GetComponent<SpriteRenderer>().enabled = false;                                     //the SpriteRenderer component is disabled
        GetComponent<Collider2D>().enabled = false;                                         //the Collider2D component is disabled

        //if hp is not equal to -1...
        if (hp != -1)
        {
            yield return new WaitForSeconds(1);                                                 //Wait 1 second
            rb.velocity = Vector2.zero;                                                         //set the RigidBody2D's component's velocityx and velocityy to 0
            rb.angularVelocity = 0.0f;                                                          //reset the angular velocity
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);                              //reset the rotation
            GameObject asteroidspawner = GameObject.FindGameObjectWithTag("AsteroidSpawner");   //GameObject asteroidspawner is set equal to the ameObject witht eh "AsteroidSpawner" tag
            asteroidspawner.GetComponent<AsteroidSpawner>().enabled = false;                    //asteroidspawner's AsteroidSpawner script is disabled
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);                                 //the ship's position is reset to origin
            GetComponent<SpriteRenderer>().enabled = true;                                      //the SpriteRenderer Component is enabled
            GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, .5f);      //the SpriteRenderer's color is set to semitransparent
            GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");             //GameObject[] asteroids is set equal to all GameObjects with the "Asteroid" tag
            GameObject[] magnetmines = GameObject.FindGameObjectsWithTag("MagnetMine");

            //iterates integer i until it is less than the length of asteroids
            for (int i = 0; i < asteroids.Length; ++i)
            {
                Destroy(asteroids[i]);  //destroy asteroid at index i
            }
            for (int i = 0; i < magnetmines.Length; ++i)
            {
                Destroy(magnetmines[i]);  //destroy asteroid at index i
            }

            yield return new WaitForSeconds(3);                                             //wait 3 seconds
            rb.velocity = Vector2.zero;                                                     //set the RigidBody2D's component's velocityx and velocityy to 0
            asteroidspawner.GetComponent<AsteroidSpawner>().enabled = true;                 //asteroidspawner's AsteroidSpawner script is enabled
            GetComponent<Collider2D>().enabled = true;                                      //the Collider2DComponent is enabled
            GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f); //the color is set to opaque
            damageseq = false;                                                              //damageseq is set to false to show ship has ended the damage sequence
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();           //rb is set equal to the attached Rigidbody2D component
        anim = GetComponent<Animator>();            //anim is set equal to the attached animator component
        shoot = GetComponents<AudioSource>()[0];    //shoot is set equal to the attached AudioSource component for shooting
        explode = GetComponents<AudioSource>()[1];  //explode is set equal to the attached AudioSource component for exploding
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
            shoot.Play();                                                                   //play the fire sound
            Instantiate(bullet, projectilespawn.position, projectilespawn.rotation);        //fire a bullet
        }
    }

    //Called when a collision occurs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the object being collided with's tag is "Asteroid"...
        if (collision.collider.tag == "Asteroid")
        {
            StartCoroutine(Damage());   //...initiate the Damage() function
        }else if(collision.collider.tag == "MagnetMine")
        {
            MagneMineBehavior magnetmine = collision.collider.GetComponent<MagneMineBehavior>();
            magnetmine.Explode();
            StartCoroutine(Damage());   
        }
        else if (collision.collider.tag == "HPPickup")
        {
            if (hp < 4)
            {
                hp += 1;
            }
            Destroy(collision.collider.gameObject);
        }
    }
}
