using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    public float force;
    public float lifetime;
    public Object explosion;
    private Rigidbody2D rb;
    private GameObject crosshair;

    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    // Update is called once per frame
    void Update()
    {
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
        rb.AddForce(transform.up * force);
        lifetime -= 1 * Time.deltaTime;
        if (lifetime <= 0.0f)
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Asteroid")
        {
            Explode();
        }
    }
}
