/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Filbert Wee, Gene Tan

Code History:
1. Filbert Wee
   Change Date: March 8, 2020
   Change Description: Added storing of username
2. Gene Tan
   Change Date: March 12, 2020
   Change Description: Added bool atMessages

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

public class SharedVariables : MonoBehaviour
{
    // variable for storing username all-throughout the game
    public static string username = "";
    // bool that keeps track if the player is on the messaging app or not (values are changed in SceneChange.cs)
    public static bool atMessages = false;
}
