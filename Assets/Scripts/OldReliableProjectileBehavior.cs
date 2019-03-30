using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldReliableProjectileBehavior : MonoBehaviour
{
    public float force;         //float value force determines the force at which the projectile is launched
    public float lifetime;      //float value lifetime determines the amount of time that the projectile is active
    private Rigidbody2D rb;     //Rigidbody2D rb will be set equal to the attached Rigidbody2D component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //set rb equal to the attached Rigidbody2D component
        rb.AddForce(transform.up * force);  //AddForce to rb in the updirection multiplied by force
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= 1 * Time.deltaTime;     //lifetime is decremented over time

        //if lifetime is less than or equal to 0 seconds...
        if(lifetime <= 0.0f)
        {
            Destroy(gameObject);    //...destroy the projectile
        }
    }

    //function called when a collider enters the attached trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the other collider's tag is "Asteroid"...
        if(other.tag == "Asteroid")
        {
            other.GetComponent<AsteroidBehavior>().Explode();   //...initialized the collider's Explode() method from their attached AsteroidBehavoir component
            Destroy(gameObject);                                //...destroy the projectile
        }
    }
}
