using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWrap : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -6.35)
        {
            transform.position = new Vector3(transform.position.x, 6.2f, transform.position.z);
        }
        if (transform.position.y >= 6.35)
        {
            transform.position = new Vector3(transform.position.x, -6.2f, transform.position.z);
        }
        if (transform.position.x >= 10.9)
        {
            transform.position = new Vector3(-10.8f, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= -10.9)
        {
            transform.position = new Vector3(+10.8f, transform.position.y, transform.position.z);
        }
    }
}
