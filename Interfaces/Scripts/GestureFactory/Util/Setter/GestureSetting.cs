using UnityEngine;
using System.Collections;
using UnityEngine;
using Leap;

public static class GestureSetting {


    public static void SetGestureCondition<T>(T ob, MountType mountType, CircleDirection circleDirection, float progress, UseArea useArea, UsingHand usingHand) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob._gestureType == GestureType.circle)
        {
            Circle_Gesture temp = ob as Circle_Gesture;

            temp._mountType = mountType;
       
            temp._useArea = useArea;
            temp._state = Gesture.GestureState.STATE_INVALID;
            temp._isChecked = false;
            temp._isClockwise = -1;
            temp._usingHand = usingHand;
            temp._isPlaying = false;
            temp.MinProgress = progress;
            if(circleDirection == CircleDirection.Clockwise)
            {
                temp._useDirection = 1;
            }
            else
            {
                temp._useDirection = -1;
            }

        }

    }

    public static void SetGestureCondition<T>(T ob, SwipeDirection swipeDirection, int sensitivity, UseArea useArea, UsingHand usingHand) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob._gestureType == GestureType.swipe)
        {
            Swipe_Gesture temp = ob as Swipe_Gesture;

            temp._mountType = temp.MountType;

            temp._isChecked = false;
            temp._direction = Vector.Zero;
            temp._state = Gesture.GestureState.STATE_INVALID;
            temp._isPlaying = false;
            temp.Sensitivity = sensitivity;
            temp._useArea = useArea;
            temp._usingHand = usingHand;

            if (temp._mountType == MountType.HeadMount)
            {
                if (swipeDirection == SwipeDirection.GoLeft)
                {
                    temp.UseAxis = 'x';
                    temp._useDirection = -1;
                }
                else if (swipeDirection == SwipeDirection.GoRight)
                {
                    temp.UseAxis = 'x';
                    temp._useDirection = 1;
                }
                else if (swipeDirection == SwipeDirection.GoDown)
                {
                    temp.UseAxis = 'z';
                    temp._useDirection = 1;
                }
                else if (swipeDirection == SwipeDirection.GoUp)
                {
                    temp.UseAxis = 'z';
                    temp._useDirection = -1;
                }
                else if (swipeDirection == SwipeDirection.GoStraight)
                {
                    temp.UseAxis = 'y';
                    temp._useDirection = 1;
                }
                else
                {
                    temp.UseAxis = 'y';
                    temp._useDirection = -1;
                }
            }
            else
            {
                if (swipeDirection == SwipeDirection.GoLeft)
                {
                    temp.UseAxis = 'x';
                    temp._useDirection = -1;
                }
                else if (swipeDirection == SwipeDirection.GoRight)
                {
                    temp.UseAxis = 'x';
                    temp._useDirection = 1;
                }
                else if (swipeDirection == SwipeDirection.GoDown)
                {
                    temp.UseAxis = 'y';
                    temp._useDirection = 1;
                }
                else if (swipeDirection == SwipeDirection.GoUp)
                {
                    temp.UseAxis = 'y';
                    temp._useDirection = -1;
                }
                else if (swipeDirection == SwipeDirection.GoStraight)
                {
                    temp.UseAxis = 'z';
                    temp._useDirection = 1;
                }
                else
                {
                    temp.UseAxis = 'z';
                    temp._useDirection = -1;
                }
            }
           
        }
        
    }


    public static void SetGestureCondition<T>(T ob, MountType mountType, UseArea useArea, UsingHand usingHand) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob._gestureType == GestureType.keytab)
        {
            KeyTap_Gesture temp = ob as KeyTap_Gesture;

            temp._mountType = temp.MountType;

            temp._useArea = useArea;
            temp._isChecked = false;
            temp._usingHand = usingHand;

        }
        else if(ob._gestureType == GestureType.screentab)
        {
            ScreenTap_Gesture temp = ob as ScreenTap_Gesture;

            temp._mountType = temp.MountType;

            temp._useArea = useArea;
            temp._isChecked = false;
            temp._usingHand = usingHand;
        }
        else if(ob._gestureType == GestureType.grabbinghand)
        {
            GrabbingHand_Gesture temp = ob as GrabbingHand_Gesture;

            temp._isChecked = false;
            temp._mountType = mountType;
            temp._useArea = useArea;
            temp._usingHand = usingHand;
        }
        else if (ob._gestureType == GestureType.fliphand)
        {
            FlipHand_Gesture temp = ob as FlipHand_Gesture;

            temp._isChecked = false;
            temp._mountType = mountType;
            temp._useArea = useArea;
            temp._usingHand = usingHand;
        }
    }

}
