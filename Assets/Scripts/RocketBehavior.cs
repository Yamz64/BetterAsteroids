using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    public float force;             //float value force determines the amount of force that the rocket is propelled at
    public float lifetime;          //float value lifetime determines the amount of time that the rocket is active
    public Object explosion;        //explosion prefab
    private Rigidbody2D rb;         //Rigidbody2D rb will be set equal to the attached Rigidbody2D component
    private GameObject crosshair;   //GameObject crosshair will be set equal to the crosshair object

    //function called when rocket explodes
    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);     //Instance an explosion at the position of the rocket
        Destroy(gameObject);    //destroy the rocket
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //rb is set equal to the attached Rigidbody2D component
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");  //crosshair is set equal to the GameObject in the Scene with the "Crosshair" tag
    }

    // Update is called once per frame
    void Update()
    {
        float distancex = crosshair.transform.position.x - transform.position.x;    //temporary float distancex is set equal to difference in x positions of the crosshair and rocket
        float distancey = crosshair.transform.position.y - transform.position.y;    //temporary float distancey is set equal to difference in y positions of the crosshair and rocket
        float angle = -Mathf.Rad2Deg * Mathf.Atan(distancex / distancey);           //the angle between the rocket and the crosshair is calculated in degrees

        //if the crosshair is above the rocket...
        if (crosshair.transform.position.y >= transform.position.y)
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);     //...set the rocket's eulerAngles equal to (0.0f, 0.0f angle) to point at the crosshair
        }
        else   //if the above statment is not true (the crosshair is below the rocket)...
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + 180);   //...set the rocket's eulerAngles equal to (0.0f, 0.0f, angle + 180) to point at the crosshair
        }
        rb.AddForce(transform.up * force);  //force is added to rb in the up direction
        lifetime -= 1 * Time.deltaTime;     //lifetime is decremented over time

        //if lifetime is less than or equal to 0 seconds...
        if (lifetime <= 0.0f)
        {
            Explode();  //...the Explode() function is initialized
        }
    }

    //function is called when a collider enter's the attached trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the collider's tag is "Asteroid"
        if(other.tag == "Asteroid")
        {
            Explode();  //...the Explode() function is initialized
        }
    }
}
