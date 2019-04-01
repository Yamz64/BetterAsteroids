using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeBehavior : MonoBehaviour
{
    public float textDelay;         //float value stores the delay between letters
    public bool sequencestarted;    //has death sequence started?
    public Text gameovertext;       //the text that plays when you game over
    public GameObject[] children;   //array of GameObjects children references all of the life images
    private GameObject player;      //GameObject player references the player ship

    private IEnumerator DeathSequence()
    {
        sequencestarted = true;
        string gameoverstring = "Game Over";
        for(int i=0; i<gameoverstring.Length; ++i)
        {
            gameovertext.text += gameoverstring[i];
            yield return new WaitForSeconds(textDelay);
        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");            //player is set equal to to the GameObject with tag "Player"
        children = new GameObject[gameObject.transform.childCount];     //children's size is set equal to the number of children the LifeHolder has
        gameovertext.text = "";

        //i is iterated for as many children as the LifeHolder has
        for(int i=0; i < children.Length; ++i)
        {
            children[i] = transform.GetChild(i).gameObject;     //set the value at index i equal to child of index i's gameObject component
            children[i].GetComponent<Image>().sprite = player.GetComponent<SpriteRenderer>().sprite;    //set child of index i's sprite to the player's sprite
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if the player's name is "HammerHead"...
        if(player.name == "HammerHead")
        {
            //...create a temporary variable of dataType HammerHeadMovement named hbehavior and set it equal to the HammerHeadMovement component of the player
            HammerHeadMovement hbehavior = player.GetComponent<HammerHeadMovement>();

            //if the member variable of hbehavior hp is equal to 0...
            if(hbehavior.hp == 0)
            {
                children[0].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 2
            }
            //if the member variable of hbehavior hp is equal to 1...
            else if(hbehavior.hp == 1)
            {
                children[0].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 2
            }
            //if the member variable of hbehavior hp is equal to 2...
            else if (hbehavior.hp == 2)
            {
                children[0].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 2
            }
            //if the member variable of hbehavior hp is equal to 3...
            else if (hbehavior.hp == 3)
            {
                children[0].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 2
            }
            //if the member variable of hbehavior hp is equal to -1 and sequence started equals false...
            else if ((hbehavior.hp == -1) && (sequencestarted == false))
            {
                StartCoroutine(DeathSequence());
            }
        //if the player's name is "OldFaithful"...
        }else if(player.name == "OldFaithful")
        {
            //...create a temporary variable of dataType OldFaithfulMovement named obehavior and set it equal to the OldFaithfulMovement component of the player
            OldFaithfulMovement obehavior = player.GetComponent<OldFaithfulMovement>();

            //if the member variable of obehavior hp is equal to 0...
            if (obehavior.hp == 0)
            {
                children[0].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 2
            }
            //if the member variable of obehavior hp is equal to 1...
            else if (obehavior.hp == 1)
            {
                children[0].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 2
            }
            //if the member variable of obehavior hp is equal to 2...
            else if (obehavior.hp == 2)
            {
                children[0].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 2
            }
            //if the member variable of obehavior hp is equal to 3...
            else if (obehavior.hp == 3)
            {
                children[0].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 2
            }
            //if the member variable of obehavior hp is equal to -1 and sequence started equals false...
            else if ((obehavior.hp == -1) && (sequencestarted == false))
            {
                StartCoroutine(DeathSequence());
            }
        }
        //if the player's name is "Tank"...
        else if (player.name == "Tank")
        {
            //...create a temporary variable of dataType TankMovement named tbehavior and set it equal to the TankMovement component of the player
            TankMovement tbehavior = player.GetComponent<TankMovement>();

            //if the member variable of tbehavior hp is equal to 0...
            if (tbehavior.hp == 0)
            {
                children[0].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 2
            }
            //if the member variable of tbehavior hp is equal to 1...
            else if (tbehavior.hp == 1)
            {
                children[0].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 2
            }
            //if the member variable of tbehavior hp is equal to 2...
            else if (tbehavior.hp == 2)
            {
                children[0].GetComponent<Image>().enabled = false;  //...disable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 2
            }
            //if the member variable of tbehavior hp is equal to 3...
            else if (tbehavior.hp == 3)
            {
                children[0].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 0
                children[1].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 1
                children[2].GetComponent<Image>().enabled = true;   //...enable the Image component of child of index 2
            }
            //if the member variable of tbehavior hp is equal to -1 and sequence started equals false...
            else if ((tbehavior.hp == -1) && (sequencestarted == false))
            {
                StartCoroutine(DeathSequence());
            }
        }
    }
}
