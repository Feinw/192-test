/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Gene Tan

Code History:
1. Filbert Wee
   Change Date: March 8, 2020
   Change Description: Added user name input

File Creation
Date: March 8, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenUsernameInput : MonoBehaviour
{
    /*
    method name: Start
    routine's creation date: March 8, 2020
    purpose of the routine: Start is called before the first frame update. Used for initializing variables.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start()
    {
        // load name from save
        SharedVariables.username = SaveManager.LoadUsername();
        // if no name have been set before
        if (SharedVariables.username == "")
        {
            // allow input
            SceneManager.LoadScene("Scenes/Username", LoadSceneMode.Additive);
        }

        // because load progress upon opening game
        List<bool> minigames = SaveManager.LoadMinigames();
        foreach(bool x in minigames)
        {
            Debug.Log(x);
        }
        SearchStart.disabled = minigames[0];
        HangManGame.finished = minigames[1];
        StartGame.didwin = minigames[2];
        AnimationTrigger.minigameStart = minigames[3];
        AnimationTrigger.minigameDone = minigames[4];
    }
}
