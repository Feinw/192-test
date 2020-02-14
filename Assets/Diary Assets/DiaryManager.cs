/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Filbert Wee

Code History:
1. Filbert Wee
   Change Date: February 3, 2020
   Change Description: Added grabbing of variable from parser and generation of buttons

File Creation
Date: February 3, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryManager : MonoBehaviour
{
    // list of diary contents as pages
    public List<DiaryPage> pages;
    // script of which the diary file has been parsed
    public GameObject diaryParser;

    // buttons template inside the diary app
    public GameObject tabPrefab;
    // area in which the buttons above will be placed in
    public GameObject contentViewport;
    
    /*
    method name: Update
    routine's creation date: February 3, 2020
    purpose of the routine: Update is called once per frame. It is a forever loop.
                            In this case, the methods inside the update function should only be called once.
                            It has been placed inside the update method instead of the start method to ensure that it is runned after the 
                            diary file has been parsed.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Update()
    {
        // checks if the pages list is empty and fills it
        if (pages.Count == 0)
        {
            // grabs the pages list from the parser and transfers it here
            pages = diaryParser.GetComponent<DiaryParser>().pages;

            foreach(DiaryPage page in pages)
            {
                // generates the buttons inside the specified area
                GameObject tab = Instantiate(tabPrefab, contentViewport.transform);
                // the title of the diary content
                Text title = tab.transform.GetChild(0).GetComponent<Text>();
                // the actual text of the diary content
                Text text = tab.transform.GetChild(1).GetComponent<Text>();

                // gets the number of the diary page and passes it to DiaryPageVars script
                DiaryPageVars dp = tab.transform.GetComponent<DiaryPageVars>();
                dp.id = page.number;

                title.text = page.title;
                if (page.unlocked)
                {
                    text.text = page.text;
                }
                else
                {
                    text.text = "LOCKED";
                    // makes the button unclickable if the diary page has not yet been unlocked
                    tab.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

}
