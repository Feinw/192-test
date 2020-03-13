/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Gene Tan

Code History:
1. Gene Tan
   Change Date: February 4, 2020
   Change Description: Created preparatory code to get started on the phone minigame functionality (for experimentation)
2. Gene Tan
   Change Date: February 19, 2020
   Change Description: Semi-finalized animations for the phone minigame
3. Gene Tan
    Change Date: February 23, 2020
    Change Description: Completed phone minigame functionality

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
    // contains the animator for the call button
    Animator anim;
    // contains the animator for the phone background
    Animator anim2;
    // contains the animator for the dialed number text
    Animator anim3;
    
    // dictionary to contain the dialogues for the phone minigame
    public Dictionary<string, List<DTypes>> story;  

    // queue to contain the operator's "lines"
    public Queue<string> conversation = new Queue<string>();

    // queue to contain the stuff the player can reply with
    public Queue<string> choices = new Queue<string>();

    // bool to signal when to start the "calling" animation
    public static bool animationStart;

    // game objects to store the call button, background, dialed number, buttons for the choices, and the area where the buttons will instantiate
    public GameObject callButton, background, choicesButton, replyArea, dialed;

    // timer for how long until the call will end or until the "other side picks up"
    public float timer = 0.0f;

    // timer for how long until the call will end or until the "other side picks up"
    public float countDown = 1.0f;

    // count for number of times the timer will repeat
    public int count;

    // timer for dialogue (how fast the messages will show)
    public float callTimer = 0.0f;

    // timer for dialogue (how fast the messages will show)
    public float callCountdown = 3.0f;

    // contains the text for the dialedNumber
    Text dialing;

    // contains the text for where the operator's messages will show up
    Text voice;

    // bool to determine whether or not to set the background to active or inactive
    public bool setBackgroundToFalse;

    // bool that determines whether the minigame has begun or not
    public static bool minigameStart = false;

    // bool that determines whether the conversation with the operator has ended or not
    public static bool conversationEnd = false;

    // bool that determines when operator is talking
    public static bool operatorIsTalking = true;

    // bool that determines if the player is successful with the minigame
    public static bool minigameSuccess = false;

    // bool that determines if the minigame is completed
    public static bool minigameDone = false;

    // bool that ensures that the conversation with the operator can only happen once
    public bool doThisOnce = true;

    // placeholder variable to contain the items in the dictionary
    public DTypes placeholder;

    /*
    method name: Start
    routine's creation date: February 4, 2020
    purpose of the routine: This routine is for initializing variables
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start()
    {
        animationStart = false;
        background.SetActive(false);
        anim = callButton.GetComponent<Animator>();
        anim2 = background.GetComponent<Animator>();
        anim3 = dialed.GetComponent<Animator>();
        dialing = background.transform.GetChild(0).GetComponent<Text>();
        dialing.text = "Calling";
        setBackgroundToFalse = false;
        count = 0;
        voice = background.transform.GetChild(1).GetComponent<Text>();
        voice.text = "";

    }
    /*
    method name: Update
    routine's creation date: February 4, 2020
    purpose of the routine: Update is called once per frame. It is a forever loop.
                            This routine decides each frame if the number is being dialed,
                            if the call has been connected, if the call has been ended, if the operator
                            is talking, or if player is allowed to choose a reply or not.
                            This function is also what makes use of the timer variables.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Update()
    {
        if (minigameStart && story != null && doThisOnce) 
        {
            fetchMessages(0); 
            minigameDone = false;
            doThisOnce = false;
        }
        if (animationStart) 
        {
            if (count < 4) 
            {
                if (timer > countDown) 
                {
                    if (dialing.text != "Calling . . .") 
                    {
                        dialing.text += " .";
                    }
                    else 
                    {
                        count +=1;
                        dialing.text = "Calling";
                        
                    }
                    timer = timer - countDown;
                }
                timer += Time.deltaTime;
            }
            else 
            {
                if (Phone.dialedNumber == "911" && (!minigameDone && minigameStart)) 
                {
                    if (callTimer > callCountdown) 
                    {
                        dialing.text = "Connected";
                        if (conversation.Count != 0) 
                        {
                            voice.text = conversation.Dequeue();
                        }
                        else if (!conversationEnd && operatorIsTalking) 
                        {
                            letPlayerReply();
                            operatorIsTalking = false;
                        }
                        else if (conversation.Count == 0 && conversationEnd) 
                        {
                            endCall();

                        }
                        callTimer = callTimer - callCountdown;
                    }
                    else 
                    {
                        callTimer += Time.deltaTime;
                    }
                }
                else 
                {
                    endCall();
                }
            }
            
        }
        if (setBackgroundToFalse) 
        {
            if (timer > 1.3f) 
            {
                background.SetActive(false);
                setBackgroundToFalse = false;
                timer = 0.0f;
            }
            timer += Time.deltaTime;
        }

    }
    /*
    method name: triggerAnim
    routine's creation date: February 4, 2020
    purpose of the routine: This function is called to trigger the calling animation, namely the call button changing to red,
                            the phone background fading to black, and the dialed number fading to white. This function also
                            "re-initializes" or resets some of the values, including the timers, the counts, and some boolean variables
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void triggerAnim() 
    {
        // Debug.Log(story["phone"][0].text);
        if (!animationStart && Phone.dialedNumber != "") 
        {
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
        else if (animationStart && Phone.dialedNumber != "") 
        {
            endCall();
            // conversation.Clear();
            // count = 4;
            // callTimer = callCountdown;
        }
        else 
        {
            // Debug.Log(animationStart);
            // Debug.Log(Phone.dialedNumber);
        }
    }
    /*
    method name: fetchMessages
    routine's creation date: February 23, 2020
    purpose of the routine: This function is to enqueue messages into the conversation queue (so that something may be dequeued in the
                            Update function)
    a list of the calling arguments: index, an integer for starting ID of messages to be fetched
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void fetchMessages(int index) 
    {
        for (int i = 0; i < replyArea.transform.childCount; i++)
        {
        // and deletes/destroys them
            Destroy (replyArea.transform.GetChild(i).gameObject);
        }
        placeholder = story["phone"][index];
        while (placeholder.nextType == "phone") 
        {
            conversation.Enqueue(placeholder.text.Replace("[Player]", SharedVariables.username));
            placeholder = story[placeholder.nextType][placeholder.nextNumber];
        }
        operatorIsTalking = true;
        conversation.Enqueue(placeholder.text.Replace("[Player]", SharedVariables.username));
        if (placeholder.nextType == "stop") 
        {
            // Debug.Log("tapos na minigame");
            conversationEnd = true;
            // Debug.Log(conversationEnd);
            // Debug.Log(conversation.Count);
            // operatorIsTalking = false;
        }
        
        // else {
        //     operatorIsTalking = true;
        // }
    }
    /*
    method name: letPlayerReply
    routine's creation date: February 23, 2020
    purpose of the routine: This function instantiates the buttons the players can click to reply to the "operator".
                            The text on the buttons will be taken from the dictionary.
                            When the player clicks a button, fetchMessages is called with the ID of the message that
                            follows the choice selected.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void letPlayerReply() 
    {
        // Debug.Log("instantiating options");
        // Debug.Log(placeholder.text);
        // Debug.Log(placeholder.choices.Count);
        foreach (int index in placeholder.choices) 
        {
            DTypes option = story["phoneChoice"][index];
            GameObject button = Instantiate(choicesButton, replyArea.transform);
            Button bt = button.GetComponent<Button>();

            Text text = button.transform.GetChild(0).gameObject.GetComponent<Text>(); //get component text
            option.text = option.text.Replace("[Player]", SharedVariables.username);
            text.text = option.text;

            // sets the required onClick script
            bt.onClick.AddListener(delegate { conversation.Enqueue(option.text); callTimer = 3.0f; fetchMessages(option.nextNumber); });
        }
    }
    /*
    method name: endCall
    routine's creation date: February 23, 2020
    purpose of the routine: This function triggers the "call ended" animation, the call button turns back to green,
                            the background turns transparent and inactive again, the dialed numbers turn black again. 
                            If the dialed number is 911, the function also sets the appropriate values to minigameSuccess and
                            minigameEnd
    a list of the calling arguments: index, an integer for starting ID of messages to be fetched
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void endCall() 
    {
        dialing.text = "Call Ended";
        anim3.SetTrigger("End");
        anim2.SetTrigger("End");
        anim.SetTrigger("End");
        animationStart = false;      
        timer = 0.0f;
        setBackgroundToFalse = true; 
        for (int i = 0; i < replyArea.transform.childCount; i++)
        {
        // deletes and destroys the choices in case they are still existing before ending call.
            Destroy (replyArea.transform.GetChild(i).gameObject);
        }
        if (Phone.dialedNumber == "911" && minigameStart) 
        {
            if (placeholder.nextType == "stop")
            {
                if (placeholder.nextNumber == 0) 
                {
                    minigameDone = true;
                    minigameSuccess = true;
                    // minigameStart = false;
                }
                else 
                {
                    minigameDone = true;
                    minigameSuccess = false;
                    // minigameStart = false;
                }
            }
            else if (placeholder.nextType != "stop") {
                minigameDone = true;
                minigameSuccess = false;
                // minigameStart = false;
            }
        }
    }

}
