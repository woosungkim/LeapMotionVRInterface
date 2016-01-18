using UnityEngine;
using System.Collections;
using Leap;

public class MyHandler : Swipe_Gesture {


	void Start()
    {
        SetGestureCondition('x', 1, 4);
        OnVR();
    }

   

    protected override void DoAction()
    {
        print("Gesture capture");
    }
}
