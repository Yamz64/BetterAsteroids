using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldReliableProjectileBehavior : MonoBehaviour
{
    public float force;
    public float lifetime;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * force);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= 1 * Time.deltaTime;
        if(lifetime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Asteroid")
        {
            other.GetComponent<AsteroidBehavior>().Explode();
            Destroy(gameObject);
        }
    }
}
