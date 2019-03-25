using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHeadMovement : MonoBehaviour
{
    public float speed;
    public float velocityx;
    public float velocityy;
    public float cooldownspeed;
    public Sprite[] images;
    public Object crosshairo;
    private SpriteRenderer image;
    private Rigidbody2D rb;
    private GameObject crosshair;
    private Collider2D col;

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
                rb.AddForce(transform.up * speed);
            }
        }
        else
        {
            image.sprite = images[1];
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
