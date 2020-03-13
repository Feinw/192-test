/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Filbert Wee

Code History:
1. Filbert Wee
   Change Date: February 1, 2020
   Change Description: Added parsing support for test file  
2. Filbert Wee
   Change Date: February 8, 2020
   Change Description: Added saving and loading to file .
                        Added multi-line diary page


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
    // file text is read from
    public TextAsset diaryPages;

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
        //lines = new list<string>();
        //pages = new list<diarypage>();
        //// hard code --- as the file parts will be different per os used.
        //string path = application.datapath + "/" + "messages assets /storm surge and flood script - diary.tsv";
        //// variable to be used in temporarily storing each line of the .tsv file at a time
        //string tmp;
        //// variable to actually read the .tsv file as a whole
        //streamreader tr = new streamreader(path);
        //// to remove the header line in the beginning
        //tr.readline();
        //while ((tmp = tr.readline()) != null)
        //{
        //    //remove blank lines
        //    if (!tmp.contains("\t\t"))
        //    {
        //        lines.add(tmp);
        //    }
        //}
        //tr.close();
        
        // characters reprsented as blank like
        string[] splits = new string[] { "\r", "\n" };
        // grab text from file
        string[] split = diaryPages.text.Split(splits, StringSplitOptions.RemoveEmptyEntries);
        // convert to list instead of array
        lines = new List<string>(split);
        // remove header line
        lines.RemoveAt(0);

        // load which lines are unlocked
        List<bool> save = SaveManager.LoadDiary(lines.Count);
        int i = 0;
        // parse into diary page from text and save
        foreach (string line in lines)
        {
            string[] parsedline = line.Trim().Split('\t');
            DiaryPage newPage = new DiaryPage
            {
                number = int.Parse(parsedline[0]),
                title = parsedline[1],
                text = parsedline[2].Replace("NEWLINE", "\n"),
                unlocked = save[i++]
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
        //string folderPath = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ? Application.persistentDataPath : Application.dataPath) + "/Messaxges Assets/";
        //string path = folderPath + "Storm Surge and Flood Script - Diary.txt";
        //// allow writing to file
        //List<string> text = new List<string>();
        ////using (var writer = new StreamWriter(path))
        ////{
        ////    writer.WriteLine("#\tTitle\ttext\tunlocked?");
        ////}
        //TextAsset file = Resources.Load<TextAsset>("Storm Surge and Flood Script - Diary");
        //while (file == null)
        //{
        //    file = Resources.Load<TextAsset>("Storm Surge and Flood Script - Diary");
        //    Debug.Log("why");
        //}
        //if (file != null)
        //{
        //    using (StreamWriter writer = new StreamWriter(new MemoryStream(file.bytes)))
        //    {
        //        writer.WriteLine("#\tTitle\ttext\tunlocked?");
        //        foreach (DiaryPage page in pages)
        //        {
        //            string[] line = new string[4] { page.number.ToString(), page.title, page.text.Replace("\n", "NEWLINE"), page.unlocked.ToString() };
        //            writer.WriteLine(string.Join("\t", line));
        //        }
        //    }
        //}
        //text.Add("#\tTitle\ttext\tunlocked?");
        //foreach(DiaryPage page in pages)
        //{
        //    string[] line = new string[4] { page.number.ToString(), page.title, page.text.Replace("\n", "NEWLINE"), page.unlocked.ToString() };
        //    text.Add(string.Join("\t", line));
        //}
        //File.WriteAllText(path, string.Join("\n",text));

        // sends pages to save file
        List<bool> x = new List<bool>();
        foreach(DiaryPage page in pages)
        {
            x.Add(page.unlocked);
        }
        SaveManager.SaveDiary(x);
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
