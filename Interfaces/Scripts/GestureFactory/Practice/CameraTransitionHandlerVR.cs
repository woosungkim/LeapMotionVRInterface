using UnityEngine;
using System.Collections;

public class CameraTransitionHandlerVR : GrabbingHand_Gesture {

    SwitcherHDCamera switcher;

    public override void DoAction()
    {
        switcher = SwitcherHDCamera.GetInstance();
        switcher.switchCameraToVR();
    }
}
