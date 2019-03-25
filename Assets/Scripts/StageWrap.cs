using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWrap : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -6.35)  //if the y position is less than -6.35...
        {
            transform.position = new Vector3(transform.position.x, 6.2f, transform.position.z);     //wrap to the top of the screen
        }
        if (transform.position.y >= 6.35)   //if the y position is greater than 6.35
        {
            transform.position = new Vector3(transform.position.x, -6.2f, transform.position.z);    //wrap to the bottom of the screen
        }
        if (transform.position.x >= 10.9)   //if the x position is greater than 10.9
        {
            transform.position = new Vector3(-10.8f, transform.position.y, transform.position.z);   //wrap to the left of the screen
        }
        if (transform.position.x <= -10.9)  //if the x position is less than -10.9
        {
            transform.position = new Vector3(+10.8f, transform.position.y, transform.position.z);   //wrap to the right of the screen
        }
    }
}
