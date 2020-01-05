using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public Text time;

	// Use this for initialization
	void Start () {
        time.text = System.DateTime.Now.ToString("hh:mm tt");
    }
	
	// Update is called once per frame
	void Update ()
    {
        time.text = System.DateTime.Now.ToString("hh:mm tt");
    }
}
