using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour {

    public Text friend;
    public string theName;

    // Use this for initialization
    void Start () {
        friend.text = theName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
