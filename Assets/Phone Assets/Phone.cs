/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Gene Tan

Code History:
1. Gene Tan
   Change Date: Feb 2, 2020
   Change Description: Added code to start(), buttonPressed(), callNumber(), eraseNumber() 

File Creation
Date: Feb 2, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public static string dialedNumber;
    public GameObject dialing;
    Text temp;
    void Start()
    {
        dialedNumber = "";
        temp = dialing.transform.GetChild(0).GetComponent<Text>();
        temp.text = dialedNumber;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void buttonPressed(string number) {
        if (dialedNumber.Length < 11) { 
            dialedNumber = dialedNumber + number;
            Debug.Log(dialedNumber);
            temp.text = dialedNumber;
        }
    }


    public void callNumber() {
        // iTween.ValueTo(callButton, iTween.Hash("from", "08B34E", "to", Color.red, "time", 1f, "onupdate", "updatingColor"));
        if (dialedNumber == "911") 
        {
            Debug.Log("perfect");
        }
        else
        {
            Debug.Log("wrong");
            
        }
    }

    public void eraseNumber() {
        if (dialedNumber.Length > 0) {
        dialedNumber = dialedNumber.Remove(dialedNumber.Length - 1, 1); 
        temp.text = dialedNumber;
        }
    }

    // public void updatingColor(Color color) {
    //     Image temp = callButton.GetComponent<Image>();
    //     temp.color = color;

    // }

}
