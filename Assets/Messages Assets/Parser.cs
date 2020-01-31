/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Filbert Wee

Code History:
1. Filbert Wee
   Change Date: January 22, 2020
   Change Description: Added parsing support for intro and choices "events."
2. Filbert Wee
   Change Date: January 23, 2020
   Change Description: Added more parsing support for event1, evac, postdisaster "events." Changed file type of story to .tsv
3. Filbert Wee
   Change Date: January 28, 2020
   Change Description: Added parsing for the remaining events. Compiled "all" "event" matrices into one dictionary. 

File Creation
Date: January 22, 2019
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Parser : MonoBehaviour
{
    // an list containing the lines read from the .tsv file
    public List<string> messages;                         
    // Dictionary named story to contain the objects after parsing
    private Dictionary<string, List<DTypes>> story;       
    // GameObject that contains the script the dictionary will be passed to
    public GameObject chatManager;                        

    /*
    method name: Start
    routine's creation date: January 22, 2020
    purpose of the routine: Start is called before the first frame update. Usually used for initializing variables.
                            This routine reads the .tsv file and converts each line into objects and append them into
                            a dictionary. The lines in the file are categorized into types. 
                            The types are the keys in the dictionary and the lines in the file (minus the type) are the
                            values.
                            After the dictionary has been filled, the dictionary is passed to ChatManager.cs 
    a list of the calling arguments: N/A
    a list of required files and/or database tables: Storm Surge and Flood Script - Sheet1.tsv
    and return value: N/A
    */
    void Start()
    {
        initializeDictionary();
        // HARD CODE --- as the file parts will be different per OS used.
        string path = Application.dataPath + "/" + "Messages Assets /Storm Surge and Flood Script - Sheet1.tsv";
        // variable to be used in temporarily storing each line of the .tsv file at a time
        string tmp;
        // variable to actually read the .tsv file as a whole
        StreamReader tr = new StreamReader(path);
        // to remove the header line in the beginning
        tr.ReadLine();
        while ((tmp = tr.ReadLine()) != null)
        {
            //remove blank lines
            if (!tmp.Contains("\t\t\t"))
            {
                messages.Add(tmp);          
            }
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
                choices = new List<int>()
            };
            if (line[5].Contains(","))
            {
                foreach (string element in line[5].Split(","[0]))
                {
                    nextLine.choices.Add(int.Parse(element));
                }
            }
            else if (string.Equals(nextLine.nextType, "choice"))
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
        if (chatManager == null) {
            Debug.Log("GameObject for chatManager is null.");
        }
        else {
            ChatManager cm = (ChatManager)chatManager.GetComponent<ChatManager>();
            cm.story = story;
        }
    }   
    
    /*
    method name: initializeDictionary
    routine's creation date: January 28, 2020
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
        string path = Application.dataPath + "/" + "Messages Assets /Storm Surge and Flood Script - Sheet2.tsv";
        // variable to be used in temporarily storing each line of the .tsv file at a time
        string tmp;
        // variable to actually read the .tsv file as a whole
        StreamReader types = new StreamReader(path);
        // to remove the header line in the beginning
        types.ReadLine();       
        while ((tmp = types.ReadLine()) != null)
        {
            //remove blank lines
            if (!tmp.Contains("\t"))
            {
                // initializes types of "events" dictionary
                story.Add(tmp, new List<DTypes>());     
            }
        }
    }
}

[System.Serializable]
// a class for the objects that will be stored in the value arrays of the dictionary
public class DTypes
{
    // identification of line
    public int number;         
    // name of "user" that sends the text                 
    public string name;     
    // content of the text                    
    public string text;            
    // type of next text in story            
    public string nextType;            
    // id of the next line of text in the story         
    public int nextNumber;                      
    // id of choices to be chosen by the player, if allowed to choose
    public List<int> choices;                   
}
