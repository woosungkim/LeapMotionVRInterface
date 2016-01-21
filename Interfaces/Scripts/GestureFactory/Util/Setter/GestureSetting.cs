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
            Circle_Gesture tempCircle = ob as Circle_Gesture;

            tempCircle._mountType = mountType;
       
            tempCircle._useArea = useArea;
            tempCircle._state = Gesture.GestureState.STATE_INVALID;
            tempCircle._isChecked = false;
            tempCircle._isClockwise = -1;
            tempCircle._usingHand = usingHand;
            tempCircle._isPlaying = false;
            tempCircle.MinProgress = progress;
            if(circleDirection == CircleDirection.Clockwise)
            {
                tempCircle._useDirection = 1;
            }
            else
            {
                tempCircle._useDirection = -1;
            }

        }

    }

    public static void SetGestureCondition<T>(T ob, SwipeDirection swipeDirection, int sensitivity, UseArea useArea, UsingHand usingHand) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob._gestureType == GestureType.swipe)
        {
            Swipe_Gesture tempSwipe = ob as Swipe_Gesture;

            tempSwipe._mountType = tempSwipe.MountType;

            tempSwipe._isChecked = false;
            tempSwipe._direction = Vector.Zero;
            tempSwipe._state = Gesture.GestureState.STATE_INVALID;
            tempSwipe._isPlaying = false;
            tempSwipe.Sensitivity = sensitivity;
            tempSwipe._useArea = useArea;
            tempSwipe._usingHand = usingHand;

            if (tempSwipe._mountType == MountType.HeadMount)
            {
                if (swipeDirection == SwipeDirection.GoLeft)
                {
                    tempSwipe.UseAxis = 'x';
                    tempSwipe._useDirection = -1;
                }
                else if (swipeDirection == SwipeDirection.GoRight)
                {
                    tempSwipe.UseAxis = 'x';
                    tempSwipe._useDirection = 1;
                }
                else if (swipeDirection == SwipeDirection.GoDown)
                {
                    tempSwipe.UseAxis = 'z';
                    tempSwipe._useDirection = 1;
                }
                else if (swipeDirection == SwipeDirection.GoUp)
                {
                    tempSwipe.UseAxis = 'z';
                    tempSwipe._useDirection = -1;
                }
                else if (swipeDirection == SwipeDirection.GoStraight)
                {
                    tempSwipe.UseAxis = 'y';
                    tempSwipe._useDirection = 1;
                }
                else
                {
                    tempSwipe.UseAxis = 'y';
                    tempSwipe._useDirection = -1;
                }
            }
            else
            {
                if (swipeDirection == SwipeDirection.GoLeft)
                {
                    tempSwipe.UseAxis = 'x';
                    tempSwipe._useDirection = -1;
                }
                else if (swipeDirection == SwipeDirection.GoRight)
                {
                    tempSwipe.UseAxis = 'x';
                    tempSwipe._useDirection = 1;
                }
                else if (swipeDirection == SwipeDirection.GoDown)
                {
                    tempSwipe.UseAxis = 'y';
                    tempSwipe._useDirection = 1;
                }
                else if (swipeDirection == SwipeDirection.GoUp)
                {
                    tempSwipe.UseAxis = 'y';
                    tempSwipe._useDirection = -1;
                }
                else if (swipeDirection == SwipeDirection.GoStraight)
                {
                    tempSwipe.UseAxis = 'z';
                    tempSwipe._useDirection = 1;
                }
                else
                {
                    tempSwipe.UseAxis = 'z';
                    tempSwipe._useDirection = -1;
                }
            }
           
        }
        
    }


    public static void SetGestureCondition<T>(T ob, MountType mountType, UseArea useArea, UsingHand usingHand) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob._gestureType == GestureType.keytab)
        {
            KeyTap_Gesture tempKeyTab = ob as KeyTap_Gesture;

            tempKeyTab._mountType = tempKeyTab.MountType;

            tempKeyTab._useArea = useArea;
            tempKeyTab._isChecked = false;
            tempKeyTab._usingHand = usingHand;

        }
        else if(ob._gestureType == GestureType.screentab)
        {
            ScreenTap_Gesture tempScreenTab = ob as ScreenTap_Gesture;

            tempScreenTab._mountType = tempScreenTab.MountType;

            tempScreenTab._useArea = useArea;
            tempScreenTab._isChecked = false;
            tempScreenTab._usingHand = usingHand;
        }
        else if(ob._gestureType == GestureType.grabbinghand)
        {
            GrabbingHand_Gesture tempGrabbingHand = ob as GrabbingHand_Gesture;

            tempGrabbingHand._isChecked = false;
            tempGrabbingHand._mountType = mountType;
            tempGrabbingHand._useArea = useArea;
            tempGrabbingHand._usingHand = usingHand;
        }
        else if (ob._gestureType == GestureType.fliphand)
        {
            FlipHand_Gesture tempFlipHand = ob as FlipHand_Gesture;

            tempFlipHand._isChecked = false;
            tempFlipHand._mountType = mountType;
            tempFlipHand._useArea = useArea;
            tempFlipHand._usingHand = usingHand;
        }
    }

}
