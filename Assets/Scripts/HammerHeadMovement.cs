using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHeadMovement : MonoBehaviour
{
    public float speed;             //float value stores the speed that the Hammer Head launches at
    public float velocityx;         //float value that reads the x velocity of the Hammer Head
    public float velocityy;         //float value that reads the y velocity of the Hammer Head
    public float cooldownspeed;     //float value that reads the speed threshold where the Hammer Head can be launched at
    public Sprite[] images;         //sprite array stores all the sprites that the Hammer Head will display
    public Object crosshairo;       //crosshairo or crosshair object is the crosshair prefab
    private SpriteRenderer image;   //image is the SpriteRenderer component attached to the gameobject
    private Rigidbody2D rb;         //rb is the Rigidbody2D componenet attached to the GameObject
    private GameObject crosshair;   //crosshair is the crosshair GameObject in the scene
    private Collider2D col;         //col is the Collider2D object attached to the GameObject

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(crosshairo);
        image = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x,2)+Mathf.Pow(rb.velocity.y,2)) < cooldownspeed)
        {
            image.sprite = images[0];
            image.color = Color.white;
            col.isTrigger = false;
            float distancex = crosshair.transform.position.x - transform.position.x;
            float distancey = crosshair.transform.position.y - transform.position.y;
            float angle = -Mathf.Rad2Deg * Mathf.Atan(distancex / distancey);
            if (crosshair.transform.position.y >= transform.position.y)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
            }
            else
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + 180);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                rb.AddForce(transform.up * speed * Mathf.Sqrt(distancex*distancex+distancey*distancey));
            }
        }
        else
        {
            image.sprite = images[1];
            image.color = Color.yellow;
            col.isTrigger = true;
        }
        velocityx = rb.velocity.x;
        velocityy = rb.velocity.y;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Asteroid")
        {
            AsteroidBehavior split = other.GetComponent<AsteroidBehavior>();
            if (split.itime <= 0.0f)
            {
                split.Explode();
            }
        }
    }
}
