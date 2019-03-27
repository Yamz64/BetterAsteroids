using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    public float forceboundsx;
    public float forceboundsy;
    public float torquebounds;
    public float itime;
    public int level;
    public Sprite[] images;
    public Object asteroid;
    public Object[] destructions;
    private Rigidbody2D rb;
    private SpriteRenderer image;

    public void Explode()
    {
        Instantiate(asteroid, new Vector3(transform.position.x + Random.Range(-.001f,.001f), transform.position.y + Random.Range(-.001f, .001f), transform.position.z + Random.Range(-.001f, .001f)), transform.rotation);
        Instantiate(asteroid, new Vector3(transform.position.x + Random.Range(-.001f, .001f), transform.position.y + Random.Range(-.001f, .001f), transform.position.z + Random.Range(-.001f, .001f)), transform.rotation);
        if(image.sprite == images[0])
        {
            Instantiate(destructions[0], transform.position, transform.rotation);
        }else if(image.sprite == images[1])
        {
            Instantiate(destructions[1], transform.position, transform.rotation);
        }else if(image.sprite == images[2])
        {
            Instantiate(destructions[2], transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        itime = 2.0f;
        image = GetComponent<SpriteRenderer>();
        image.sprite = images[Random.Range(0, 3)];
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-forceboundsx, forceboundsx), Random.Range(-forceboundsy, forceboundsy)));
        float clonen = 0;
        for (int i = 0; i < gameObject.name.Length; ++i)
        {
            if ((gameObject.name[i] == '(') && (gameObject.name.Substring(i, 7) == "(Clone)"))
            {
                clonen += 1;
            }
        }
        if (clonen != 4)
        {
            level = (int)Mathf.Ceil(3 / clonen);
        }
        else
        {
            level = 0;
        }
        if (level == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            rb.AddTorque(Random.Range(-torquebounds, torquebounds)*(level/3.0f));
            transform.localScale = new Vector3(level / 3.0f, level / 3.0f, level / 3.0f);
        }
    }
    private void Update()
    {
        itime -= 1 * Time.deltaTime;
        if(itime > 0.0f)
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.white;
        }
    }
}