/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Nephia Dalisay, Gene Tan, Filbert Wee

Code History:
1. Filbert Wee
   Change Date: March 8, 2020
   Change Description: Added way of inputting username

File Creation
Date: March 8, 2019
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserName : MonoBehaviour
{
    // text input field
    public InputField field;

    /*
    method name: addName
    routine's creation date: March 8, 2020
    purpose of the routine: Grabs text from input and stores it
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void addName()
    {
        // if not blank
        if (string.Compare(field.text.Trim(), "") != 0)
        {
            // remove whitespace
            SharedVariables.username = field.text.Trim();
            // store name
            SaveManager.SaveUsername(SharedVariables.username);
            SceneManager.UnloadSceneAsync("Scenes/Username");
        }
    }
}
