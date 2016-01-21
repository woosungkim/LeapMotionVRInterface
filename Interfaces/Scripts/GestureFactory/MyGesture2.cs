using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Leap;

public class MyGesture2 : Swipe_Gesture {

   
    Switcher switcher = null;	
	// Update is called once per frame
	

    public override void DoAction()
    {
        switcher = Switcher.GetInstance();
        switcher.switchCameraToAR();
        /*
        GameObject VR = GameObject.Find("VRGesture");
        VR.active = false;
        VR.active = true;*/
    }
    
}
