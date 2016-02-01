using UnityEngine;
using System.Collections;

public class CameraTransitionHandlerVR : GrabHand_Gesture {

    SwitcherHDCamera switcher;

    public override void DoAction()
    {
        switcher = SwitcherHDCamera.GetInstance();
        switcher.switchCameraToVR();
    }
}
