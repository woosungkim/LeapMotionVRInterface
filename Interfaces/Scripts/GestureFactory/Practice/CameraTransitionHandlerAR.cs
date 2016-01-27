using UnityEngine;
using System.Collections;

public class CameraTransitionHandlerAR : Swipe_Gesture {

    Switcher switcher;

    public override void DoAction()
    {
        print("check1");
        switcher = Switcher.GetInstance();
        switcher.switchCameraToAR();
    }
}
