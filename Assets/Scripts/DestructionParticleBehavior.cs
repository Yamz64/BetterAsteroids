using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionParticleBehavior : MonoBehaviour
{
    public float killtime;
    // Update is called once per frame
    void Update()
    {
        killtime -= 1.0f * Time.deltaTime;
        if(killtime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
