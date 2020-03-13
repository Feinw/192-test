/*
“This is a course requirement for CS 192 Software Engineering II under the 
supervision of Asst. Prof. Ma. Rowena C. Solamo of the Department of Computer Science, 
College of Engineering, University of the Philippines, Diliman for the AY 2019-2020”.

Author/s: Nephia Dalisay

Code History:
1. Nephia Dalisay
   Change Date: January 20, 2020
   Change Description: Created the intial function that moves the reply bar up.
2. Nephia Dalisay
   Change Date: January 22, 2020
   Change Description: Added code that moves the reply bar down when clicked the second time.

File Creation
Date: January 20, 2020
Development Group: Nephia Dalisay, Gene Tan, Filbert Wee
Client Group: Prof. Ma. Rowena C. Solamo, students of CS 192, people interested in mobile games
Purpose of the software: The purpose of this project is to create a mobile application that aims to 
                         encourage disaster preparedness in a fun, unique, and memorable way.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplyBarScript : MonoBehaviour
{
    
    //a boolean variable that tells the current state of the reply bar
    bool down = true;
    //Game Object that will move up or down (Reply Bar)
    public GameObject bar;
    //Game Object that will shrink (Scroll View)
    public GameObject panel;
    //integer that determines how much the reply bar will move up and how much the panel/ scroll view will shrink
    public int movement;

    public static bool haschoice = false;

    //a boolean variable that tells the code whether the reply button must enabled or disabled
    public static bool canReply = false;
    
    /*
    method name: MoveStuff()
    routine's creation date: January 20, 2020
    purpose of the routine: This routine is what allows for the reply area to move up and make space for the reply/option objects
                            when a button is clicked.
                            As the reply bar moves up, the Scroll View object "shrinks".
                            This routine also allows the reply area to move back down to "hide" the reply area when the button
                            is clicked once again.
                            As the reply bar moves down, the Scroll View object "expands" to its original size.
    a list of the calling arguments: N/A
    a list of required files and/or database tables: N/A
    and return value: N/A
    */
    public void MoveStuff()
    {
        if (bar == null || panel == null) 
        {
            Debug.Log("GameObjects 'bar' or 'panel' is null.");
        }
        else 
        {
            if (canReply)
            {
                // Debug.Log("I CAN ERPLY");
                if (down)
                {
                    //will contain the position of the object (x,y)
                    Vector3 pos;
                    //moves the bar up
                    pos = bar.transform.position;
                    pos.y += (movement);
                    bar.transform.position = pos;
                    
                    //resizes panel

                    RectTransform rt = panel.GetComponent<RectTransform>();
                    // Vector2 from = new Vector2(rt.rect.width, rt.rect.height);
                    // Vector2 to = new Vector2(rt.rect.width, rt.rect.height - 90 - (movement/2));
                    rt.sizeDelta = new Vector2(rt.rect.width, rt.rect.height - 90 - (movement/2)); 
        
                    // panel.GetComponent<VerticalLayoutGroup>().padding.top = 313;
                    // rt.padding.top = 313;

                    Vector3 pos2 = panel.transform.position;
                    pos2.y += (movement/2);
                    panel.transform.position = pos2;
                    // iTween.ScaleTo(panel, iTween.Hash("scale", size, "time", 0.6f, "easeType", iTween.EaseType.easeInOutSine));
                    // iTween.ValueTo(rt.sizeDelta, iTween.Hash("from", from, "to", to, "time",0.6f,"onupdate","changeMotionBlur"));

                    // iTween.MoveTo(bar, iTween.Hash("position", pos, "time", 0.6f, "easeType", iTween.EaseType.easeInOutSine));
                    // iTween.MoveTo(panel, iTween.Hash("position", pos2, "time", 0.6f, "easeType", iTween.EaseType.easeInOutSine));

                    down = false;

                }
                else
                {
                    //will contain the position of the object (x,y)
                    Vector3 pos;
                    //move the bar down
                    pos = bar.transform.position;
                    pos.y -= movement;
                    bar.transform.position = pos;
                        //resize the panel
                    RectTransform rt = panel.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(rt.rect.width, rt.rect.height + 90 + (movement / 2));
                        // RectTransform rt = panel.GetComponent<RectTransform>();
                        // rt.sizeDelta = new Vector2(800, 832);

                    //will contain the position of the object (x,y)
                    Vector3 pos2 = panel.transform.position;
                    pos2.y -= (movement/2);
                    panel.transform.position = pos2;
                    //the reply bar is now down
                    

                    // iTween.MoveTo(bar, iTween.Hash("position", pos, "time", 0.6f, "easeType", iTween.EaseType.easeInOutSine));
                    // iTween.MoveTo(panel, iTween.Hash("position", pos2, "time", 0.6f, "easeType", iTween.EaseType.easeInOutSine));
                    down = true;
                }
            }
        }
       
    }

}
