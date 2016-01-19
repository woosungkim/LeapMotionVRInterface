using UnityEngine;
using System.Collections;
using UnityEngine;
using Leap;

public static class GesutreSetting {
     
    public static void SetGestureCondition<T>( T ob , int direction, float progress) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if(ob.gt == GestureType.circle)
        {
            Circle_Gesture temp = ob as Circle_Gesture;

            if (temp.mt == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }

            temp._userSide = 0;
            temp._state = Gesture.GestureState.STATE_INVALID;
            temp._isChecked = false;
            temp._isClockwise = -1;
            temp._isRight = false;
            temp._isPlaying = false;
            temp._useDirection = direction;
            temp._minProgress = progress;
            
        }
        
    }

    public static void SetGestureCondition<T>(T ob, char axis, int direction, int side) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob.gt == GestureType.swipe)
        {
            Swipe_Gesture temp = ob as Swipe_Gesture;

            if (temp.mt == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }
            temp._isChecked = false;
            temp._userSide = 0;
            temp._direction = Vector.Zero;
            temp._state = Gesture.GestureState.STATE_INVALID;
            temp._isRight = false;
            temp._isPlaying = false;

            temp._useAxis = axis;
            temp._useDirection = direction;
            temp._userSide = side;
        
            
        }
        
    }

    public static void SetGestureCondition<T>(T ob, char axis, int direction, int sensitivity, int side) where T : IGesture
    {
        string t = ob.GetType().ToString();

        if (ob.gt == GestureType.swipe)
        {
            Swipe_Gesture temp = ob as Swipe_Gesture;

            if (temp.mt == MountType.HeadMount)
            {
                temp._isHeadMount = true;
            }
            else
            {
                temp._isHeadMount = false;
            }
            temp._isChecked = false;
            temp._userSide = 0;
            temp._direction = Vector.Zero;
            temp._state = Gesture.GestureState.STATE_INVALID;
            temp._isRight = false;
            temp._isPlaying = false;

            temp._useAxis = axis;
            temp._useDirection = direction;
            temp._sensitivity = sensitivity;
            temp._userSide = side;
            
           
        }
        
    }

    


}
