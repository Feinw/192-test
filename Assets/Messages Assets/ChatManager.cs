/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Nephia Dalisay, Gene Tan, Filbert Wee

Code History:
1. Filbert Wee
   Change Date: January 20, 2020
   Change Description: Added bare minimum for any type of message to the "chat."
2. Gene Tan
   Change Date: January 20, 2020
   Change Description: Added timer to sending messages to chat. Updated sending function to reflect UI changes
3. Filbert Wee
   Change Date: January 20, 2020
   Change Description: Updated sending function to reflect more UI changes  
4. Gene Tan
   Change Date: January 20, 2020
   Change Description: Added a queue for messages. Added function sending of test messages upon key press.
5. Gene Tan
   Change Date: January 22, 2020
   Change Description: Added function to read and store variables from Parser script
6. Nephia Dalisay
   Change Date: January 23, 2020
   Change Description: Added function to "send" player chosen text.
7. Filbert Wee
   Change Date: January 23, 2020
   Change Description: Added additional variables to chat manager to reflect changes made to parser. Added function to send message based on message type. Moved passing/sharing of variables to parser.cs. 
8. Gene Tan
   Change Date: January 24, 2020
   Change Description: Added ability to display name of "user" to UI.
9. Nephia Dalisay
   Change Date: January 26, 2020
   Change Description: Added function read from the parser the choices the player is allowed to choose from.
10.Filbert Wee
   Change Date: January 26, 2020
   Change Description: Added function create choices buttons based from the parser.
11.Gene Tan
   Change Date: January 27, 2020
   Change Description: Fixed bug with Thread.sleep
12.Filbert Wee
   Change Date: Jan 28, 2020
   Change Description: Updated variable to reflect parser changes. Rearranged functions for cleaner code.
13.Gene Tan
   Change Date: Jan 31, 2020
   Change Description: Added test code for a dropdown notification with timing.
14.Gene Tan
   Change Date: Feb 1, 2020
   Change Description: Removed test code for a dropdown notification to move it to a different file.
15.Gene Tan
   Change Date: Feb 2, 2020
   Change Description: Changed some of the code in sendToChat() to match the changes made in the bot bubble & player bubble assets
16.Gene Tan
   Change Date: Feb 5, 2020
   Change Description: Added code to unlock diary pages after choices are made
16.Filbert Wee
   Change Date: Feb 11, 2020
   Change Description: Added hangman game to the story

