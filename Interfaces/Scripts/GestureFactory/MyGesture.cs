using UnityEngine;
using System.Collections;


public class MyGesture : Circle_Gesture {

    public bool result;
    public bool isclockwise;
	// Use this for initialization
	void Start () {
        result = false;
        SetConfig();
	}
	
	// Update is called once per frame
	void Update () {
        result = CheckGesture();
        if (result) isclockwise = IsClockWise(); 
        /*
        if(result == true)
        {
            print("Circle Gesture");
            if (IsClockWise())
            {
                print("시계방향");
            }
            else {
                print("반시계방향");
            }
        }
        */
        //UnCheck();
	}
}
