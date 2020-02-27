/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Nephia Dalisay

Code History:
1. Nephia Dalisay
   Change Date: Feb. 25, 2020
   Change Description: Added FoundDocuments() method.


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
    public GameObject HomeButton;
    public GameObject UpperPhoto;
    public GameObject LowerPhoto;
    public GameObject PlayButton;
    public GameObject Instructions;
    public GameObject Winner;

    public GameObject Dog;
    public GameObject CeilingFan;
    public GameObject WindowCracks;
    public GameObject Documents;

    public GameObject FoundObject;

    static int score;
    public static bool didwin;

    private bool moved;

    public Text txt;

    // Start is called before the first frame update
    void Start()
    {
        moved = false;
        score = 0;
        didwin = false;
    }

    // Update is called once per frame


    void Update()
    {
        if (score >= 4)
        {
            GameWon();
        }
    }

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

    void AddScore()
    {
        score = score + 1;
        
        string scorestring = score.ToString();
        txt.text = "Score: " + score;
        Debug.Log("score is ");
        Debug.Log(score);
    }

    public void FoundDog()
    {
        Destroy(Dog);
        AddScore();
    }

    public void FoundFan()
    {
        Destroy(CeilingFan);
        AddScore();
    }

    public void FoundCracks()
    {
        Destroy(WindowCracks);
        AddScore();
    }

    public void FoundDocuments()
    {
    	Destroy(Documents);
    	AddScore();
    }

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