File Creation
Date: January 20, 2019
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChatManager : MonoBehaviour
{
    // allows variables to be visible in the unity ui
    [SerializeField]
    // list containing all the messages "sent"
    public List<Message> messageList = new List<Message>();
    // queue used in determining which messages to be "sent" next
    Queue<Message> messagesQueue = new Queue<Message>();

    // time to wait to send a message from the queue (in seconds)
    public float countdown;
    // timer variable
    private float timer;
    
    // list of available choices the player is allowed to choose next.
    public List<DTypes> options;

    // variable containing all the parsed messages in the story
    public Dictionary<string, List<DTypes>> story;

    // variable containing all the parsed diary pages
    public List<DiaryPage> diary;
    // GameObject that contains the diary parser
    public GameObject diaryParser;

    // gameobjects (where to post the messages, what the "friends', your, and system messages look like") to be accessed by the script
    public GameObject chatPanel, botReplyPrefab, playerReplyPrefab, systemNotifPrefab;
    // variable to check whether the name of a "sender" should be displayed
    private string oldName = "";

    
    //Choosing the next type of messages
    public string type;
    //Choosing the next id of messages given the type
    public int num;

    // variable to check in which state the game(messages) is currently in
    public string cases;
    
    // GameObject to be the location in which choices will be displayed
    public GameObject choosingReplyArea;
    // what the choices will look like
    public GameObject choicesButtonPrefab;
    // GameObject that contains the button to display the area where choices will be displayed
    public GameObject replyButton;
    // variable to store the function from the script that controls the movement of the area "choosingReplyArea".
    ReplyBarScript rs;
    

    public bool test = false;

    /*
    method name: Start
    routine's creation date: January 20, 2020
    purpose of the routine: Start is called before the first frame update. Usually used for initializing variables.
                            This routine sets the type of message to be an intro(ductory) message of the 0th index in its list.
                            This routine will also set how long a timer should countdown, in which state the game starts with, and sets the "rs" to read from the correct script
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start()
    {
        type = "intro";
        num = 0;
        Debug.Log("nice");
        timer = countdown;
        
        cases = "retrieving text";
        rs = replyButton.GetComponent<ReplyBarScript>();

    }

    /*
    method name: Update
    routine's creation date: January 20, 2020
    purpose of the routine: Update is called once per frame. It is a forever loop.
                            This routine decides each frame if the player is allowed to choose a reply or not.
                            This also sends "messages" based on the message queue and the timer variable.
                            This will determine at which state the game is always in.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Update()
    {        
        // ensures that grabbing the diary contents is done after the parser does its job
        if (diary.Count == 0)
        {
            diary = diaryParser.GetComponent<DiaryParser>().pages;
        }
        // if there are no more messages to be sent, allow the player to make a reply
        if (messagesQueue.Count == 0)
        {
            // call a function from a different script
            ReplyBarScript.canReply = true;
        }
        // otherwise, send messages after a certain interval
        else
        {
            if (timer >= countdown)
            {
                // send messages based on FIFO queue
                sendToChat(messagesQueue.Dequeue());
                // reset timer
                timer = timer - countdown;
            }
            timer += Time.deltaTime;
        }

        // chooses the current state of the game
        switch (cases)
        {
            // chooses what message is next from the dictionary
            case "retrieving text":
                chooseMessage();
                ReplyBarScript.canReply = false;
                break;
            // chooses and create choices for player interaction
            case "retrieving reply":
                setOptionButtons();             
                break;
            // a wait for reply inside the update loop(forever loop)
            case "choosing reply":
                break;
            case "playing minigame":
                waitForMinigameEnd();
                ReplyBarScript.canReply = false;
                break;
            // tells the game that an ending is reached
            case "ending":           
                ReplyBarScript.canReply = false;
                break;
        }
    }

    /*
    method name: chooseMessage
    routine's creation date: January 23, 2020
    purpose of the routine: This function decides which message to send next based on the current message.
                            This will may also determine which state the game will be in next depending on certain of messages.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void chooseMessage()
    {
        // creates new message bases on the dictionary, type, num. Basically, based on what was sent last
        Message newMessage = new Message
        {
            name = story[type][num].name,
            text = story[type][num].text,
            script = story[type][num],
        };
        // add the new message to the message queue
        messagesQueue.Enqueue(newMessage);
        // creates a temporary variable to store the current type of message as it will be overwritten
        string currentType = type;
        // update the type of message to what next is needed
        type = story[currentType][num].nextType;

        // determines if next message allows the player to choose from choices or keep sending story messages
        // if the player need to make a decision
        if (type == "choice")
        {
            // reset options list
            options = new List<DTypes>();
            // add into options new choices based on what was sent in the story
            foreach (int choicenumber in story[currentType][num].choices)
            {
                options.Add(story["choice"][choicenumber]);
            }
            // sets state to waiting to create choices buttons
            cases = "retrieving reply";
        }
        // if the player has reached an ending
        else if (type == "stop")
        {
            cases = "ending";
        }
        else if (type == "minigame")
        {
            cases = "playing minigame";
            num = story[currentType][num].nextNumber;
        }
        // otherwise, procede to next line in the story
        else
        {
            num = story[currentType][num].nextNumber;
        }
    }

    /*
    method name: waitForMinigameEnd
    routine's creation date: January 26, 2020
    purpose of the routine: This routine just checks other scripts if a specific minigame is finished
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void waitForMinigameEnd()
    {
        // if the hangman minigame
        if (num == 0)
        {
            // start the game
            if (SearchStart.disabled)
            {
                SearchStart.disabled = false;
            }
            // checks if finished
            if (HangManGame.finished)
            {
                type = "minigame-answer";
                // if lost
                if (HangManGame.failed)
                {
                    options.Add(story[type][1]);
                }
                // else won
                else
                {
                    options.Add(story[type][0]);
                }
                // proceed with story
                cases = "retrieving reply";
            }
        }
    }
    
    /*
    method name: setOptionButtons
    routine's creation date: January 26, 2020
    purpose of the routine: Creates the actual physical buttons for each of the choices, and passes the choice chosen to either 
                            setOptionButton() if the story path is available, or setBlockedButton() if the path is blocked.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void setOptionButtons()
    {
        foreach (DTypes option in options)
        {
            // creates choices button inside the game
            GameObject button = Instantiate(choicesButtonPrefab, choosingReplyArea.transform);
            // collect the button part of the component above
            Button bt = button.GetComponent<Button>();
            // collect the text part of the button above
            Text text = button.transform.GetChild(0).gameObject.GetComponent<Text>(); //get component text
            text.text = option.text;

            // sets the required onClick script
            if (option.nextType != "block")
            {
                bt.onClick.AddListener(delegate { setOptionButton(option); });
            }
            else
            {
                bt.onClick.AddListener(delegate { setBlockedButton(option); });
            }
        }
        cases = "choosing reply";
    }

    /*
    method name: setBlockedButton
    routine's creation date: January 26, 2020
    purpose of the routine: Only displays the text of the chosen (blocked) choice on the console. 
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void setBlockedButton(DTypes option)
    {
        Debug.Log(story["block"][option.nextNumber].text);
    }

    /*
    method name: setOptionButton
    routine's creation date: January 26, 2020
    purpose of the routine: This routine sets what message to send based on the chosen,usable button upon clicked.
                            This also determines that the state of the game is to receive that just sent message.
                            After which, this routine will clear the buttons in the choices area by calling the clearButtons routine.
    a list of the calling arguments: DTypes option, DTypes is a custom class used to store the already parsed messages from the dictionary
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void setOptionButton(DTypes option)
    {
        // checks if the choice leads to a minigame
        if (option.nextType == "minigame")
        {
            cases = "playing minigame";
            num = option.nextNumber;
        }
        else
        {
            // creates new message from the choice chooses
            Message newMessage = new Message
            {
                name = option.name,
                text = option.text,
                script = option
            };
            // adds new message to the message queue
            messagesQueue.Enqueue(newMessage);
            // reset the timer
            timer = countdown;
            // chooses the next message to be sent
            type = option.nextType;
            num = option.nextNumber;
            // sets the next state of the game
            cases = "retrieving text";
            // unlock a diary page
            if (option.unlocks != -1)
            {
                unlockDiaryPage(option.unlocks);
            }
            // clear choices buttons currently shown.
        }
        clearButtons();
    }

    /*
    method name: clearButtons
    routine's creation date: January 26, 2020
    purpose of the routine: This routine clear button choices that have already been passed.
                            This routine will also hide the choices area upon called and temporarily disable the reply button.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void clearButtons()
    {
        // calls ReplyBarScript.cs to hide the choices area
        rs.MoveStuff();
        // temporarily disables the reply button
        ReplyBarScript.canReply = false;
        // gets all choices currently shown
        for (int i = 0; i < choosingReplyArea.transform.childCount; i++)
        {
            // and deletes/destroys them
            Destroy (choosingReplyArea.transform.GetChild(i).gameObject);
        }
        options = new List<DTypes>();
    }

    /*
    method name: unlockDiaryPage
    routine's creation date: Febuary 1, 2020
    purpose of the routine: This routine sets a diary page to unlocked.
                            This will also edit the text file outside the game so that it will keep pages unlocked by the player.
    a list of the calling arguments: int pageNumber
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void unlockDiaryPage(int pageNumber)
    {
        // unlocks inside the game
        if ((diary[pageNumber].unlocked) == false) {
            diary[pageNumber].unlocked = true;
            // unlocks outside the game
            DiaryParser.unlockPage(diary);
            NotifText.text = "A diary page has been unlocked";
            NotifText.title = "My Diary";
            NotifText.dropdown = true;
            // NotificationDropdown.text = "A diary page has been unlocked.";
        }

    }

    /*
    method name: sendToChat
    routine's creation date: January 20, 2020
    purpose of the routine: This routine is responsible for the messages we see on the game. Whenever this function is called,
                            it instantiates a player bubble object, a "bot" bubble object, or a system message object and sets the object's
                            text to the text of the argument (newMessage). The newMessage is then added into an array that contains all
                            sent messages.
    a list of the calling arguments: newMessage
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void sendToChat(Message newMessage)
    {
        GameObject newSpace;
        if (newMessage.name != "system")
        {
            if (newMessage.name == "Player")
            {
                // instantiate player bubble prefab under the chatPanel gameobject
                newSpace = Instantiate(playerReplyPrefab, chatPanel.transform);
                // set the text of the object
                GameObject newText = newSpace.transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).gameObject;
                newMessage.textObject = newText.GetComponent<Text>();
                newMessage.textObject.text = newMessage.text;
                // code for the name appearing on top of the bubbles
                if (oldName == newMessage.name)
                {
                    Text name = newSpace.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
                    name.fontSize = 1;
                    name.text = "";
                    name = newSpace.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
                    name.fontSize = 1;
                    name.text = "";
                }
                else
                {
                    Text name = newSpace.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
                    name.text = newMessage.name;
                    name.fontSize = 30;
                    name = newSpace.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
                    name.text = "";
                    name.fontSize = 20;
                }
            }
            else
            {
                // instantiate bot bubble prefab under the chatPanel gameobject
                newSpace = Instantiate(botReplyPrefab, chatPanel.transform);
                // set the text of the object
                GameObject newText = newSpace.transform.GetChild(2).transform.GetChild(0).gameObject;
                newMessage.textObject = newText.GetComponent<Text>();
                newMessage.textObject.text = newMessage.text;
                // code for the name appearing on top of the bubbles
                if (oldName == newMessage.name)
                {
                    Text name = newSpace.transform.GetChild(0).GetComponent<Text>();
                    name.fontSize = 1;
                    name.text = "";
                    name = newSpace.transform.GetChild(1).GetComponent<Text>();
                    name.fontSize = 1;
                    name.text = "";
                }
                else
                {
                    Text name = newSpace.transform.GetChild(1).GetComponent<Text>();
                    name.text = newMessage.name;
                    name.fontSize = 30;
                    name = newSpace.transform.GetChild(0).GetComponent<Text>();
                    name.text = "";
                    name.fontSize = 20;
                }
            }
            

            oldName = newMessage.name;
        }
        else
        {
            // instantiate system prefab under the chatPanel gameobject
            newSpace = Instantiate(systemNotifPrefab, chatPanel.transform);
            // set the text of the object
            GameObject newText = newSpace.transform.GetChild(0).transform.GetChild(0).gameObject;
            newMessage.textObject = newText.GetComponent<Text>();
            newMessage.textObject.text = newMessage.text;
            // "reset" the name
            oldName = "";
        }
        messageList.Add(newMessage);

    }

    
}

[System.Serializable]
// a class for the messages that will be displayed in the UI
public class Message
{
    // name of the "user"
    public string name;                
    // text that will be displayed 
    public string text;          
    // text object that will be shown in the UI      
    public Text textObject;       
    // line taken from the parser     
    public DTypes script;               
}
