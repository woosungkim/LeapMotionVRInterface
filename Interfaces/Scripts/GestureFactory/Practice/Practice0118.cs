using UnityEngine;
using System.Collections;
using Leap;

public class Practice0118 : MonoBehaviour {


    Controller _leap_controller;
    Frame _lastFrame;
    HandList Hands;
    GestureList _gestures;

	// Use this for initialization
	void Start () {
        _leap_controller = ControllerSetter.SetConfig(GestureType.swipe);   
	}
	
	// Update is called once per frame
	void Update () {
        _lastFrame = _leap_controller.Frame(0);
        Hands = _lastFrame.Hands;
        _gestures = _lastFrame.Gestures();

        foreach(Hand hand in Hands)
        {
            foreach(Gesture gesture in _gestures)
            {
                if(gesture.Type == Gesture.GestureType.TYPE_SWIPE)
                {
                    print("Check");
                }
            }
        }
	}
}
