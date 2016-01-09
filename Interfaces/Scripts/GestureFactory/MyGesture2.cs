using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Leap; 

public class MyGesture2 : Swipe_Gesture {

    public bool result;
    public bool flagAR;
    public bool flagVR;

	// Use this for initialization
	void Start () {
        result = false;
        flagAR = false;
        flagVR = false;
        SetConfig();
	}
	
	// Update is called once per frame
	void Update () {
        if(!isChecked)
        {
            result = CheckGesture();
        }
        
        /*
        result = CheckGesture();
        if(result == true)
        {
           // print("Swipe Gesture");
        }
        */
        //UnCheck();
        
	}

    public override bool CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        hands = lastFrame.Hands;
        gestures = lastFrame.Gestures();
        foreach (Hand hand in hands)
        {
            // 오른손!
           // if (hand.IsRight)
           // {
                //Debug.Log ("Right hand is detecting");
                //Debug.Log (hand.PalmPosition);
                // debug text
            GameObject debugText = GameObject.Find("DebugText");
            debugText.GetComponent<Text>().text = " x : " + hand.PalmPosition.x +
                " y : " + hand.PalmPosition.y +
                    " z : " + hand.PalmPosition.z;

            ////
            FingerList fingers = hand.Fingers;

            // 이부분을.. gesture id로 확정처리 가능하지 않을까..?
            foreach (Gesture gesture in gestures)
            {
                int id = gesture.Id;

                // 스와입 제스처에 대한 처리..
                if (gesture.Type == Gesture.GestureType.TYPE_SWIPE)
                {
                    
                    print("Swipe Gesture");
                    swipe_gestrue = new SwipeGesture(gesture);
                    AnyHand();
                    if (!isPlaying && gesture.State == Gesture.GestureState.STATE_START)
                    {
                        isPlaying = true;
                        startPoint = hand.PalmPosition;
                        print("start");
                        // print("start position : " + startPoint);

                    }
                    if (isPlaying && gesture.State == Gesture.GestureState.STATE_STOP)
                    {
                        endPoint = hand.PalmPosition;
                        //isPlaying = false;

                        //print("end position : " + endPoint);
                        print("stop");

                        if (startPoint.y < 130 && endPoint.y < 130 &&
                            startPoint.z < endPoint.z)
                        {

                            this.isChecked = true;
                            this.flagAR = true;
                            isPlaying = !isPlaying;
                            break;
                        }

                        else if (startPoint.y < 130 && endPoint.y < 130 &&
                            startPoint.z > endPoint.z)
                        {

                            this.isChecked = true;
                            this.flagVR = true;
                            isPlaying = !isPlaying;
                            break;
                        }


                    }
                }
            }
            if (isPlaying)
            {
                print("other");
                endPoint = hand.PalmPosition;
                if (startPoint.y < 130 && endPoint.y < 130 &&
                            startPoint.z < endPoint.z)
                {

                    this.isChecked = true;
                    this.flagAR = true;
                    isPlaying = !isPlaying;

                }

                if (startPoint.y < 130 && endPoint.y < 130 &&
                    startPoint.z > endPoint.z)
                {

                    this.isChecked = true;
                    this.flagVR = true;
                    isPlaying = !isPlaying;

                }
                
               
            }

            //}
            //if (hand.IsLeft)
               // Debug.Log("Left hand is detecting");
        }

        return isChecked;
    }
}
