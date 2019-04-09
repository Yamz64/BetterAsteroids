using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehavior : MonoBehaviour
{
    private SpriteRenderer sr;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        sr.sprite = player.GetComponent<SpriteRenderer>().sprite;
        transform.localScale = player.transform.localScale;
    }
}
