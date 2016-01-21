using UnityEngine;
using System.Collections;
using Leap;

public class MyHandler : Circle_Gesture {

    public override void DoAction()
    {
        print("Gesture capture");
    }
}
