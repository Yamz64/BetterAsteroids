using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionParticleBehavior : MonoBehaviour
{
    public float killtime;  //float value stores the amount of time before the particle effect destroys itself
    // Update is called once per frame
    void Update()
    {
        killtime -= 1.0f * Time.deltaTime;  //decrement killtime over time

        //if killtime is less than or equal to 0...
        if(killtime <= 0)
        {
            Destroy(gameObject);    //...destroy the particle effect 
        }
    }
}
