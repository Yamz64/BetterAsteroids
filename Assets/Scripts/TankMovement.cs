using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed;             //float value speed stores the speed at which the tank will travel
    public bool damageseq;          //determines if the ship is in the damage sequence
    public int hp;                  //the amount of health that the tank has
    public Object crosshairo;       //the crosshair prefab
    public Object rocket;           //the rocket prefab
    public Object destruction;      //the particle effect on the destruction of the tank
    public Transform barrel;        //Transform barrel will be set equal to the barrel of the tank
    private Transform bulletspawn;  //Transform bulletspawn will be set equal to the location that rockets will come out of
    private Rigidbody2D rb;         //Rigidbody2D rb will be set equal to the attached Rigidbody2D component
    private GameObject crosshair;   //GameObject crosshair will be set equal the GameObject component of the active crosshair prefab
    private AudioSource explode;    //The AudioSource attached that plays the explode sound


    // Damage is called when ship takes damage
    public IEnumerator Damage()
    {
        damageseq = true;                                                                   //damageseq is set to true to show ship is in damage sequence
        hp -= 1;                                                                            //health is decremented by 1
        Instantiate(destruction, transform.position, transform.rotation);                   //the destruction particle is instanced at the current position
        explode.Play();                                                                     //play the exlosion sound
        GetComponent<SpriteRenderer>().enabled = false;                                     //the SpriteRenderer component is disabled
        barrel.GetComponent<SpriteRenderer>().enabled = false;                              //the SpriteRenderer attached to the barrel is disabled
        GetComponent<Collider2D>().enabled = false;                                         //the Collider2D component is disabled
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
            barrel.GetComponent<SpriteRenderer>().enabled = true;                               //the SpriteRenderer attached to the barrel is enabled
            GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, .5f);      //the SpriteRenderer's color is set to semitransparent
            barrel.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, .5f);//the barrel's SpriteRenderer's color is set to semitransparent
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
            barrel.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);//the barrel's color is set to opaque
            damageseq = false;                                                              //damageseq is set to false to show ship has ended the damage sequence
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;                                     //Hid e the mouse cursor
        Instantiate(crosshairo);                                    //Instance the crosshair prefab
        barrel = transform.GetChild(0);                             //barrel is set equal to the first child of the tank
        bulletspawn = barrel.transform.GetChild(0);                 //bulletspawn is set equal to the first child of the barrel
        rb = GetComponent<Rigidbody2D>();                           //rb is set equal to the attached Rigidbody2D component
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");  //crosshair is set equal to the GameObject in the scene with the "Crosshair" tag
        explode = GetComponent<AudioSource>();                      //explode is set equal to the attached AudioSource component
    }

    // Update is called once per frame
    void Update()
    {
        //if the uparrow or 'w' is pressed...
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * speed);  //...AddForce in the up direction equal to speed

            //if the rightarrow or 'd' is pressed...
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, -45.0f);    //...point NE
            }
            //if the leftarrow or 'a' is pressed...
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))    //...point NW
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
            }
            //if the 2 above statements are false (only the uparrow or 'w' is pressed)...
            else
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);  //...point N
            }
        }
        //if the above statement is not true and the downarrow or 's' is pressed...
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.up * speed);      //...AddForce in the up direction equal to speed

            //if the rightarrow or 'd' is pressed...
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, -135.0f);   //...point SE
            }
            //if the leftarrow or 'a' is pressed...
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 135.0f);    //...point SW
            }
            //if the 2 above statements are false (only the downarrow or 's' is pressed)...
            else
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);    //...point S
            }
        }
        //if the above statement is not true and the rightarrow or the 'd' key is pressed...
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.up * speed);      //...AddForce in the up direction equal to speed
            transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);    //...Point E
        }
        //if the above statement is not true and the leftarrow or the 'a' key is pressed...
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.AddForce(transform.up * speed);      //...AddForce in the up direction equal to speed
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);     //...point W
        }
        float distancex = crosshair.transform.position.x - barrel.position.x;   //temporary float distancex is set equal to difference in x positions of the crosshair and rocket
        float distancey = crosshair.transform.position.y - barrel.position.y;   //temporary float distancey is set equal to difference in y positions of the crosshair and rocket
        float angle = -Mathf.Rad2Deg * Mathf.Atan(distancex / distancey);   //the angle between the barrel and the crosshair is calculated in degrees

        //if the crosshair is above the rocket...
        if (crosshair.transform.position.y >= barrel.transform.position.y)
        {
            barrel.eulerAngles = new Vector3(0.0f, 0.0f, angle);    //...set the barrel's eulerAngles equal to (0.0f, 0.0f angle) to point at the crosshair
        }
        else   //if the above statment is not true (the crosshair is below the barrel)...
        {
            barrel.eulerAngles = new Vector3(0.0f, 0.0f, angle+180);    //...set the rocket's eulerAngles equal to (0.0f, 0.0f, angle + 180) to point at the crosshair
        }
        //if the "Fire1" button is pressed...
        if(Input.GetButton("Fire1"))
        {
            //if there is no rocket...
            if (GameObject.FindGameObjectWithTag("Rocket") == null)
            {
                Instantiate(rocket, bulletspawn.position, bulletspawn.rotation);    //...instance a rocket at the bulletspawn position
            }
        }
    }

    //Called when a collision occurs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the object being collided with's tag is "Asteroid"...
        if (collision.collider.tag == "Asteroid")
        {
            StartCoroutine(Damage());   //...initiate the Damage() function
        }
        else if (collision.collider.tag == "MagnetMine")
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
        }
    }
}
