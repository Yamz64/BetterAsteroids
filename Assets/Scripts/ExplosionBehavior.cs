﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    public float growthspeed;
    public float maxgrowthrad;

    // Update is called once per frame
    void Update()
    {
        if((transform.localScale.x <= maxgrowthrad) && (transform.localScale.y <= maxgrowthrad))
        {
            transform.localScale += new Vector3(growthspeed * Time.deltaTime, growthspeed * Time.deltaTime, 1.0f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Asteroid")
        {
            AsteroidBehavior split = other.GetComponent<AsteroidBehavior>();
            if(split.itime <= 0.0f)
            {
                split.Explode();
            }
        }
    }
}
