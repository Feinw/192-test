/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Gene Tan

Code History:
1. Gene Tan
   Change Date: Feb 2, 2020
   Change Description: Added code to make the Notification Dropdown appear when the message scene is opened and to
                        keep the notification dropdown "persistent" even on scene change/ scene add.

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
using UnityEngine.SceneManagement;

// this function is connected to the Messages scene
public class GlobalControl : MonoBehaviour
{
    // variable to make sure code only runs once (just in case)
    public bool test = false;

    /*
    method name: Start
    routine's creation date: February 2, 2020
    purpose of the routine: This function is to "activate" or load the NotifDropdown scene after the Messages app has been 
                            opened for the first time.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start()
    {
        if (!test)
        {
            SceneManager.LoadSceneAsync("Scenes/NotifDropdown", LoadSceneMode.Additive);
            test = true;
        }
                
    }
}
