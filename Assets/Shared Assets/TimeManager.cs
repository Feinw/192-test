/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Filbert Wee

Code History:
1. Filbert Wee
   Change Date: January 20, 2020
   Change Description: Added ability to go see system time within the game

File Creation
Date: January 20, 2019
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    // this is the text UI that will be displayed
    public Text time;

    /*
    method name: Start
    routine's creation date: January 20, 2020
    purpose of the routine: Start is called before the first frame update. Usually used for initializing variables.
                            This routine sets the in game time to system time.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start () {
        if (time == null)
        {
            time = GetComponent<Text>();
        }
        else
        {
            time.text = System.DateTime.Now.ToString("hh:mm tt");
        }
    }

    /*
    method name: Update
    routine's creation date: January 20, 2020
    purpose of the routine: Update is called once per frame. It is a forever loop.
                            This routine updates the in game time to system time.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Update ()
    {
        if (time != null)
        {
            time.text = System.DateTime.Now.ToString("hh:mm tt");
        }
    }
}
