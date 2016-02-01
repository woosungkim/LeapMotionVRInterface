using UnityEngine;
using System.Collections;
using UnityEngine;
using Leap;


// After controller setting using ControllerSetter.SetConfig(), you should set gesture options.
// This class is static class for setting gesture options.
// Methods in this class, has variety and different type arguments.
// When you use member method, please check type what is properly method.
public static class GestureSetting {

    public static void SetGestureCondition<T>(T ob, MountType mountType, CircleDirection circleDirection, float progress, UseArea useArea, UsingHand usingHand) where T : IGesture
    {
        if (ob._gestureType == GestureType.circle) // If gesture type is circle.
        {
            Circle_Gesture tempCircle = ob as Circle_Gesture;

            /* Initiate gesture option */
            tempCircle._state = Gesture.GestureState.STATE_INVALID; 
            tempCircle._isChecked = false; 
            tempCircle._isClockwise = -1;
            tempCircle._isPlaying = false;

            /* Initiate to user options. */
            tempCircle._usingHand = usingHand;
            tempCircle._mountType = mountType;
            tempCircle._useArea = useArea; 
            tempCircle.MinProgress = progress;
            if (circleDirection == CircleDirection.Clockwise)
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
        if (ob._gestureType == GestureType.swipe)// If gesture type is swipe.
        {
            Swipe_Gesture tempSwipe = ob as Swipe_Gesture;

            /* Initiate gesture option */
            tempSwipe._isChecked = false;
            tempSwipe._direction = Vector.Zero;
            tempSwipe._state = Gesture.GestureState.STATE_INVALID;
            tempSwipe._isPlaying = false;

            /* Initiate to user options. */
            tempSwipe._mountType = tempSwipe.MountType;
            tempSwipe.Sensitivity = sensitivity;
            tempSwipe._useArea = useArea;
            tempSwipe._usingHand = usingHand;

            //For swipe gesture, this gesture has many direction about mount type.
            //So, We process this coordinates.
            if (tempSwipe._mountType == MountType.HeadMount)
            {
                if (swipeDirection == SwipeDirection.GoLeft)
                {
                    tempSwipe.UseAxis = 'x';
                    tempSwipe._useDirection = 1;
                }
                else if (swipeDirection == SwipeDirection.GoRight)
                {
                    tempSwipe.UseAxis = 'x';
                    tempSwipe._useDirection = -1;
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
                    tempSwipe._useDirection = -1;
                }
                else if (swipeDirection == SwipeDirection.GoUp)
                {
                    tempSwipe.UseAxis = 'y';
                    tempSwipe._useDirection = 1;
                }
                else if (swipeDirection == SwipeDirection.GoStraight)
                {
                    tempSwipe.UseAxis = 'z';
                    tempSwipe._useDirection = -1;
                }
                else
                {
                    tempSwipe.UseAxis = 'z';
                    tempSwipe._useDirection = 1;
                }
            }
        }
        
    }


    public static void SetGestureCondition<T>(T ob, MountType mountType, UseArea useArea, UsingHand usingHand) where T : IGesture
    {
        if (ob._gestureType == GestureType.keytab)// If gesture type is keytab.
        {
            KeyTap_Gesture tempKeyTab = ob as KeyTap_Gesture;

            /* Initiate gesture option */
            tempKeyTab._isChecked = false;

            /* Initiate to user options. */
            tempKeyTab._mountType = tempKeyTab.MountType;
            tempKeyTab._useArea = useArea;
            tempKeyTab._usingHand = usingHand;

        }
        else if (ob._gestureType == GestureType.screentab)// If gesture type is screentab.
        {
            ScreenTap_Gesture tempScreenTab = ob as ScreenTap_Gesture;

            /* Initiate gesture option */
            tempScreenTab._isChecked = false;
            
            /* Initiate to user options. */
            tempScreenTab._useArea = useArea;
            tempScreenTab._mountType = tempScreenTab.MountType;
            tempScreenTab._usingHand = usingHand;
        }
        else if (ob._gestureType == GestureType.grabhand)// If gesture type is grabbing hand.
        {
            GrabHand_Gesture tempGrabHand = ob as GrabHand_Gesture;

            /* Initiate gesture option */
            tempGrabHand._isChecked = false;

            /* Initiate to user options. */
            tempGrabHand._mountType = mountType;
            tempGrabHand._useArea = useArea;
            tempGrabHand._usingHand = usingHand;
        }
       
    }

    public static void SetGestureCondition<T>(T ob, MountType mountType, UseArea useArea, UsingHand usingHand, PalmDirection palmDirection) where T : IGesture
    {
        if (ob._gestureType == GestureType.fliphand)// If gesture type is flip hand.
        {
            FlipHand_Gesture tempFlipHand = ob as FlipHand_Gesture;

            /* Initiate gesture option */
            tempFlipHand._isChecked = false;

            /* Initiate to user options. */
            tempFlipHand._mountType = mountType;
            tempFlipHand._useArea = useArea;
            tempFlipHand._usingHand = usingHand;
            tempFlipHand._palmDirection = palmDirection;
        }
    }

}
