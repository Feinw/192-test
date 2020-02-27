/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Gene Tan

Code History:
1. Gene Tan
   Change Date: February 4, 2020
   Change Description: Created preparatory code to get started on the phone minigame functionality 
2. Gene Tan
    Change Date: February 22, 2020
    Change Description:  Completed phone minigame functionality

File Creation
Date: February 4, 2020
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
    Animator anim3;
    // private static bool start;
    public Dictionary<string, List<DTypes>> story;  
    public Queue<string> conversation = new Queue<string>();
    public Queue<string> choices = new Queue<string>();
    // public List<string> choices = new List<string>() {"Oh, nevermind.", "My friend got stuck."};
    public static bool animationStart;
    public GameObject callButton, background, choicesButton, replyArea, dialed;
    public float timer = 0.0f;
    public float callTimer = 0.0f;
    public float callCountdown = 3.0f;
    public float countDown = 1.0f;
    Text dialing;
    Text voice;
    public int count;
    public bool setBackgroundToFalse;

    public static bool minigameStart = false;
    public static bool conversationEnd = false;
    public static bool operatorIsTalking = true;
    public static bool minigameSuccess = false;
    public static bool minigameDone = true;

    public bool doThisOnce = true;

    DTypes placeholder;
    void Start()
    {
        // start = false;
        animationStart = false;
        background.SetActive(false);
        anim = callButton.GetComponent<Animator>();
        anim2 = background.GetComponent<Animator>();
        anim3 = dialed.GetComponent<Animator>();
        dialing = background.transform.GetChild(0).GetComponent<Text>();
        dialing.text = "Calling";
        setBackgroundToFalse = false;
        count = 0;
        // conversation.Enqueue("'Hello?'");
        // conversation.Enqueue("'What's your emergency?'");
        voice = background.transform.GetChild(1).GetComponent<Text>();
        voice.text = "";

    }
    void Update()
    {

        // Debug.Log(animationStart);
        // Debug.Log(setBackgroundToFalse);
        if (minigameStart && story != null && doThisOnce) {
            fetchMessages(0);
            minigameDone = false;
            doThisOnce = false;
            // minigameStart = false;
        }
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
                if (Phone.dialedNumber == "911" && !minigameDone) {
                    if (callTimer > callCountdown) {
                        dialing.text = "Connected";
                        if (conversation.Count != 0) {
                            voice.text = conversation.Dequeue();
                        }
                        else if (!conversationEnd && operatorIsTalking) {
                            letPlayerReply();
                            operatorIsTalking = false;
                        }
                        else if (conversation.Count == 0 && conversationEnd) {
                            endCall();
 
                            // conversationEnd = false;
                            // operatorIsTalking = true;
                            // background.SetActive(false);
                        }
                        callTimer = callTimer - callCountdown;
                    }
                    else {
                        callTimer += Time.deltaTime;
                    }
                }
                else {
                    endCall();
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
        // Debug.Log(story["phone"][0].text);
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
            anim3.SetTrigger("Active");
            dialing.text = "Calling";
            voice.text = "";
            operatorIsTalking = true;
            // start = true;
        }
        else if (animationStart && Phone.dialedNumber != "") {
            endCall();
            // conversation.Clear();
            // count = 4;
            // callTimer = callCountdown;
        }
        else {
            // Debug.Log(animationStart);
            // Debug.Log(Phone.dialedNumber);
        }
    }

    //creation date: Feb 23
    public void fetchMessages(int index) {
        for (int i = 0; i < replyArea.transform.childCount; i++)
            {
            // and deletes/destroys them
                Destroy (replyArea.transform.GetChild(i).gameObject);
            }
        placeholder = story["phone"][index];
        while (placeholder.nextType == "phone") {
            conversation.Enqueue(placeholder.text);
            placeholder = story[placeholder.nextType][placeholder.nextNumber];
        }
        operatorIsTalking = true;
        conversation.Enqueue(placeholder.text);
        if (placeholder.nextType == "stop") {
            Debug.Log("tapos na minigame");
            conversationEnd = true;
            Debug.Log(conversationEnd);
            Debug.Log(conversation.Count);
            // operatorIsTalking = false;
        }
        
        // else {
        //     operatorIsTalking = true;
        // }
    }

    //creation date: Feb 23
    public void letPlayerReply() {
        Debug.Log("instantiating options");
        Debug.Log(placeholder.text);
        Debug.Log(placeholder.choices.Count);
        foreach (int index in placeholder.choices) {
            DTypes option = story["phoneChoice"][index];
            GameObject button = Instantiate(choicesButton, replyArea.transform);
            Button bt = button.GetComponent<Button>();

            Text text = button.transform.GetChild(0).gameObject.GetComponent<Text>(); //get component text
            text.text = option.text;

            // sets the required onClick script
            bt.onClick.AddListener(delegate { conversation.Enqueue(option.text); callTimer = 3.0f; fetchMessages(option.nextNumber); });
        }
    }

//creation date: Feb 23
    public void endCall() {
        dialing.text = "Call Ended";
        anim3.SetTrigger("End");
        anim2.SetTrigger("End");
        anim.SetTrigger("End");
        animationStart = false;      
        timer = 0.0f;
        setBackgroundToFalse = true; 
        for (int i = 0; i < replyArea.transform.childCount; i++)
            {
            // and deletes/destroys them
                Destroy (replyArea.transform.GetChild(i).gameObject);
            }
        if (Phone.dialedNumber == "911" && placeholder.nextType == "stop") {
            if (placeholder.nextNumber == 0) {
                minigameDone = true;
                minigameSuccess = true;
            }
            else {
                minigameDone = true;
                minigameSuccess = false;
            }
        }
        else if(Phone.dialedNumber == "911" && placeholder.nextType != "stop") {
            // conversationEnd = true;
            minigameDone = true;
            minigameSuccess = false;
        }
    }

}
