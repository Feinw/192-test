/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Nephia Dalisay

Code History:
1. Nephia Dalisay
   Change Date: March 12, 2020
   Change Description: Added code to prevent minigame2 from opening once finished

File Creation
Date: March 12, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGame : MonoBehaviour
{
	/*
    method name: willOpen()
    routine's creation date: Mar 12, 2020
    purpose of the routine: Determines whether of not Minigame2 (Spot the Difference) has already been finished, if it has, the minigame cannot be opened/clicked again.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void willOpen()
    {
    	if (!StartGame.didwin)
    	{
    		SceneChange.ToMinigame2();
    	}
    }
}
