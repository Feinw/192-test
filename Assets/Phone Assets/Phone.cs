/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Gene Tan

Code History:
1. Gene Tan
   Change Date: February 19, 2020
   Change Description: Added code to start(), buttonPressed(), callNumber(), eraseNumber() 

File Creation
Date: February 19, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    
    [SerializeField]
    // contains the number dialed by the player
    public static string dialedNumber;

    // contains the game object containing the number dialed by the player
    public GameObject dialing;
    
    // contains the text object of the dialing game object
    Text dialingText;

    /*
    method name: Start
    routine's creation date: February 19, 2020
    purpose of the routine: This routine is for initializing variables, namely the dialed number, the game object of the number being dialed and its text object
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start()
    {
        dialedNumber = "";
        dialingText = dialing.transform.GetChild(0).GetComponent<Text>();
        dialingText.text = dialedNumber;
    }

    /*
    method name: buttonPressed
    routine's creation date: February 19, 2020
    purpose of the routine: This routine is for adding numbers into the dialed number string every time a button is pressed 
                            on the UI. If the dialed number's length is greater than 11 or if the animation
                            has begun, nothing should happen when any button is pressed (except for the call and erase button)
    a list of the calling arguments: number, the string containing the number attached to the button
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void buttonPressed(string number) 
    {
        if (dialedNumber.Length < 11 && AnimationTrigger.animationStart == false) 
        { 
            dialedNumber = dialedNumber + number;
            Debug.Log(dialedNumber);
            dialingText.text = dialedNumber;
        }
    }




    /*
    method name: eraseNumber
    routine's creation date: February 19, 2020
    purpose of the routine: This routine is for deleting numbers from the dialedNumber string. Whenever the player clicks the
                            erase button on the UI, the rightmost character should be erased. The button shouldn't do anything
                            when it is clicked while the animation has begun or while no number has been dialed.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */

    public void eraseNumber() 
    {
        if (dialedNumber.Length > 0 && AnimationTrigger.animationStart == false) 
        {
            dialedNumber = dialedNumber.Remove(dialedNumber.Length - 1, 1); 
            dialingText.text = dialedNumber;
        }
    }


}
