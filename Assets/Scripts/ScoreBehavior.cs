using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBehavior : MonoBehaviour
{
    public int score;       //int value stores the current score
    private Text text;      //text component attached to the score text
    private Text htext;     //text component attached to the hiscore text

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();    //text is set equal to the text component attached
        htext = GameObject.FindGameObjectWithTag("HiScore").GetComponent<Text>();   //htext is set equal to the text component attached to the gameObject with the "HiScore" tag
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "-Score-\n" + score.ToString();     //the text member variable of text is set equal to -Score-[ENTER](the score integer)
        htext.text = "-HiScore-\n" + PlayerPrefs.GetInt("HiScore").ToString();  //the text member variable of htext is set equal to -HiScore-[ENTER](the HiScore player preference integer)
        
        //if score is greater than the HiScore...
        if (score > PlayerPrefs.GetInt("HiScore"))
        {
            PlayerPrefs.SetInt("HiScore", score);   //...set the HiScore equal to the score
        }
    }
}
