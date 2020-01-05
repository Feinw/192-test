using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour {

    [SerializeField]
    public List<Message> messageList = new List<Message>();

    public GameObject chatPanel, textObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            sendToChat("test");
        }
	}

    public void sendToChat(string text)
    {
        Message newMessage = new Message();
        newMessage.text = text;
        
        GameObject newBubble = Instantiate(textObject, chatPanel.transform ); 
        GameObject newText = newBubble.transform.GetChild(0).gameObject;
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;

        messageList.Add(newMessage);
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}