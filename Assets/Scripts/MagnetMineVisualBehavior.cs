using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMineVisualBehavior : MonoBehaviour
{
    public float speed;
    public bool direction;

    // Start is called before the first frame update
    private void Start()
    {
        float rdirection = Random.Range(0f, 1f);
        if(rdirection < .5f)
        {
            direction = false;
        }
        else
        {
            direction = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == true)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, speed * Time.deltaTime));
        }
        else
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, -speed * Time.deltaTime));
        }
    }
}
