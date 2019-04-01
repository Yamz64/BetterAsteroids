using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHeadMovement : MonoBehaviour
{
    public float speed;             //float value stores the speed that the Hammer Head launches at
    public float velocityx;         //float value that reads the x velocity of the Hammer Head (debug variable)
    public float velocityy;         //float value that reads the y velocity of the Hammer Head (debug variable)
    public float cooldownspeed;     //float value that reads the speed threshold where the Hammer Head can be launched at
    public bool damageseq;          //determines if the ship is in the damage sequence
    public int hp;                  //the amount of health that the hammerhead has
    public Sprite[] images;         //sprite array stores all the sprites that the Hammer Head will display
    public Object crosshairo;       //crosshairo or crosshair object is the crosshair prefab
    public Object destruction;      //particle effect on destruction
    private SpriteRenderer image;   //image is the SpriteRenderer component attached to the gameobject
    private Rigidbody2D rb;         //rb is the Rigidbody2D componenet attached to the GameObject
    private GameObject crosshair;   //crosshair is the crosshair GameObject in the scene
    private Collider2D col;         //col is the Collider2D object attached to the GameObject
    private ScoreBehavior score;    //The score UI element's ScoreBehavior component

    // Damage is called when ship takes damage
    public IEnumerator Damage()
    {
        damageseq = true;                                                                   //damageseq is set to true to show ship is in damage sequence
        hp -= 1;                                                                            //health is decremented by 1
        Instantiate(destruction, transform.position, transform.rotation);                   //the destruction particle is instanced at the current position
        image.enabled = false;                                                              //the SpriteRenderer component is disabled
        col.enabled = false;                                                                //the Collider2D component is disabled

        //if hp is not equal to -1...
        if (hp != -1)
        {
            yield return new WaitForSeconds(1);                                                 //Wait 1 second
            GameObject asteroidspawner = GameObject.FindGameObjectWithTag("AsteroidSpawner");   //GameObject asteroidspawner is set equal to the ameObject witht eh "AsteroidSpawner" tag
            asteroidspawner.GetComponent<AsteroidSpawner>().enabled = false;                    //asteroidspawner's AsteroidSpawner script is disabled
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);                                 //the ship's position is reset to origin
            rb.angularVelocity = 0;                                                             //reset the angular velocity
            image.enabled = true;                                                               //the SpriteRenderer Component is enabled
            image.color = new Color(255.0f, 255.0f, 255.0f, .5f);                               //the SpriteRenderer's color is set to semitransparent
            GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");             //GameObject[] asteroids is set equal to all GameObjects with the "Asteroid" tag

            //iterates integer i until it is less than the length of asteroids
            for (int i = 0; i < asteroids.Length; ++i)
            {
                Destroy(asteroids[i]);  //destroy asteroid at index i
            }

            yield return new WaitForSeconds(3);                                 //wait 3 seconds
            rb.velocity = Vector2.zero;                                         //set the RigidBody2D's component's velocityx and velocityy to 0
            rb.angularVelocity = 0;                                             //reset the angular velocity
            asteroidspawner.GetComponent<AsteroidSpawner>().enabled = true;     //asteroidspawner's AsteroidSpawner script is enabled
            col.enabled = true;                                                 //the Collider2DComponent is enabled
            damageseq = false;                                                  //damageseq is set to false to show ship has ended the damage sequence
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(crosshairo);                                        //Instance a crosshair at origin
        image = GetComponent<SpriteRenderer>();                         //image is set equal to the attached SpriteRenderer component
        rb = GetComponent<Rigidbody2D>();                               //rb is set equal to the attached RigidBody2D component
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");      //crosshair is set equal to the GameObject with the tag "Crosshair"
        col = GetComponent<Collider2D>();                               //col is set equal to the attached Collider2D component
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreBehavior>();    //score is set equal to the ScoreBehavior component attached to the score UI element
    }

    // Update is called once per frame
    void Update()
    {
        //if the magnitude of the rb's velocity is less than the cooldownspeed...
        if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2)) < cooldownspeed)
        {
            image.sprite = images[0];                                                   //...the sprite is set equal to the nonthrusting image

            //if the damage sequence is not occurring...
            if (damageseq == false)
            {
                image.color = Color.white;  //...the color is set to white
            }
            col.isTrigger = false;                                                      //...the collider is set to not being a trigger
            float distancex = crosshair.transform.position.x - transform.position.x;    //...distance x is calculated between the hammerhead and the crosshair
            float distancey = crosshair.transform.position.y - transform.position.y;    //...distance y is calculated between the hammerhead and the crosshair
            float angle = -Mathf.Rad2Deg * Mathf.Atan(distancex / distancey);           //...the angle is calculated between the the crosshair and the hammerhead in degrees

            //if the crosshair is above the hammerhead...
            if (crosshair.transform.position.y >= transform.position.y)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);         //...set the hammerhead's eulerangles equal to (0,0,angle) to point at the crosshair
            }
            else   //if the crosshair is below the hammerhead...
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + 180);   //...set the hammerhead's eulerangles equal to (0,0,angle+180) to point at the crosshair
            }

            //if the "Fire1" button is pressed and the damageseq is set to false
            if ((Input.GetButtonDown("Fire1")) && (damageseq == false))
            {
                rb.AddForce(transform.up * speed * Mathf.Sqrt(distancex * distancex + distancey * distancey));  //...AddForce to the hammerhead so that it reaches the crosshair approximately
            }
        }
        else   //if magnitude of rb's velocity is greater than cooldownspeed...
        {
            image.sprite = images[1];       //...set the image to the thrust animation
            image.color = Color.yellow;     //...set the color to yellow
            col.isTrigger = true;           //...set the collider to a trigger
        }
        velocityx = rb.velocity.x;          //velocityx is set to the x velocity of rb
        velocityy = rb.velocity.y;          //velocityy is set to the y velocity of rb

        //if rb's velocity magnitude is less than .01f...
        if (rb.velocity.magnitude < .05f)    
        {
            rb.velocity = Vector2.zero;         //...set rb's velocity to zero
        }

        //if rb's angular velocity is less tha .01f
        if(rb.angularVelocity < .01f)
        {
            rb.angularVelocity = 0;             //...set rb's angular velocity to 0
        }
    }

    //Called when a collider enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the collider's tag is "Asteroid"...
        if (other.tag == "Asteroid")
        {
            AsteroidBehavior split = other.GetComponent<AsteroidBehavior>();    //...create a temporary dataType AsteroidBehavior named split and set it equal to the collider's attached AsteroidBehavior component

            //if split's itime is less than or equal to 0 seconds...
            if (split.itime <= 0.0f)
            {
                //if the level of the asteroid is equal to 3...
                if (split.level == 3)
                {
                    score.score += 10;  //...increment the score member variable of score by 10
                }
                //if the level of the asteroid is equal to 2...
                else if (split.level == 2)
                {
                    score.score += 50;  //...increment the score member variable of score by 50
                }
                //if the level of the asteroid is equal to 1...
                else if (split.level == 1)
                {
                    score.score += 100; //...increment the score member variable of score by 100
                }
                split.Explode();    //...start split's Explode() function
            }
        }
    }

    //Called when a collision occurs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the object being collided with's tag is "Asteroid"...
        if(collision.collider.tag == "Asteroid")
        {
            StartCoroutine(Damage());   //...initiate the Damage() function
        }
    }
}
