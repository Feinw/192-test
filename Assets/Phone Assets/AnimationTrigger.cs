/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Gene Tan

Code History:
1. Gene Tan
   Change Date: Feb xx, 2020
   Change Description:  

File Creation
Date: Feb xx, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class AnimationTrigger : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    Animator anim2;
    // private static bool start;
    public Dictionary<string, List<DTypes>> story;  
    public Queue<string> conversation = new Queue<string>();
    public List<string> choices = new List<string>() {"Oh, nevermind.", "My friend got stuck."};
    public bool animationStart;
    public GameObject callButton, background;
    public float timer = 0.0f;
    public float callTimer = 0.0f;
    public float callCountdown = 3.0f;
    public float countDown = 1.0f;
    Text dialing;
    Text voice;
    public int count;
    public bool setBackgroundToFalse;
    void Start()
    {
        // start = false;
        animationStart = false;
        background.SetActive(false);
        anim = callButton.GetComponent<Animator>();
        anim2 = background.GetComponent<Animator>();
        dialing = background.transform.GetChild(0).GetComponent<Text>();
        dialing.text = "Calling";
        setBackgroundToFalse = false;
        count = 0;
        conversation.Enqueue("'Hello?'");
        conversation.Enqueue("'What's your emergency?'");
        voice = background.transform.GetChild(1).GetComponent<Text>();
        voice.text = "";

    }
    void Update()
    {
        // Debug.Log(animationStart);
        // Debug.Log(setBackgroundToFalse);
        if (animationStart) {
            if (count < 4) {
                if (timer > countDown) {
                    if (dialing.text != "Calling . . .") {
                        dialing.text += " .";
                    }
                    else {
                        count +=1;
                        dialing.text = "Calling";
                        
                    }
                    timer = timer - countDown;
                }
                timer += Time.deltaTime;
            }
            else {
                if (callTimer > callCountdown) {
                    dialing.text = "Connected";
                    if (conversation.Count != 0 && Phone.dialedNumber == "911") {
                        voice.text = conversation.Dequeue();
                    }
                    else {
                        dialing.text = "Call Ended";
                        anim2.SetTrigger("End");
                        anim.SetTrigger("End");
                        animationStart = false;      
                        timer = 0.0f;
                        setBackgroundToFalse = true;  
                        // background.SetActive(false);
                    }
                    callTimer = callTimer - callCountdown;
                }
                else {
                    callTimer += Time.deltaTime;
                }
            }
            
        }
        if (setBackgroundToFalse) {
            if (timer > 1.3f) {
                background.SetActive(false);
                setBackgroundToFalse = false;
                timer = 0.0f;
            }
            timer += Time.deltaTime;
        }

    }
    public void triggerAnim() {
        if (!animationStart && Phone.dialedNumber != "") {
            animationStart = true;
            timer = 0.0f;
            count = 0;
            callTimer = 0.0f;
            setBackgroundToFalse = false;
            background.SetActive(true);
            Debug.Log("showing background");
            anim.SetTrigger("Active");
            anim2.SetTrigger("Active");
            dialing.text = "Calling";
            voice.text = "";
            // start = true;
        }
        else if (animationStart && Phone.dialedNumber != "") {
            conversation.Clear();
            count = 4;
            callTimer = callCountdown;
        }
        else {
            // Debug.Log(animationStart);
            // Debug.Log(Phone.dialedNumber);
        }
    }

}
