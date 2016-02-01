using UnityEngine;
using System.Collections;

public class CameraTransitionHandlerVRInf : GrabbingHand_Gesture {

    SwitcherInfraredCamera switcher;

    public override void DoAction()
    {
        switcher = SwitcherInfraredCamera.GetInstance();
        switcher.switchCameraToVR();
    }
}
