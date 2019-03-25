using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float nextasteroid;
    public float maxasteroids;
    public Vector2 asteroidbounds;
    public Object asteroid;
    public GameObject[] asteroids;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        nextasteroid = Random.Range(asteroidbounds[0], asteroidbounds[1]);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        if(asteroids.Length < maxasteroids)
        {
            nextasteroid -= 1.0f * Time.deltaTime;
            if(nextasteroid <= 0.0f)
            {
                float xpos = Random.Range(-1, 1) * 10.8f;
                float ypos = Random.Range(-1, 1) * 6.2f;
                float distance = Mathf.Sqrt(Mathf.Pow(xpos - player.transform.position.x, 2) + Mathf.Pow(ypos - player.transform.position.y, 2));
                if(distance > 3.0f)
                {
                    Instantiate(asteroid, new Vector3(xpos, ypos), transform.rotation);
                    nextasteroid = Random.Range(asteroidbounds[0], asteroidbounds[1]);
                }
            }
        }
    }
}
