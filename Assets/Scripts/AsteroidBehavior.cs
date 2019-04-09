using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    public float forceboundsx;      //float value forceboundsx stores the lowest and highest possible force values to be applied to asteroid on the x axis
    public float forceboundsy;      //float value forceboundsy stores the lowest and highest possible force values to be applied to asteroid on the y axis
    public float torquebounds;      //float value torquebouunds stores the lowest and highest possible torque values to be applied to asteroid
    public float itime;             //float value itime, determines the amount of time asteroid is to be in iframes
    public int level;               //int value level determines the level of the asteroid with 3 being large, 2 being medium, and 1 being small
    public Sprite[] images;         //array of datatype Sprite stores all of the possible asteroid sprite images
    public Object asteroid;         //asteroid prefab
    public Object noise;            //empty object that plays a sound if the asteroid level is 0
    public Object hppickup;         //the object that spawns for a health pickup
    public Object[] destructions;   //array of prefabs that spawn a different type of particle effect depending on the sprite currently used
    private Rigidbody2D rb;         //datatype Rigidbody2D rb will be set equal to the attached Rigidbody2D component
    private SpriteRenderer image;   //datatype SpriteRenderer will be set equal to the attached image component
    private AudioSource explode;    //audiosource explode is the attached component that plays when an asteroid explodes

    //Function called when Asteroid Explodes
    public void Explode()
    {
        //Instance 2 asteroids at roughly the same position as the Parent asteroid (randomness added so that asteroids do not spawn inside of eachother)
        Instantiate(asteroid, new Vector3(transform.position.x + Random.Range(-.001f,.001f), transform.position.y + Random.Range(-.001f, .001f), transform.position.z + Random.Range(-.001f, .001f)), transform.rotation);
        Instantiate(asteroid, new Vector3(transform.position.x + Random.Range(-.001f, .001f), transform.position.y + Random.Range(-.001f, .001f), transform.position.z + Random.Range(-.001f, .001f)), transform.rotation);

        float healthchance = Random.Range(0, 100);
        if(healthchance == 99)
        {
            Instantiate(hppickup, transform.position, transform.rotation);
        }

        //if the spriterenderer's sprite is equal to...
        //...the first image in images
        if(image.sprite == images[0])
        {
            GameObject particle = Instantiate(destructions[0], transform.position, transform.rotation) as GameObject;   //...instance the first particle effect in destructions
            //set the particle effect's scale equal to the scale of the asteroid based on the level of the asteroid
            if(level == 3)
            {
                particle.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }else if(level == 2)
            {
                particle.transform.localScale = new Vector3(2.0f/3.0f, 2.0f/3.0f, 2.0f/3.0f);
            }
            else if (level == 1)
            {
                particle.transform.localScale = new Vector3(1.0f / 3.0f, 1.0f / 3.0f, 1.0f / 3.0f);
            }

            //...the second image in images
        }
        else if(image.sprite == images[1])
        {
            GameObject particle = Instantiate(destructions[0], transform.position, transform.rotation) as GameObject;   //...instance the first particle effect in destructions
            //set the particle effect's scale equal to the scale of the asteroid based on the level of the asteroid
            if (level == 3)
            {
                particle.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else if (level == 2)
            {
                particle.transform.localScale = new Vector3(2.0f / 3.0f, 2.0f / 3.0f, 2.0f / 3.0f);
            }
            else if (level == 1)
            {
                particle.transform.localScale = new Vector3(1.0f / 3.0f, 1.0f / 3.0f, 1.0f / 3.0f);
            }

            //...the third image in images
        }
        else if(image.sprite == images[2])
        {
            GameObject particle = Instantiate(destructions[0], transform.position, transform.rotation) as GameObject;   //...instance the first particle effect in destructions
            //set the particle effect's scale equal to the scale of the asteroid based on the level of the asteroid
            if (level == 3)
            {
                particle.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else if (level == 2)
            {
                particle.transform.localScale = new Vector3(2.0f / 3.0f, 2.0f / 3.0f, 2.0f / 3.0f);
            }
            else if (level == 1)
            {
                particle.transform.localScale = new Vector3(1.0f / 3.0f, 1.0f / 3.0f, 1.0f / 3.0f);
            }
        }
        Destroy(gameObject);    //destroy the attached gameObject component
    }
    // Start is called before the first frame update
    void Start()
    {
        itime = 2.0f;   //set itime to 2 seconds
        image = GetComponent<SpriteRenderer>();     //image is set to the attached SpriteRenderer component
        image.sprite = images[Random.Range(0, 3)];      //the sprite member variable of image is set to a random image from images
        rb = GetComponent<Rigidbody2D>();   //rb is set to the attached Rigidbody2D component
        rb.AddForce(new Vector2(Random.Range(-forceboundsx, forceboundsx), Random.Range(-forceboundsy, forceboundsy)));     //rb is given a random force between the negative and positive versions of forceboundsx and forceboundsy
        explode = GetComponent<AudioSource>();  //explode is set equal to the attached AudioSource component
        float clonen = 0;   //a temporary float named clonen is set to 0

        //a temporary int i is iterated so long as it's less than the number of characters in the name of the object
        for (int i = 0; i < gameObject.name.Length; ++i)
        {
            //if the character at index i in the name of the gameObject is equal to '(' and the Substring at index i to 7 characters ahead is equal to "(Clone)"...
            if ((gameObject.name[i] == '(') && (gameObject.name.Substring(i, 7) == "(Clone)"))
            {
                clonen += 1;    //...increment clonen
            }
        }

        //if clonen is not equal to 4...
        if (clonen != 4)
        {
            level = (int)Mathf.Ceil(3 / clonen);    //...set the level of the asteroid equal to the highest closest integer to 3 divided by clonen
        }
        else   //if the above statement is not true (level is equal to 4)...
        {
            level = 0;  //...set the asteroid's level to 0
        }

        //if the asteroid's level is equal to 0...
        if (level == 0)
        {
            Destroy(gameObject);    //...destroy the asteroid
        }
        else   //if the above statement is not true...
        {
            rb.AddTorque(Random.Range(-torquebounds, torquebounds)*(level/3.0f));   //AddTorque to the Asteroid between the negative and positive torquebounds multiplied by the (level of the asteroid/3)
            transform.localScale = new Vector3(level / 3.0f, level / 3.0f, level / 3.0f);   //set the scale on all axes equal to the level of the asteroid divided by 3
        }
        //if the asteroid's level is not equal to 3...
        if((level != 3) && (level != 0))
        {
            explode.Play();     //...play the explosion sound
        
        //if the asteroid's level is equal to 0...
        }else if(level == 0)
        {
            Instantiate(noise, transform.position, transform.rotation);     //Instance the noise object
        }
    }

    //Update is called once per frame
    private void Update()
    {
        itime -= 1 * Time.deltaTime;    //decrement itime over time

        //if itime is greater than 0...
        if(itime > 0.0f)
        {
            image.color = Color.yellow;     //...set the asteroid's color to yellow
        }
        else   //if the above statement is not true...
        {
            image.color = Color.white;      //...set the asteroid's color to white
        }
    }
}