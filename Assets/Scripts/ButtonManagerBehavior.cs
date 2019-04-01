using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManagerBehavior : MonoBehaviour
{
    public Text shiptitle;
    public Text shipdescription;

    public void MouseOverOF()
    {
        shiptitle.text = "Old Faithful";
        shipdescription.text = "Designed after the classic Asteroids star fighter...\nUse the the left/right arrow keys or the A/D keys to turn.\nUse the W or up arrow key to thrust forward.\nFire with the space key";
    }

    public void MouseOverT()
    {
        shiptitle.text = "Tank";
        shipdescription.text = "A slow, large, and powerful tank...\nUse the WASD/Arrow Keys to move in the 8 cardinal directions.\nLeft click to fire rockets that explode on contact with asteroids or after an amount of time.\nRockets are affected by asteroid iframes.\nRockets will follow the crosshair.";
    }

    public void MouseOverHH()
    {
        shiptitle.text = "Hammer Head";
        shipdescription.text = "Ram through asteroids with the click of a button...\nMove to the crosshair by left clicking.\nYou can ram into asteroids while thrusting, but will explode while sitting still.\nThe Hammer Head is affected by Asteroid iframes.";
    }

    public void ClickedOF()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickedT()
    {
        SceneManager.LoadScene(2);
    }

    public void ClickedHH()
    {
        SceneManager.LoadScene(3);
    }
}
