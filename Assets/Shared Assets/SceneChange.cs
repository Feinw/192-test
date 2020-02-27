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
3. Gene Tan
    Change Date: February 2, 2020
    Change Description: Added ability to go into the phone app
4. Nephia Dalisay
    Change Date: Feb 9, 2020
    Change Description: Added method/ability to go into the Minigame 2 scene.
5. Nephia Dalisay
    Change Date: Feb 12, 2020
    Change Description: Added method/ability to go back to diary main menu from specific page.
6. Nephia Dalisay
    Change Date: Feb 25, 2020
    Change Description: Edited ToMinigame2() method, added ToMessagesAfterMG2() and ToMessagesAfterMG2Win(), which are methods to go back into the messages app from the Spot the Difference minigame

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

        // rename later one to load messages
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
    method name: ToHomeFromAnywhereElse
    routine's creation date: February 3, 2020
    purpose of the routine: Allow player go back into the home page while from anywhere other than the messaging app
    a list of the calling arguments: string currentScene
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void ToHomeFromAnywhereElse(string currentScene)
    {
        SceneManager.UnloadSceneAsync(currentScene);
    }
    /*
    method name: ToMessages
    routine's creation date: January 23, 2020
    purpose of the routine: Allow player go back into the messages page from the home page.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */

        // rename later one to unload to messages
    public void ToMessages()
    {
        SceneManager.UnloadSceneAsync("Scenes/HomeAfterMessages");
    }
    /*
    method name: ToAnywhereElse
    routine's creation date: February 3, 2020
    purpose of the routine: Allow player go back into other apps from the home page.
    a list of the calling arguments: string currentScene
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void ToAnywhereElse(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }
    /*
    method name: ToMinigame2
    routine's creation date: February 9, 2020
    purpose of the routine: Allow player go back into the scene of Minigame 2 (Spot the Difference).
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void ToMinigame2()
    {
        SceneManager.LoadScene("Scenes/HomeAfterMessages", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scenes/Minigame2", LoadSceneMode.Additive);
    }

    /*
    method name: ToDiaryMainFromPage
    routine's creation date: February 12, 2020
    purpose of the routine: Allow player go back into the main menu of the diary from a specific page
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void ToDiaryMainFromPage()
    {
        SceneManager.UnloadSceneAsync("Scenes/Diary");
        SceneManager.LoadScene("Scenes/Diary", LoadSceneMode.Additive);
    }

    public void ToMessagesAfterMG2()
    {
        SceneManager.UnloadSceneAsync("Scenes/Minigame2");
    }

    public void ToMessagesAfterMG2Win()
    {
        SceneManager.UnloadSceneAsync("Scenes/Minigame2");
    }

}
