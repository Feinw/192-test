/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Gene Tan

Code History:
1. Gene Tan
   Change Date: Feb 2, 2020
   Change Description: Created script to allow for dynamic/ changing text on the notification dropdown based
                        on what's happening in ChatManager.cs

File Creation
Date: February 2, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is connected to the NotifDropdown and Messages Scenes
public class NotifText : MonoBehaviour
{
    [SerializeField]
    // will contain the text that will be shown on the notification bar body
    public static string text;
    // will contain the text that will be shown on the notification bar title
    public static string title;
    // boolean signifying whether the notification should drop down or not
    public static bool dropdown;

    /*
    method name: Start
    routine's creation date: February 1, 2020
    purpose of the routine: initializes values, more importantly sets dropdown to false as the notification
                            bar should be out of the screen when this is first called.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start()
    {
        text = "oh no";
        dropdown = false;
        
    }

}
