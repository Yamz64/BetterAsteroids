using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed;
    public Object crosshairo;
    public Object rocket;
    public Transform barrel;
    private Transform bulletspawn;
    private Rigidbody2D rb;
    private GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(crosshairo);
        barrel = transform.GetChild(0);
        bulletspawn = barrel.transform.GetChild(0);
        rb = GetComponent<Rigidbody2D>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * speed);
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, -45.0f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
            }
            else
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.up * speed);
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, -135.0f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 135.0f);
            }
            else
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.up * speed);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.AddForce(transform.up * speed);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
        }
        float distancex = crosshair.transform.position.x - barrel.position.x;
        float distancey = crosshair.transform.position.y - barrel.position.y;
        float angle = -Mathf.Rad2Deg * Mathf.Atan(distancex / distancey);
        if(crosshair.transform.position.y >= barrel.transform.position.y)
        {
            barrel.eulerAngles = new Vector3(0.0f, 0.0f, angle);
        }
        else
        {
            barrel.eulerAngles = new Vector3(0.0f, 0.0f, angle+180);
        }
        if(Input.GetButton("Fire1"))
        {
            if (GameObject.FindGameObjectWithTag("Rocket") == null)
            {
                Instantiate(rocket, bulletspawn.position, bulletspawn.rotation);
            }
        }
    }
}
