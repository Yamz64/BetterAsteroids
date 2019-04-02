using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldReliableProjectileBehavior : MonoBehaviour
{
    public float force;         //float value force determines the force at which the projectile is launched
    public float lifetime;      //float value lifetime determines the amount of time that the projectile is active
    private Rigidbody2D rb;     //Rigidbody2D rb will be set equal to the attached Rigidbody2D component
    private ScoreBehavior score;//The score UI element's ScoreBehavior component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //set rb equal to the attached Rigidbody2D component
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreBehavior>();    //score is set equal to the ScoreBehavior component attached to the score UI element
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
            //if the level of the asteroid is equal to 3...
            if(other.GetComponent<AsteroidBehavior>().level == 3)
            {
                score.score += 10;  //...increment the score member variable of score by 10
            }
            //if the level of the asteroid is equal to 2...
            else if (other.GetComponent<AsteroidBehavior>().level == 2)
            {
                score.score += 50;  //...increment the score member variable of score by 50
            }
            //if the level of the asteroid is equal to 1...
            else if (other.GetComponent<AsteroidBehavior>().level == 1)
            {
                score.score += 100; //...increment the score member variable of score by 100
            }
            Destroy(gameObject);                                //...destroy the projectile
        }else if(other.tag == "MagnetMine")
        {
            score.score += 50;
            MagneMineBehavior magnetmine = other.GetComponent<MagneMineBehavior>();
            magnetmine.Explode();
        }
    }
}
