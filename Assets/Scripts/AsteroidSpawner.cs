using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float difficultytimer;   //float value store the amount of time that has passed to determine difficulty
    public float nextasteroid;      //float value stores time until the next asteroid
    public float maxasteroids;      //float value determines the max amount of asteroids
    public Vector2 asteroidbounds;  //the least and most amount of time taken inbetween asteroid spawns
    public Object asteroid;         //asteroid prefab
    public GameObject[] asteroids;  //Gameobject array stores all asteroids
    public GameObject player;       //Player gameobject

    // Start is called before the first frame update
    void Start()
    {
        nextasteroid = Random.Range(asteroidbounds[0], asteroidbounds[1]);  //nextasteroid is set to a random amount between the asteroidbounds
        player = GameObject.FindGameObjectWithTag("Player");                //player is found by looking for the object with the "Player" tag
    }

    // Update is called once per frame
    void Update()
    {
        difficultytimer += 1.0f * Time.deltaTime;           //Increase the difficultytimer over time
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");  //all objects with "Asteroid" tag are added to asteroids array
        if(asteroids.Length < maxasteroids)                 //if the length of the asteroids array is less than the max asteroids float...
        {
            nextasteroid -= 1.0f * Time.deltaTime;              //nextasteroid decrements over time
            if(nextasteroid <= 0.0f)                            //if next asteroid is less than or equal to 0...
            {
                float xpos = Random.Range(-1, 1) * 10.8f;           //a prospective xpos is selected randomly between -10.8 and 10.8
                float ypos = Random.Range(-1, 1) * 6.2f;            //a prospective ypos is selected randomly between -6.2 and 6.2
                float distance = Mathf.Sqrt(Mathf.Pow(xpos - player.transform.position.x, 2) + Mathf.Pow(ypos - player.transform.position.y, 2));   //the distance between the player and the prospective point is calculated
                if(distance > 3.0f)     //if the distance between the player and the prospective point is greater than 3...
                {
                    Instantiate(asteroid, new Vector3(xpos, ypos), transform.rotation);     //create an asteroid at that position
                    nextasteroid = Random.Range(asteroidbounds[0], asteroidbounds[1]);      //nextasteroid is set to a random amount between the asteroidbounds
                }
            }
        }
        if(difficultytimer > 30.0f)
        {
            maxasteroids = 5 + (int)(difficultytimer / 15.0f);
        }
        else
        {
            maxasteroids = 5;
        }
    }
}
