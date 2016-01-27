using UnityEngine;
using System.Collections;

public class CameraTransitionHandlerVR : Swipe_Gesture {

    Switcher switcher;

    public override void DoAction()
    {
        print("check2");
        switcher = Switcher.GetInstance();
        switcher.switchCameraToVR();
    }
}
