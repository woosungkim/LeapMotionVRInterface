using UnityEngine;
using System.Collections;
using Leap;

// This static class calculate gesture's value.
// The value calculdated by method is the common elements to Gesture.
// The other methods are not need operations are defined in each gesture classes. 
public static class PropertyGetter {
    
    //This static method is calculate gesture's direction.
    //We provide only base vector of x,y,z axis.
    //If you want to get more complicated value, modify this method or define new method in your gesture script.
	public static Vector GetDirection<T>(T ob) where T : IGesture
    {
        if(ob._gestureType == GestureType.swipe) 
        {
            Swipe_Gesture temp = ob as Swipe_Gesture;
            if (temp._swipe_gestrue != null)// If gesture instance is valid.
            {
                Vector tempDirection = temp._swipe_gestrue.Direction;
                float x = Mathf.Abs(tempDirection.x);
                float y = Mathf.Abs(tempDirection.y);
                float z = Mathf.Abs(tempDirection.z);
                if (x > y && x > z)
                {
                    if (tempDirection.x > 0) temp._direction = new Vector(1, 0, 0);
                    else if (tempDirection.x < 0) temp._direction = new Vector(-1, 0, 0);
                }
                else if (y > x && y > z)
                {
                    if (tempDirection.x > 0) temp._direction = new Vector(0, 1, 0);
                    else if (tempDirection.x < 0) temp._direction = new Vector(0, -1, 0);
                }
                else if (z > x && z > y)
                {
                    if (tempDirection.x > 0) temp._direction = new Vector(0, 0, 1);
                    else if (tempDirection.x < 0) temp._direction = new Vector(0, 0, -1);
                }
                return temp._direction;
            }
            else//If gesture instance is not valid of other error, return zero vector.
            {
                return new Vector(0,0,0);
            }
        }
        else if (ob._gestureType == GestureType.keytab)
        {
            KeyTap_Gesture temp = ob as KeyTap_Gesture;
            if (temp._keytab_gesture != null)
            {
                Vector tempDirection = temp._keytab_gesture.Direction;
                float x = Mathf.Abs(tempDirection.x);
                float y = Mathf.Abs(tempDirection.y);
                float z = Mathf.Abs(tempDirection.z);
                if (x > y && x > z)
                {
                    if (tempDirection.x > 0) temp._direction = new Vector(1, 0, 0);
                    else if (tempDirection.x < 0) temp._direction = new Vector(-1, 0, 0);
                }
                else if (y > x && y > z)
                {
                    if (tempDirection.y > 0) temp._direction = new Vector(0, 1, 0);
                    else if (tempDirection.y < 0) temp._direction = new Vector(0, -1, 0);
                }
                else if (z > x && z > y)
                {
                    if (tempDirection.z > 0) temp._direction = new Vector(0, 0, 1);
                    else if (tempDirection.z < 0) temp._direction = new Vector(0, 0, -1);
                }

                return temp._direction;
            }
            else
            {
                return new Vector(0,0,0);
            }
        }
        else if (ob._gestureType == GestureType.screentab)
        {
            ScreenTap_Gesture temp = ob as ScreenTap_Gesture;
            if (temp._screentap_gesture != null)
            {
                Vector tempDirection = temp._screentap_gesture.Direction;
                float x = Mathf.Abs(tempDirection.x);
                float y = Mathf.Abs(tempDirection.y);
                float z = Mathf.Abs(tempDirection.z);
                if (x > y && x > z)
                {
                    if (tempDirection.x > 0) temp._direction = new Vector(1, 0, 0);
                    else if (tempDirection.x < 0) temp._direction = new Vector(-1, 0, 0);
                }
                else if (y > x && y > z)
                {
                    if (tempDirection.x > 0) temp._direction = new Vector(0, 1, 0);
                    else if (tempDirection.x < 0) temp._direction = new Vector(0, -1, 0);
                }
                else if (z > x && z > y)
                {
                    if (tempDirection.x > 0) temp._direction = new Vector(0, 0, 1);
                    else if (tempDirection.x < 0) temp._direction = new Vector(0, 0, -1);
                }

                return temp._direction;
            }
            else
            {
                return new Vector(0,0,0);
            }
        }
        else
        {
            return new Vector(0,0,0);//I
        }
    }

    //For circle gesture, direction is clockwise.
    //All gesture object has pointable object. Using this, you can get angle element.
    //Using angle value in the caluculation can be determinded move in any direction.
    public static int IsClockWise<T>(T ob) where T : IGesture
    {
        if(ob._gestureType == GestureType.circle)
        {
            Circle_Gesture tempCircle = ob as Circle_Gesture;

            if (tempCircle._circle_gesture.Pointable.Direction.AngleTo(tempCircle.GetNormal()) <= 3.14 / 2)
            {
                tempCircle._isClockwise = 1; // If direction is clock wise.
            }
            else
            {
                tempCircle._isClockwise = -1; // If direction is counter clock wise.
            }

            return tempCircle._isClockwise;
        }

        return 0;
    }

    // Method for getting gesture's pointable object.
    public static Pointable GetPointable<T>(T ob) where T : IGesture
    {
        if(ob._gestureType == GestureType.circle)
        {
            Circle_Gesture tempCircle = ob as Circle_Gesture;
            tempCircle._pointable = tempCircle._circle_gesture.Pointable;
            return tempCircle._pointable;
        }
        else if(ob._gestureType == GestureType.swipe)
        {
            Swipe_Gesture tempSwipe = ob as Swipe_Gesture;
            tempSwipe._pointable = tempSwipe._swipe_gestrue.Pointable;
            return tempSwipe._pointable;
        }
        else if(ob._gestureType == GestureType.keytab)
        {
            KeyTap_Gesture tempKeyTab = ob as KeyTap_Gesture;
            tempKeyTab._pointable = tempKeyTab._keytab_gesture.Pointable;
            return tempKeyTab._pointable;

        }
        else if(ob._gestureType == GestureType.screentab)
        {
            ScreenTap_Gesture tempScreenTab = ob as ScreenTap_Gesture;
            tempScreenTab._pointable = tempScreenTab._screentap_gesture.Pointable;
            return tempScreenTab._pointable;
        }
        else
        {
            return new Pointable();
        }
    }

    // Someone who may also be required where it originated gesture.
    // If you use this value, possibility arise up about new gesture of another pattern.
    public static Vector GetGestureInvokePosition<T>(T ob) where T : IGesture
    {
        if (ob._gestureType == GestureType.swipe)
        {
            Swipe_Gesture tempSwipe = ob as Swipe_Gesture;
            if (tempSwipe._swipe_gestrue != null)
            {
                tempSwipe._position = tempSwipe._swipe_gestrue.Position;
                return tempSwipe._position;
            }
        }
        else if (ob._gestureType == GestureType.keytab)
        {
            KeyTap_Gesture tempKeyTab = ob as KeyTap_Gesture;
            if(tempKeyTab._keytab_gesture!=null)
            {
                tempKeyTab._position = tempKeyTab._keytab_gesture.Position;
                return tempKeyTab._position;
            }
            

        }
        else if (ob._gestureType == GestureType.screentab)
        {
            ScreenTap_Gesture tempScreenTab = ob as ScreenTap_Gesture;
            if(tempScreenTab._screentap_gesture!=null)
            {
                tempScreenTab._position = tempScreenTab._screentap_gesture.Position;
                return tempScreenTab._position;
            }
        }
        
        return new Vector(0,0,0);
        
    }
}
