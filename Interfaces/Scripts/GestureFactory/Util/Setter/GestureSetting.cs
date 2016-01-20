using UnityEngine;
using System.Collections;
using UnityEngine;
using Leap;

public static class GestureSetting {
     
    public static void SetGestureCondition<T>( T ob , int direction, float progress) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if(ob._gestureType == GestureType.circle)
        {
            Circle_Gesture temp = ob as Circle_Gesture;

            if (temp._mountType == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }

            temp._useArea = UseArea.All;
            temp._state = Gesture.GestureState.STATE_INVALID;
            temp._isChecked = false;
            temp._isClockwise = -1;
            temp._isRight = false;
            temp._isPlaying = false;
            temp._useDirection = direction;
            temp._minProgress = progress;
            
        }
        
    }

    public static void SetGestureCondition<T>(T ob, int direction, float progress, UseArea useArea) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob._gestureType == GestureType.circle)
        {
            Circle_Gesture temp = ob as Circle_Gesture;

            if (temp._mountType == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }

            temp._useArea = useArea;
            temp._state = Gesture.GestureState.STATE_INVALID;
            temp._isChecked = false;
            temp._isClockwise = -1;
            temp._isRight = false;
            temp._isPlaying = false;
            temp._useDirection = direction;
            temp._minProgress = progress;

        }

    }

    public static void SetGestureCondition<T>(T ob, char axis, int direction) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob._gestureType == GestureType.swipe)
        {
            Swipe_Gesture temp = ob as Swipe_Gesture;

            if (temp._mountType == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }
            temp._isChecked = false;
            temp._direction = Vector.Zero;
            temp._state = Gesture.GestureState.STATE_INVALID;
            temp._isRight = false;
            temp._isPlaying = false;

            temp._useAxis = axis;
            temp._useDirection = direction;
            temp._useArea = UseArea.All;


        }

    }

    public static void SetGestureCondition<T>(T ob, char axis, int direction, UseArea useArea) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob._gestureType == GestureType.swipe)
        {
            Swipe_Gesture temp = ob as Swipe_Gesture;

            if (temp._mountType == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }
            temp._isChecked = false;
            temp._direction = Vector.Zero;
            temp._state = Gesture.GestureState.STATE_INVALID;
            temp._isRight = false;
            temp._isPlaying = false;

            temp._useAxis = axis;
            temp._useDirection = direction;
            temp._useArea = useArea;
        
            
        }
        
    }

    public static void SetGestureCondition<T>(T ob, char axis, int direction, int sensitivity, UseArea useArea) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob._gestureType == GestureType.swipe)
        {
            Swipe_Gesture temp = ob as Swipe_Gesture;

            if (temp._mountType == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }
            temp._isChecked = false;
            temp._direction = Vector.Zero;
            temp._state = Gesture.GestureState.STATE_INVALID;
            temp._isRight = false;
            temp._isPlaying = false;

            temp._useAxis = axis;
            temp._useDirection = direction;
            temp._sensitivity = sensitivity;
            temp._useArea = useArea;
            
           
        }
        
    }

    public static void SetGestureCondition<T>(T ob) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob._gestureType == GestureType.keytab)
        {
            KeyTap_Gesture temp = ob as KeyTap_Gesture;

            if (temp._mountType == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }

            temp._useArea = UseArea.All;
            temp._isChecked = false;
            temp._isRight = false;

        }
        else if(ob._gestureType == GestureType.screentab)
        {
            ScreenTap_Gesture temp = ob as ScreenTap_Gesture;

            if (temp._mountType == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }

            temp._useArea = UseArea.All;
            temp._isChecked = false;
            temp._isRight = false;
            
        }

    }

    public static void SetGestureCondition<T>(T ob, UseArea useArea) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob._gestureType == GestureType.keytab)
        {
            KeyTap_Gesture temp = ob as KeyTap_Gesture;

            if (temp._mountType == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }

            temp._useArea = useArea;
            temp._isChecked = false;
            temp._isRight = false;

        }
        else if(ob._gestureType == GestureType.screentab)
        {
            ScreenTap_Gesture temp = ob as ScreenTap_Gesture;
            if (temp._mountType == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }

            temp._useArea = useArea;
            temp._isChecked = false;
            temp._isRight = false;
        }

    }

}
