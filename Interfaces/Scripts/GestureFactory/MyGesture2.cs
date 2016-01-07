using UnityEngine;
using System.Collections;

public class MyGesture2 : Swipe_Gesture {

    public bool result;

	// Use this for initialization
	void Start () {
        result = false;
        SetConfig();
	}
	
	// Update is called once per frame
	void Update () {
        result = CheckGesture();
        if(result == true)
        {
            print("Swipe Gesture");
        }

        UnCheck();
	}
}
