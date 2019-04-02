using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    public float growthspeed;       //float value stores the rate at which the blast-radius grows
    public float maxgrowthrad;      //float value stores the largest radius of the blast
    private ScoreBehavior score;    //The score UI element's ScoreBehavior component

    // Start is called before the first frame update
    private void Start()
    {
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreBehavior>();    //score is set equal to the ScoreBehavior component attached to the score UI element
    }

    // Update is called once per frame
    void Update()
    {
        //if the x scale is less than or equal to the max growth radius and the y scale is less than or equal to the max growth radius...
        if((transform.localScale.x <= maxgrowthrad) && (transform.localScale.y <= maxgrowthrad))
        {
            transform.localScale += new Vector3(growthspeed * Time.deltaTime, growthspeed * Time.deltaTime, 1.0f);  //...increase the radius of the blast over time
        }
        else   //if the above statement is not true...
        {
            Destroy(gameObject);    //...destroy the explosion
        }
    }

    //function called when a collider enters the trigger collider attached
    private void OnTriggerEnter2D(Collider2D other)
    {

        //if the collider's tag is "Asteroid"...
        if (other.tag == "Asteroid")
        {
            AsteroidBehavior split = other.GetComponent<AsteroidBehavior>();    //temporary datatype named split is set equal to the attached AsteroidBehavior component

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
        else if (other.tag == "MagnetMine")
        {
            score.score += 50;
            MagneMineBehavior magnetmine = other.GetComponent<MagneMineBehavior>();
            magnetmine.Explode();
        }
    }
}
