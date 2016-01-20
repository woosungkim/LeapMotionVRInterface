using UnityEngine;
using System.Collections;

public class Practice0119 : Swipe_Gesture {

    bool result;
	// Use this for initialization
	void Start () {
        SetGestureCondition('x', 1, 0);
	}

    protected override void DoAction()
    {
        result = AnyHand();
        print(result);
        print("check");
    }
}
