using UnityEngine;
using System.Collections;
using Leap;

public class MyHandler : Circle_Gesture {


	void Start()
    {
        SetGestureCondition(1, 1);
        SetMount();
    }

   

    protected override void DoAction()
    {
        print("Gesture capture");
    }
}
