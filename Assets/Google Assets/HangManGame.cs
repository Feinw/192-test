/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Filbert Wee

Code History:
1. Filbert Wee
   Change Date: February 7, 2020
   Change Description: Added the important parts of the game
2. Filbert Wee
   Change Date: February 11, 2020
   Change Description: Integrated the game into the messaging app

File Creation
Date: Feb 7, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangManGame : MonoBehaviour
{
    // checker if the game is finished
    public static bool finished = false;
    // checker if the player loses
    public static bool failed = false;

    // the tries left in hangman
    public int tries;
    // the text displaying the tries left
    public Text triesText;

    // the text displaying game text
    public Text text;
    // the game text as array for string manipulation
    private char[] gameText;
    // string to keep track of correct answer
    private string answer;
    // correct places for the letters
    private Dictionary<char, List<int>> placements;

    // area containing the tries left
    public GameObject triesArea;
    // area containing the correct answer if the player loses
    public GameObject correctText;
    // area for the 'keyboard'
    public GameObject keyboard;
    // image for hangman
    public GameObject[] parts;

    /*
    method name: Start
    routine's creation date: February 7, 2020
    purpose of the routine: Start is called before the first frame update. Usually used for initializing variables.
                            This routine sets the main game text depending if the player has finished the game yet or not
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start()
    {
        answer = text.text;
        gameText = new char[answer.Length + 1];
        if (!finished)
        {
            initializeDictionary();
            for(int i = 0; i < answer.Length; i++)
            {
                if (answer[i]==' ' || answer[i] == '.')
                {
                    gameText[i] = answer[i];
                }
                else
                {
                    gameText[i] = '_';
                    placements[char.ToLower(answer[i])].Add(i);
                }
            }
            text.text = new string(gameText);
        }
        else
        {
            for (int i = 0; i < answer.Length; i++)
            {
                gameText[i] = answer[i];
            }
            parts[5].SetActive(true);
            tries = 0;
            triesText.text = "Tries Left: 0";
            disableKeyboard();
        }
    }

    /*
    method name: click
    routine's creation date: February 7, 2020
    purpose of the routine: This routine plays the game depending on the 'key' press.
                            This routine also deletes the used key once done and updates the tries left if needed.
    a list of the calling arguments: GameObject button - a key in the made up keyboard
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void click(GameObject button)
    {
        // checks if game is already finished
        if (!finished)
        {
            // if the player is wrong
            if (placements[char.ToLower(button.name.ToCharArray()[0])].Count == 0)
            {
                tries--;
                triesText.text = "Tries Left: " + tries.ToString();
                parts[5 - tries].SetActive(true);
            }
            // unlocks all of the same letter
            else
            {
                foreach (int i in placements[char.ToLower(button.name.ToCharArray()[0])])
                {
                    gameText[i] = answer[i];
                }
            }
            // destroy button after
            Destroy(button);
            // update game text
            text.text = new string(gameText);
            
            // if no letters left or out of tries
            if (string.Compare(text.text, answer) == 0)
            {
                finished = true;
                failed = false;
                disableKeyboard();
            }
            else if (tries <= 0)
            {
                finished = true;
                failed = true;
                correctText.SetActive(true);
                disableKeyboard();
            }
        }
        
    }

    /*
    method name: initializeDictionary
    routine's creation date: February 7, 2020
    purpose of the routine: This routine initializes a dictionary to store all the locations if letters
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void initializeDictionary()
    {
        string alpha = "abcdefghijklmnopqrstuvwxyz";
        placements = new Dictionary<char, List<int>>();
        foreach(char a in alpha)
        {
            placements[a] = new List<int>();
        }
    }

    /*
    method name: disableKeyboard
    routine's creation date: February 7, 2020
    purpose of the routine: When the game finishes, this routine deletes all remaining unused keys and hides the keyboard.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void disableKeyboard()
    {
        GameObject top = keyboard.transform.GetChild(0).gameObject;
        for(int i = 0; i < top.transform.childCount; i++)
        {
            top.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
        GameObject middle = keyboard.transform.GetChild(1).gameObject;
        for (int i = 0; i < middle.transform.childCount; i++)
        {
            middle.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
        GameObject bottom = keyboard.transform.GetChild(2).gameObject;
        for (int i = 0; i < bottom.transform.childCount; i++)
        {
            bottom.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
        iTween.MoveBy(triesArea, iTween.Hash("y", -618, "default", 1.5f));
        iTween.MoveBy(keyboard, iTween.Hash("y", -618, "default", 1.5f));
    }
}
