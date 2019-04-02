using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneMineBehavior : MonoBehaviour
{
    public float gravity;
    public Object explosion;
    private Rigidbody2D rb;
    private GameObject player;

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.up);
        float distancex = player.transform.position.x - transform.position.x;   //temporary float distancex is set equal to difference in x positions of the crosshair and rocket
        float distancey = player.transform.position.y - transform.position.y;   //temporary float distancey is set equal to difference in y positions of the crosshair and rocket
        float angle = -Mathf.Rad2Deg * Mathf.Atan(distancex / distancey);   //the angle between the barrel and the crosshair is calculated in degrees

        //if the crosshair is above the rocket...
        if (player.transform.position.y >= transform.position.y)
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);    //...set the barrel's eulerAngles equal to (0.0f, 0.0f angle) to point at the crosshair
        }
        else   //if the above statment is not true (the crosshair is below the barrel)...
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + 180);    //...set the rocket's eulerAngles equal to (0.0f, 0.0f, angle + 180) to point at the crosshair
        }

        rb.AddForce(transform.up * (gravity / (Mathf.Sqrt(distancex * distancex + distancey * distancey))));
    }
}
 