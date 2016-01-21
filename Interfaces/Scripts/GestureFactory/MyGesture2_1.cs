using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Leap;
using System.Threading;

public class MyGesture2_1 : Swipe_Gesture {

    Switcher switcher = null;

    // Update is called once per frame
  
    
    public override void DoAction()
    {
        switcher.switchCameraToVR();
      
    }
     
}
