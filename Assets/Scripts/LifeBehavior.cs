using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBehavior : MonoBehaviour
{
    public GameObject[] children;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        children = new GameObject[gameObject.transform.childCount];
        for(int i=0; i < children.Length; ++i)
        {
            children[i] = transform.GetChild(i).gameObject;
            children[i].GetComponent<Image>().sprite = player.GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.name == "HammerHead")
        {
            HammerHeadMovement hbehavior = player.GetComponent<HammerHeadMovement>();
            if(hbehavior.hp == 0)
            {
                children[0].GetComponent<Image>().enabled = false;
                children[1].GetComponent<Image>().enabled = false;
                children[2].GetComponent<Image>().enabled = false;
            }else if(hbehavior.hp == 1)
            {
                children[0].GetComponent<Image>().enabled = false;
                children[1].GetComponent<Image>().enabled = false;
                children[2].GetComponent<Image>().enabled = true;
            }else if (hbehavior.hp == 2)
            {
                children[0].GetComponent<Image>().enabled = false;
                children[1].GetComponent<Image>().enabled = true;
                children[2].GetComponent<Image>().enabled = true;
            }else if (hbehavior.hp == 3)
            {
                children[0].GetComponent<Image>().enabled = true;
                children[1].GetComponent<Image>().enabled = true;
                children[2].GetComponent<Image>().enabled = true;
            }
        }
    }
}
