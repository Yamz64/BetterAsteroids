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

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(crosshairo);
        image = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    // Update is called once per frame
    void Update()
    {
        if ((rb.velocity.x <= cooldownspeed) && (rb.velocity.y <= cooldownspeed))
        {
            image.sprite = images[0];
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
        }
        velocityx = rb.velocity.x;
        velocityy = rb.velocity.y;
    }
}
