using UnityEngine;
using System.Collections;
using Leap;

public class MyGesture3 : KeyTap_Gesture {

   

	// Use this for initialization
	void Start () {
    
        SetConfig();
	}

    protected override void DoAction()
    {
        base.DoAction();
    }	
}
