using UnityEngine;
using System.Collections;


public class MyGesture : Circle_Gesture {

   
	// Use this for initialization
	void Start () {
        SetConfig();
        SetGestureCondition(-1, 1f);
	}

    protected override void DoAction()
    {
        print("원이당 ~~~");
    }
}
