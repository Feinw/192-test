/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Nephia Dalisay

Code History:
1. Nephia Dalisay
   Change Date: Feb. 25, 2020
   Change Description: Added FoundDocuments() method.
2. Nephia Dalisay
   Change Date: Feb 26, 2020
   Change Description: Added static bool, didwin, to check if player has finished the minigame


File Creation
Date: February 9, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class StartGame : MonoBehaviour
{
	// all game objects on background
    public GameObject HomeButton;
    public GameObject UpperPhoto;
    public GameObject LowerPhoto;
    public GameObject PlayButton;
    public GameObject Instructions;
    public GameObject Winner;
    public GameObject FoundObject;
    public Text txt;

    // game objects in the photo
    public GameObject Dog;
    public GameObject CeilingFan;
    public GameObject WindowCracks;
    public GameObject Documents;

    // stores player's score
    static int score;

    // if player has finished game or not
    public static bool didwin;

    // if minigame start button has been clicked
    private bool moved;

    
    /*
    method name: Start
    routine's creation date: Feb 9 2020
    purpose of the routine: Initializes variables
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start()
    {
        moved = false;
        score = 0;
        didwin = false;
    }

    /*
    method name: Update
    routine's creation date: Feb 9 2020
    purpose of the routine: Checks if player has reached winning score, then calls the GameWon method
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Update()
    {
        if (score >= 4)
        {
            GameWon();
        }
    }

    /*
    method name: StartIt
    routine's creation date: Feb 9 2020
    purpose of the routine: Moves gameobjects to be able to play the actual minigame
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void StartIt()
    {
        if (!moved)
        {
            RectTransform HomeRT = HomeButton.GetComponent<RectTransform>();
            RectTransform UpperRT = UpperPhoto.GetComponent<RectTransform>();
            RectTransform LowerRT = LowerPhoto.GetComponent<RectTransform>();
            RectTransform PlayRT = PlayButton.GetComponent<RectTransform>();
            RectTransform InstRT = Instructions.GetComponent<RectTransform>();

            HomeRT.anchoredPosition = new Vector3(333, 499, 0);
            UpperRT.anchoredPosition = new Vector3(-2, 192, 0);
            LowerRT.anchoredPosition = new Vector3(-2, -332, 0);
            PlayRT.anchoredPosition = new Vector3(659, -571, 0);
            InstRT.anchoredPosition = new Vector3(-60, 565, 0);

            moved = true;
        }

        RectTransform FoundRT = FoundObject.GetComponent<RectTransform>();
        FoundRT.anchoredPosition = new Vector3(0, -652, 0);
    }

    /*
    method name: AddScore
    routine's creation date: Feb 9 2020
    purpose of the routine: Adds +1 to player's score everytime they click on a correct area
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void AddScore()
    {
        score = score + 1;
        
        string scorestring = score.ToString();
        txt.text = "Score: " + score;
        Debug.Log("score is ");
        Debug.Log(score);
    }

    /*
    method name: FoundDog
    routine's creation date: Feb 9 2020
    purpose of the routine: Adds to player's score after they've found the dog gameobject, then destroys this gameobject afterwards.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void FoundDog()
    {
        Destroy(Dog);
        AddScore();
    }

    /*
    method name: FoundDFan
    routine's creation date: Feb 9 2020
    purpose of the routine: Adds to player's score after they've found the ceiling fan gameobject, then destroys this gameobject afterwards.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void FoundFan()
    {
        Destroy(CeilingFan);
        AddScore();
    }

    /*
    method name: Found
    routine's creation date: Feb 9 2020
    purpose of the routine: Adds to player's score after they've found the windows cracks gameobject, then destroys this gameobject afterwards.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void FoundCracks()
    {
        Destroy(WindowCracks);
        AddScore();
    }

    /*
    method name: FoundDocuments
    routine's creation date: Feb 9 2020
    purpose of the routine: Adds to player's score after they've found the dpocuments gameobject, then destroys this gameobject afterwards.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void FoundDocuments()
    {
    	Destroy(Documents);
    	AddScore();
    }

    /*
    method name: GameWon
    routine's creation date: Feb 9 2020
    purpose of the routine: Method for when the player finds all differences (gameobjects). The objects are moved to show a different screen saying they have fininshed the minigame.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void GameWon()
    {
        RectTransform WonRT = Winner.GetComponent<RectTransform>();
        WonRT.anchoredPosition = new Vector3(-4,7,0);

        RectTransform UpperRT = UpperPhoto.GetComponent<RectTransform>();
        RectTransform LowerRT = LowerPhoto.GetComponent<RectTransform>();
        RectTransform PlayRT = PlayButton.GetComponent<RectTransform>();
        RectTransform InstRT = Instructions.GetComponent<RectTransform>();
        RectTransform FoundRT = FoundObject.GetComponent<RectTransform>();

        UpperRT.anchoredPosition = new Vector3(-810, -189, 0);
        LowerRT.anchoredPosition = new Vector3(-810, -189, 0);
        PlayRT.anchoredPosition = new Vector3(-810, -189, 0);
        InstRT.anchoredPosition = new Vector3(-810, -189, 0);
        FoundRT.anchoredPosition = new Vector3(-0, -652, 0);

        didwin = true;
    }
}
