﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehavior : MonoBehaviour
{
    public SpriteRenderer sr;
    public GameObject player;

    // Start is called before the first frame update
    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        sr.sprite = player.GetComponent<SpriteRenderer>().sprite;
        transform.localScale = player.transform.localScale;
    }
}
