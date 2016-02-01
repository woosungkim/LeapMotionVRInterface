using UnityEngine;
using System.Collections;

public class CameraTransitionHandlerARInf : GrabHand_Gesture {

    SwitcherInfraredCamera switcher;

    public override void DoAction()
    {
        switcher = SwitcherInfraredCamera.GetInstance();
        switcher.switchCameraToAR();
    }
}
