using UnityEngine;
using System.Collections;

public class CameraTransitionHandlerAR : GrabHand_Gesture {

    SwitcherHDCamera switcher;

    public override void DoAction()
    {
        switcher = SwitcherHDCamera.GetInstance();
        switcher.switchCameraToAR();
    }
}
