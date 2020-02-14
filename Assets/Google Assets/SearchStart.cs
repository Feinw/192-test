/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Filbert Wee

Code History:
1. Filbert Wee
   Change Date: February 7, 2020
   Change Description: Added movement of main objects

File Creation
Date: Feb 7, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchStart : MonoBehaviour
{
    // the image of the Google logo
    public GameObject logo;
    // the area where the player has to guess the words
    public GameObject gameArea;
    // the area containing the amount of tries left
    public GameObject triesArea;
    // the image of a keyboard
    public GameObject keyboard;
    // the "search bar"
    private GameObject bar;

    public static bool disabled = true;

    // if has moved already
    private bool moved;

    /*
    method name: Start
    routine's creation date: February 7, 2020
    purpose of the routine: Start is called before the first frame update. Usually used for initializing variables.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start()
    {
        // sets the search bar to what object this is connected to
        bar = gameObject;
        // initialize "has moved" to not yet
        moved = false;

        // changes the text if the player hasnt reached this point in the story yet
        if (disabled)
        {
            Text text = bar.transform.GetChild(0).GetComponent<Text>();
            text.text = "Progress further to unlock";
        }
    }

    /*
    method name: moveStart
    routine's creation date: February 7, 2020
    purpose of the routine: This routine is called to moved the gameObjects from outside the screen to the visible area with animation.
                            This also moves and scales the other gameObjects already in the screen.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void moveStart()
    {
        // checks is has moved already
        if (!moved && !disabled)
        {
            // movement and scaling of Google logo
            iTween.ScaleBy(logo, iTween.Hash("x", 0.5, "y", 0.5, "default", 1f));
            iTween.MoveBy(logo, iTween.Hash("y", 600, "default", 1f));


            // movement and scaling of "search bar"
            iTween.ScaleBy(bar, iTween.Hash("x", 1.3, "y", 2, "default", 1f));
            iTween.MoveBy(bar, iTween.Hash("y", 600, "default", 1f));

            // disables the search bar - turning it into a title area
            bar.GetComponent<Button>().interactable = false;

            // changes the text of the search bar to a proper title bar
            Text text = bar.transform.GetChild(0).GetComponent<Text>();
            // set size
            text.transform.localScale = new Vector3(0.7692308f, 0.5f, 1);
            // set text
            text.text = "What is a storm surge?";
            // set color
            text.color = new Color(0, 0, 0);
            
            // game area
            iTween.MoveBy(gameArea, iTween.Hash("y", 1432, "default", 1f));
            // tries area
            iTween.MoveBy(triesArea, iTween.Hash("y", 837, "default", 1f));
            // keyboard
            iTween.MoveBy(keyboard, iTween.Hash("y", 618, "default", 2f));

            moved = true;
        }
    }
}
