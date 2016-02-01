using UnityEngine;
using System.Collections;

public class CameraTransitionHandlerAR : GrabbingHand_Gesture {

    SwitcherHDCamera switcher;

    public override void DoAction()
    {
        switcher = SwitcherHDCamera.GetInstance();
        switcher.switchCameraToAR();
    }
}
