/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Nephia Dalisay, Gene Tan, Filbert Wee

Code History:
1.  Filbert Wee
    Change Date: Mar 8, 2020
    Change Description: Save and Load for username, diary, and minigame
1.  Filbert Wee
    Change Date: Mar 10, 2020
    Change Description: Save and Load for messages

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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    /*
    method name: SaveDiary
    routine's creation date: March 8, 2020
    purpose of the routine: This function saves which diary pages are unlocked in binary formatting.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public static void SaveDiary(List<bool> pages)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "-diary.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        bool[] x = pages.ToArray();
        formatter.Serialize(stream, x);
        stream.Close();
    }
    /*
    method name: LoadDiary
    routine's creation date: March 8, 2020
    purpose of the routine: This function grabs which diary pages are unlocked from file.
                            if the file does not exist, creates new file
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public static List<bool> LoadDiary(int size)
    {
        string path = Application.persistentDataPath + "-diary.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            bool[] x = formatter.Deserialize(stream) as bool[];
            List<bool> pages = new List<bool>();
            foreach (bool y in x)
            {
                pages.Add(y);
            }

            stream.Close();
            return pages;
        }
        else
        {
            bool[] x = new bool[size];
            List<bool> pages = new List<bool>();
            foreach(bool y in x)
            {
                pages.Add(y);
            }
            SaveDiary(pages);
            return pages;
        }
    }
    /*
    method name: SaveUsername
    routine's creation date: March 8, 2020
    purpose of the routine: This function saves username in binary formatting.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public static void SaveUsername(string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "-user.name";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(stream, name);
        stream.Close();
    }
    /*
    method name: LoadUsername
    routine's creation date: March 8, 2020
    purpose of the routine: This function grabs username from file.
                            if the file does not exist, creates new file
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public static string LoadUsername()
    {
        string path = Application.persistentDataPath + "-user.name";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string x = formatter.Deserialize(stream) as string;
            stream.Close();
            return x;
        }
        else
        {
            SaveUsername("");
            return "";
        }
    }
    /*
    method name: SaveMinigame
    routine's creation date: March 8, 2020
    purpose of the routine: This function saves minigame progress in binary formatting.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public static void SaveMinigames(List<bool> progress)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "-mini.game";
        FileStream stream = new FileStream(path, FileMode.Create);

        bool[] x = progress.ToArray();
        formatter.Serialize(stream, x);
        stream.Close();
    }
    /*
    method name: LoadMinigame
    routine's creation date: March 8, 2020
    purpose of the routine: This function grabs minigame progress from file.
                            if the file does not exist, creates new file
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public static List<bool> LoadMinigames()
    {
        string path = Application.persistentDataPath + "-mini.game";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            bool[] x = formatter.Deserialize(stream) as bool[];
            List<bool> progress = new List<bool>();
            foreach (bool y in x)
            {
                progress.Add(y);
            }

            stream.Close();
            return progress;
        }
        else
        {
            bool[] x = new bool[5];
            // this is equivalent to SearchStart.disabled = true;
            x[0] = true;
            List<bool> progress = new List<bool>();
            foreach (bool y in x)
            {
                progress.Add(y);
            }
            SaveMinigames(progress);
            return progress;
        }
    }
    /*
    method name: SaveMessages
    routine's creation date: March 10, 2020
    purpose of the routine: This function saves message progress in binary formatting in 2 files.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public static void SaveMessages(List<SavedMessage> sm)
    {
        string path1 = Application.persistentDataPath + "-t.save";
        string path2 = Application.persistentDataPath + "-n.save";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream1 = new FileStream(path1, FileMode.Create);
        FileStream stream2 = new FileStream(path2, FileMode.Create);

        List<string> types = new List<string>();
        List<int> numbers = new List<int>();
        foreach (SavedMessage s in sm)
        {
            types.Add(s.type);
            numbers.Add(s.number);
        }
        string[] x = types.ToArray();
        int[] y = numbers.ToArray();
        formatter.Serialize(stream1, x);
        formatter.Serialize(stream2, y);

        stream1.Close();
        stream2.Close();
    }
    /*
    method name: LoadMessages
    routine's creation date: March 10, 2020
    purpose of the routine: This function grabs message progress from file.
                            if the file does not exist, creates new file
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public static List<SavedMessage> LoadMessages()
    {
        string path1 = Application.persistentDataPath + "-t.save";
        string path2 = Application.persistentDataPath + "-n.save";
        if (File.Exists(path1) && File.Exists(path2))
        {
            List<SavedMessage> messages = new List<SavedMessage>();

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream1 = new FileStream(path1, FileMode.Open);
            FileStream stream2 = new FileStream(path2, FileMode.Open);

            string[] x = formatter.Deserialize(stream1) as string[];
            int[] y = formatter.Deserialize(stream2) as int[];

            for (int i = 0; i < y.Length; i++)
            {
                SavedMessage newSM = new SavedMessage
                {
                    type = x[i],
                    number = y[i]
                };
                messages.Add(newSM);
            }

            stream1.Close();
            stream2.Close();

            return messages;
        }
        else
        {
            List<SavedMessage> messages = new List<SavedMessage>();
            SavedMessage firstMessage = new SavedMessage
            {
                type = "intro",
                number = 0
            };
            messages.Add(firstMessage);
            SaveMessages(messages);
            return messages;
        }
    }
}
