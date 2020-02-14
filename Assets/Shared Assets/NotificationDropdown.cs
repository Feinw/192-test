/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Gene Tan

Code History:
1. Gene Tan
   Change Date: February 1, 2020
   Change Description: Transferred the Notification Dropdown code from ChatManager.cs to this file

File Creation
Date: February 1, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NotificationDropdown : MonoBehaviour
{
    [SerializeField]
    // the timer (will start from 0 up to the countdown)
    public float alertTimer;
    // dictates how long the notifdropdown will linger before it moves up
    public float alertCountdown;

    // will contain the Notification Bar Dropdown game object
    public GameObject notifBar;
    // dictates how far the notification bar will move down or up the y axis
    public int notifMove; 

    // vector containing the position of the notification bar 
    public Vector3 notifBarPosition;

    // boolean to check whether to perform the code in Update() or not
    public bool currentlyNotifying;
    // boolean to help the program know whether the move the notifbar up or down
    public bool notifyingDone;
    // variable for debugging
    private bool test = false;

    // will contain the text/body to be displayed on the notif bar
    public string text;

    // will contain title to be displayed on the notif bar
    public string title;

    /*
    method name: Start
    routine's creation date: February 1, 2020
    purpose of the routine: Initializes some of the variable
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start()
    {   
        text ="ohno";
        notifBarPosition = notifBar.transform.position;
        currentlyNotifying = false;
        notifyingDone = false;
    }

    /*
    method name: Update
    routine's creation date: February 1, 2020
    purpose of the routine: Function that allows the notif bar to drop down when there is a notification for the player
                            (currently whenever a diary page is unlocked). Also has a timer for how long the notification will
                            stay on until it moves back up out of the screen. 
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Update()
    {
        // Updates the text on the notification bar
        Text temp = notifBar.transform.GetChild(1).GetComponent<Text>();
        Debug.Log(temp);
        temp.text = NotifText.text;
        temp = notifBar.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        temp.text = NotifText.title;
        
        
        if (currentlyNotifying == true || NotifText.dropdown == true)
        {
            // if first time going through this if statement while the notif bar is out of the screen,
            // call this "function"
            if (!notifyingDone) {
                alert();
                // set to true so "alert()" only gets called once every notification drop down
                notifyingDone = true;
                
                currentlyNotifying = true;
                NotifText.dropdown = false;
            }
            // if the timer is up, call "removeAlert()" and reset timer and other boolean variables
            if (alertTimer >= alertCountdown) {
                removeAlert();
                alertTimer = 0;
                currentlyNotifying = false;
                notifyingDone = false;
            }
            // increment the timer
            alertTimer += Time.deltaTime;
        }
        
    }
    /*
    method name: alert
    routine's creation date: February 1, 2020
    purpose of the routine: moves the notification bar down (into the screen)
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void alert() {
        notifBarPosition.y -= notifMove;
        iTween.MoveTo(notifBar, iTween.Hash("position", notifBarPosition, "time", 1.0f, "easeType", iTween.EaseType.easeInOutSine));
    }
    /*
    method name: removeAlert
    routine's creation date: February 1, 2020
    purpose of the routine: moves the notification bar up (out of the screen)
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void removeAlert() {
        notifBarPosition.y += notifMove;
        iTween.MoveTo(notifBar, iTween.Hash("position", notifBarPosition, "time", 1.0f, "easeType", iTween.EaseType.easeInOutSine));
    }

}
