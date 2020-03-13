/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Gene Tan

Code History:
1. Filbert Wee
   Change Date: March 6, 2020
   Change Description: Added reset progress button
2. Filbert Wee
   Change Date: March 8, 2020
   Change Description: Fixed bug on resetting messages

File Creation
Date: March 6, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    /*
    method name: resetAll
    routine's creation date: March 6, 2020
    purpose of the routine: Resets progress and saving that reset
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void resetAll()
    {
        SearchStart.disabled = true;
        HangManGame.finished = false;
        StartGame.didwin = false;
        AnimationTrigger.minigameStart = false;
        AnimationTrigger.minigameDone = false;
        // collects minigame reset and saving it
        List<bool> x = new List<bool>(new bool[] { true, false, false, false, false });
        SaveManager.SaveMinigames(x);

        // reset messages to first message and saving it
        List<SavedMessage> messages = new List<SavedMessage>();
        SavedMessage firstMessage = new SavedMessage
        {
            type = "intro",
            number = 0
        };
        messages.Add(firstMessage);
        SaveManager.SaveMessages(messages);
        // reset messages
        ChatManager.save = messages;
    }
}
