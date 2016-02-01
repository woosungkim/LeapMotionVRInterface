using UnityEngine;
using System.Collections;

public class CameraTransitionHandlerVRInf : GrabHand_Gesture {

    SwitcherInfraredCamera switcher;

    public override void DoAction()
    {
        switcher = SwitcherInfraredCamera.GetInstance();
        switcher.switchCameraToVR();
    }
}
