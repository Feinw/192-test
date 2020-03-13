/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Nephia Dalisay, Gene Tan, Filbert Wee

Code History:
1. Filbert Wee
   Change Date: March 8, 2020
   Change Description: Added where diary page moves
2. Gene Tan
   Change Date: March 11, 2020
   Change Description: Improved code -- Changed movement of page from horizontal to vertical; also added code to prevent multiple consecutive "move" and "moveback" movements of the page

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

public class PageContent : MonoBehaviour
{
    // diary page number
    [SerializeField]
    public int index;
    // content area of diary page
    GameObject viewArea;
    // bool to prevent something from tweening when another one isnt finish yet
    public static bool canTween = false;

    /*
    method name: Start
    routine's creation date: March 8, 2020
    purpose of the routine: Start is called before the first frame update. Used for initializing variables.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void Start()
    {
        // get index
        index = gameObject.transform.GetSiblingIndex();
        // get entire "tab" of the diary page
        viewArea = gameObject.transform.parent.parent.parent.gameObject;
        // boolean to check if gameObject not busy moving
        canTween = true;
    }

    /*
    method name: move
    routine's creation date: March 8, 2020
    purpose of the routine: Used to move the diary page into the screen
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */  
    public void move()
    {
        if (canTween)
        {
            canTween = false;
            // get the child
            GameObject page = viewArea.transform.GetChild(index + 2).gameObject;
            // move to screen
            iTween.MoveBy(page, iTween.Hash("y", page.transform.GetComponent<RectTransform>().rect.height, "default", 1f, "onComplete", "allowTween", "onCompleteTarget", gameObject));
        }
    }
    /*
    method name: moveback
    routine's creation date: March 8, 2020
    purpose of the routine: Used to move the diary page out of the screen
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void moveback()
    {
        if (canTween){
            canTween = false;
            // get the child
            GameObject page = gameObject.transform.parent.parent.gameObject;
            // move out of screen
            iTween.MoveBy(page, iTween.Hash("y", (-1)*page.transform.GetComponent<RectTransform>().rect.height, "default", 1f, "onComplete", "allowTween", "onCompleteTarget", gameObject));
        }
    }
    /*
    method name: allowTween
    routine's creation date: March 11, 2020
    purpose of the routine: Function to change the value of "canTween" after the "move" and "moveback" animations
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void allowTween()
    {
        canTween = true;
    }
}
