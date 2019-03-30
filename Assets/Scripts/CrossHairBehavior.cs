using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairBehavior : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //set the position of the crosshair equal to the mouse's position in the main Camera with respect to the Camera's width and height
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3((Input.mousePosition.x)/Camera.main.pixelWidth, (Input.mousePosition.y) / Camera.main.pixelHeight, 10.0f));
    }
}
