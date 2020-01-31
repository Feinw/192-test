/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Filbert Wee

Code History:
1. Filbert Wee
   Change Date: January 20, 2020
   Change Description: Added ability to go into the messages "app"
2. Filbert Wee
   Change Date: January 24, 2020
   Change Description: Added ability to go into the home "page" and go back into the messages "app"

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
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    /*
    method name: ToMessagesFirstLaunch
    routine's creation date: January 20, 2020
    purpose of the routine: Allow player to open the messages "app"
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void ToMessagesFirstLaunch()
    {
        SceneManager.LoadScene("Scenes/Messages");
    }

    /*
    method name: ToHome
    routine's creation date: January 23, 2020
    purpose of the routine: Allow player go back into the home page while "saving" progress of player inside the messaging "app"
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void ToHome()
    {
        SceneManager.LoadScene("Scenes/HomeAfterMessages", LoadSceneMode.Additive);
    }

    /*
    method name: ToMessages
    routine's creation date: January 23, 2020
    purpose of the routine: Allow player go back into the messages page from the home page.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void ToMessages()
    {
        SceneManager.UnloadSceneAsync("Scenes/HomeAfterMessages");
    }

}
