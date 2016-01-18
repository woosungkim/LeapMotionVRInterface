using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Leap;

public class MyGesture2 : Swipe_Gesture {

   
    Switcher switcher = null;

	// Use this for initialization
	void Start () {
        switcher = Switcher.GetInstance();
     
        SetConfig();
        SetGestureCondition('z', 1, 1);
	}
	
	// Update is called once per frame
	

    protected override void DoAction()
    {
        switcher.switchCameraToAR();
        /*
        GameObject VR = GameObject.Find("VRGesture");
        VR.active = false;
        VR.active = true;*/
    }
    
}
