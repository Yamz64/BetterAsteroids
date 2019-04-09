using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHealthupBehavior : HealthBehavior
{
    public override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        sr.sprite = player.GetComponent<SpriteRenderer>().sprite;
        transform.localScale = player.transform.localScale*2;
    }
}
