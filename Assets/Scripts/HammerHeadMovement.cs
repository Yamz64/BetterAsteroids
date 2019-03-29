using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHeadMovement : MonoBehaviour
{
    public float speed;             //float value stores the speed that the Hammer Head launches at
    public float velocityx;         //float value that reads the x velocity of the Hammer Head
    public float velocityy;         //float value that reads the y velocity of the Hammer Head
    public float cooldownspeed;     //float value that reads the speed threshold where the Hammer Head can be launched at
    public bool damageseq;          //determines if the ship is in the damage sequence
    public int hp;                  //the amount of health that the hammerhead has
    public Sprite[] images;         //sprite array stores all the sprites that the Hammer Head will display
    public Object crosshairo;       //crosshairo or crosshair object is the crosshair prefab
    public Object destruction;      //particle effect on destruction
    private SpriteRenderer image;   //image is the SpriteRenderer component attached to the gameobject
    private Rigidbody2D rb;         //rb is the Rigidbody2D componenet attached to the GameObject
    private GameObject crosshair;   //crosshair is the crosshair GameObject in the scene
    private Collider2D col;         //col is the Collider2D object attached to the GameObject

    public IEnumerator Damage()
    {
        damageseq = true;
        hp -= 1;
        Instantiate(destruction, transform.position, transform.rotation);
        image.enabled = false;
        col.enabled = false;
        yield return new WaitForSeconds(1);
        GameObject asteroidspawner = GameObject.FindGameObjectWithTag("AsteroidSpawner");
        asteroidspawner.GetComponent<AsteroidSpawner>().enabled = false;
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        image.enabled = true;
        image.color = new Color(255.0f, 255.0f, 255.0f, .5f);
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        for(int i=0; i<asteroids.Length; ++i)
        {
            Destroy(asteroids[i]);
        }
        yield return new WaitForSeconds(3);
        rb.velocity = Vector2.zero;
        asteroidspawner.GetComponent<AsteroidSpawner>().enabled = true;
        col.enabled = true;
        damageseq = false;

    }

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
        
        if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2)) < cooldownspeed)
        {
            image.sprite = images[0];
            if (damageseq == false)
            {
                image.color = Color.white;
            }
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
            if ((Input.GetButtonDown("Fire1")) && (damageseq == false))
            {
                rb.AddForce(transform.up * speed * Mathf.Sqrt(distancex * distancex + distancey * distancey));
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
        if(rb.velocity.magnitude < .01f)
        {
            rb.velocity = Vector2.zero;
        }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Asteroid")
        {
            StartCoroutine(Damage());
        }
    }
}
