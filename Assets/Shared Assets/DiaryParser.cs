/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Filbert Wee

Code History:
1. Filbert Wee
   Change Date: February 1, 2020
   Change Description: Added parsing support for test file  

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
using System.IO;
using System;

public class DiaryParser : MonoBehaviour
{
    // an list containing the lines read from the .tsv file
    public List<string> lines;
    // an list parsed diary pages
    public List<DiaryPage> pages;

    /*
    method name: Start
    routine's creation date: February 1, 2020
    purpose of the routine: Start is called before the first frame update. Usually used for initializing variables.
                            This routine reads the .tsv file and converts each line into objects and stored in a list. 
                            The types are the keys in the dictionary and the lines in the file (minus the type) are the
                            values.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: Storm Surge and Flood Script - Diary.tsv
    and return value: N/A
    */
    void Start()
    {
        lines = new List<string>();
        pages = new List<DiaryPage>();
        // HARD CODE --- as the file parts will be different per OS used.
        string path = Application.dataPath + "/" + "Messages Assets /Storm Surge and Flood Script - Diary.tsv";
        // variable to be used in temporarily storing each line of the .tsv file at a time
        string tmp;
        // variable to actually read the .tsv file as a whole
        StreamReader tr = new StreamReader(path);
        // to remove the header line in the beginning
        tr.ReadLine();
        while ((tmp = tr.ReadLine()) != null)
        {
            //remove blank lines
            if (!tmp.Contains("\t\t"))
            {
                lines.Add(tmp);          
            }
        }
        tr.Close();
        foreach (string line in lines)
        {
            string[] parsedline = line.Trim().Split("\t"[0]);
            DiaryPage newPage = new DiaryPage
            {
                number = int.Parse(parsedline[0]),
                title = parsedline[1],
                text = parsedline[2],
                unlocked = Boolean.Parse(parsedline[3])
            };
            pages.Add(newPage);        
        }
    }

    /*
    method name: unlockPage
    routine's creation date: February 1, 2020
    purpose of the routine: This routine will edit the .tsv file storing the diary page contents.
                            It will overwrite all lines in the file.
                            
    a list of the calling arguments: List<DiaryPage> pages
    a list of required files and/or database tables: Storm Surge and Flood Script - Diary.tsv
    and return value: N/A
    */
    public static void unlockPage(List<DiaryPage> pages)
    {
        // HARD CODE --- as the file parts will be different per OS used.
        string path = Application.dataPath + "/" + "Messages Assets /Storm Surge and Flood Script - Diary.tsv";
        // allow writing to file
        using (var writer = new StreamWriter(path))
        {
            writer.WriteLine("#\tTitle\ttext\tunlocked?");
            foreach(DiaryPage page in pages)
            {
                string[] line = new string[4] { page.number.ToString(), page.title, page.text, page.unlocked.ToString() };
                writer.WriteLine(string.Join("\t", line));
            }
        }
    }
}

[System.Serializable]
// a class for the objects that will be stored in the value arrays of the dictionary
public class DiaryPage
{
    // identification of line
    public int number;         
    // Title of page                 
    public string title;     
    // content of the text                    
    public string text;
    // whether the player has already unlocked the page                    
    public bool unlocked;
}
