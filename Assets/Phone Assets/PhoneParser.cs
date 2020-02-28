/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Gene Tan

Code History:
1. Gene Tan
   Change Date: February 23, 2020
   Change Description: Code is heavily based off the other parser script. Certain variables and lines were changed for successful
                        parsing of the phone conversation text file.

File Creation
Date: February 23, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhoneParser : MonoBehaviour
{
    // an list containing the lines read from the .txt file
    public List<string> messages;                         
    // Dictionary named story to contain the objects after parsing
    public Dictionary<string, List<DTypes>> story;       
    // GameObject that contains the script the dictionary will be passed to
    public GameObject callButton;

    // to contain the phone conversation text file; this variable is initialized on the Unity UI
    public TextAsset phoneConversation;

    // to contain the phone conversation types text file; this variable is initialized on the Unity UI
    public TextAsset phoneConversationTypes;

    /*
    method name: Start
    routine's creation date: February 23, 2020
    purpose of the routine: Start is called before the first frame update. Usually used for initializing variables.
                            This routine reads the .txt file and converts each line into objects and append them into
                            a dictionary. The lines in the file are categorized into types. 
                            The types are the keys in the dictionary and the lines in the file (minus the type) are the
                            values.
                            After the dictionary has been filled, the dictionary is passed to AnimationTrigger.cs
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    void Start()
    {
        initializeDictionary();
        // HARD CODE --- as the file parts will be different per OS used.
        //string path = Application.dataPath + "/" + "Phone Assets /PhoneConversation.tsv";
        // variable to be used in temporarily storing each line of the .tsv file at a time
        //string tmp;
        // variable to actually read the .tsv file as a whole
        //StreamReader tr = new StreamReader(path);
        // to remove the header line in the beginning
        //tr.ReadLine();
        //while ((tmp = tr.ReadLine()) != null)
        //{
            //remove blank lines
            //if (!tmp.Contains("\t\t\t"))
            //{
                //messages.Add(tmp);          
            //}
        //}
        string[] splits = new string[] { "\r", "\n", "\t\t\t\t\t\t" };
        string[] words = phoneConversation.text.Split(splits, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < words.Length; i++)
        {
            messages.Add(words[i]);
        }
        foreach (string message in messages)
        {
            string[] line = message.Trim().Split("\t"[0]);
            DTypes nextLine = new DTypes
            {
                number = int.Parse(line[1]),
                name = line[2],
                text = line[3],
                nextType = line[4],
                nextNumber = 0,
                choices = new List<int>(),
                unlocks = int.Parse(line[6])
            };
            if (line[5].Contains(","))
            {
                foreach (string element in line[5].Split(","[0]))
                {
                    nextLine.choices.Add(int.Parse(element));
                }
            }
            else if (string.Equals(nextLine.nextType, "phoneChoice"))
            {
                nextLine.choices.Add(int.Parse(line[5]));
            }
            else
            {
                nextLine.nextNumber = int.Parse(line[5]);
            }

            story[line[0]].Add(nextLine);
        
        }

        //passing lists to chat manager
        if (callButton == null) {
            Debug.Log("GameObject for callButton is null.");
        }
        else {
            Debug.Log("done");
            AnimationTrigger phone = callButton.GetComponent<AnimationTrigger>();
            phone.story = story;
        }
    }   
    
    /*
    method name: initializeDictionary
    routine's creation date: February 23, 2020
    purpose of the routine: This routine initializes the dictionary by reading the types of events on the script.
                            It initializes the dictionary using the types as the keys and empty lists of objects
                            as the values.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: Storm Surge and Flood Script - Sheet2.tsv
    and return value: N/A
    */
    public void initializeDictionary()
    {
        story = new Dictionary<string, List<DTypes>>();
        // HARD CODE --- as the file parts will be different per OS used.
        //string path = Application.dataPath + "/" + "Phone Assets /PhoneConversation - Types.tsv";
        // variable to be used in temporarily storing each line of the .tsv file at a time
        //string tmp;
        // variable to actually read the .tsv file as a whole
        //StreamReader types = new StreamReader(path);
        // to remove the header line in the beginning
        //types.ReadLine();       
        //while ((tmp = types.ReadLine()) != null)
        //{
            //remove blank lines
            //if (!tmp.Contains("\t"))
            //{
                // initializes types of "events" dictionary
                //Debug.Log(tmp);
                //story.Add(tmp, new List<DTypes>());     
            //}
        //}
        string[] splits = new string[] { "\r", "\n", "\t" };
        string[] words = phoneConversationTypes.text.Split(splits, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < words.Length; i++)
        {
            story.Add(words[i], new List<DTypes>());
        }
    }
}

// [System.Serializable]
// a class for the objects that will be stored in the value arrays of the dictionary
// public class DTypes
// {
//     // identification of line
//     public int number;         
//     // name of "user" that sends the text                 
//     public string name;     
//     // content of the text                    
//     public string text;            
//     // type of next text in story            
//     public string nextType;            
//     // id of the next line of text in the story         
//     public int nextNumber;                      
//     // id of choices to be chosen by the player, if allowed to choose
//     public List<int> choices;
//     // id of the diary page if it unlocks one, otherwise set to -1         
//     public int unlocks;
// }
