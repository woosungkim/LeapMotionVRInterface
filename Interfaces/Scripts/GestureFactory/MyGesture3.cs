using UnityEngine;
using System.Collections;

public class MyGesture3 : KeyTap_Gesture {

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
            print("KeyTap Gesture");
        }
        UnCheck();
	}
}
