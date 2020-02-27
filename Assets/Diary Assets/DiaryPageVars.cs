/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Nephia Dalisay

Code History:
1. Nephia Dalisay
   Change Date: February 10, 2020
   Change Description: Instantiates (opens) new page if user clicks on a specific diary page

2. Nephia Dalisay
    Change Date: February 12, 2020
    Change Description: Able to read from tsv file, added functional back button that allows player to go from specific diary page back to diary menu

File Creation
Date: Feb 1, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DiaryPageVars : MonoBehaviour
{
    // id number of a specific diary page
    public int id;

    // both variables are used in parsing the diary page
    public int contentid;
    public string actualcontent;

    // Back button game object
    public GameObject BackButton;

    // contains parent of the game objects (where they will be placed)
    public GameObject placeholder;

    // background of diary page
    public GameObject SelectedPagePrefab;

    // diary page title
    public Text PageTitle;

    //diary page content
    public Text PageContent;

    //actual instantiated page
    GameObject page;

    // an list containing the lines read from the .tsv file
    public List<string> content;

    public TextAsset diaryPageContents;

    /*
    method name: Start
    routine's creation date: Feb 10, 2020
    purpose of the routine: Start is called before the first frame update. Usually used for initializing variables.
                            This routine reads the .tsv file and converts each line into objects and stored in a list. 
                            The types are the keys in the dictionary and the lines in the file (minus the type) are the
                            values.
    a list of the calling arguments:
    a list of required files and/or database tables: Page Contents.tsv
    and return value: N/A
    */
    void Start()
    {
        content = new List<string>();
        
        string[] splits = new string[] { "\r", "\n", "\t\t" };
        string[] words = diaryPageContents.text.Split(splits, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < words.Length; i++)
        {
            content.Add(words[i]);
        }

        foreach (string scontent in content)
        {
            string[] parsedcontent = scontent.Trim().Split("\t"[0]);
            contentid = int.Parse(parsedcontent[0]);
            if(contentid == id)
            {
                actualcontent = parsedcontent[1];
            }
        }
    }

    /*
    method name: Open
    routine's creation date: Feb 10, 2020
    purpose of the routine: Instantiates the diary page selected by the user.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void Open()
    {
        page = Instantiate(SelectedPagePrefab, new Vector3(540,800,0), transform.rotation);
        page.transform.parent = GameObject.Find("New Screen Parent").transform;

        GameObject back = Instantiate(BackButton, new Vector3(100, 1700, 0), transform.rotation);
        back.transform.parent = GameObject.Find("New Screen Parent").transform;

        Text titletxt = Instantiate(PageTitle, new Vector3(540, 1400, 0), transform.rotation);
        titletxt.transform.parent = GameObject.Find("New Screen Parent").transform;

        Text contenttxt = Instantiate(PageContent, new Vector3(540, 800, 0), transform.rotation);
        contenttxt.transform.parent = GameObject.Find("New Screen Parent").transform;

        id = id + 1;
        titletxt.text = "Page Number " + id;
        contenttxt.text = actualcontent;
        Debug.Log(PageTitle.text);
    }

}
