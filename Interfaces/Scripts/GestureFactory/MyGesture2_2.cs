using UnityEngine;
using System.Collections;
using Leap;
public class MyGesture2_2 : Swipe_Gesture {

    public bool result;

	// Use this for initialization
	void Start () {
        result = false;
        SetConfig();
	}
	
	// Update is called once per frame
	void Update () {
        //result = CheckGesture();
        if(result == true)
        {/*
            print("swipe");
            int i = AnyHand();
            if(i==1)
            {
                print("right hand");
            }
            else
            {
                print("left hand");
            }
          */
        }
        UnCheck();
	}

    public override void CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        _hands = lastFrame.Hands;
        _gestures = lastFrame.Gestures();

        for (int g = 0; g < _gestures.Count; g++)
        {
            if (_gestures[g].Type == Gesture.GestureType.TYPE_SWIPE)
            {

                _swipe_gestrue = new SwipeGesture(_gestures[g]);
                
                if(_gestures[g].State == Gesture.GestureState.STATE_START)
                {
                    print("start");
                }
                else if (_gestures[g].State == Gesture.GestureState.STATE_UPDATE)
                {
                    print("update");
                }
                else if(_gestures[g].State == Gesture.GestureState.STATE_STOP)
                {
                    print("stop");
                    
                }
                else
                {
                    print("invalid");
                }
                AnyHand();

                this.isChecked = true;
                break;
            }
        }
        //return isChecked;
    }
}
