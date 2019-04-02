using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSoundBehavior : MonoBehaviour
{
    private AudioSource sound;  //the attached AudioSourceComponent

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();    //sound is set equal to the attached AudioSource component
    }

    // Update is called once per frame
    void Update()
    {
        //if the sound is not playing...
        if(sound.isPlaying != true)
        {
            Destroy(gameObject);    //...destroy the gameObject
        }
    }
}
