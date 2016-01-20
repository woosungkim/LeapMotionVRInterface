using UnityEngine;
using System.Collections;
using Leap;

public static class PropertyGetter {

	public static Vector GetDirection<T>(T ob) where T : IGesture
    {
        string t = ob.GetType().ToString();
        
        if(ob._gestureType == GestureType.swipe)
        {
            Swipe_Gesture temp = ob as Swipe_Gesture;
            if (temp._swipe_gestrue != null)
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
            else
            {
                return new Vector(1,0,0);
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
                return null;
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
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public static bool AnyHand<T>(T ob) where T : IGesture
    {
        switch(ob._gestureType)
        {
            case  GestureType.swipe :
                Swipe_Gesture temp1 = ob as Swipe_Gesture;
                if (temp1._swipe_gestrue.IsValid)
                {
                    if (temp1._swipe_gestrue.Hands.Rightmost.IsRight)
                    {
                        temp1._isRight = true;
                    }
                    else if (temp1._swipe_gestrue.Hands.Leftmost.IsLeft)
                    {
                        temp1._isRight = false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
                
            case GestureType.circle :
                Circle_Gesture temp2 = ob as Circle_Gesture;
                if (temp2._circle_gesture.IsValid)
                {
                    if (temp2._circle_gesture.Hands.Rightmost.IsRight)
                    {
                        temp2._isRight = true;
                    }
                    else if (temp2._circle_gesture.Hands.Leftmost.IsLeft)
                    {
                        temp2._isRight = false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            case GestureType.keytab :
                KeyTap_Gesture temp3 = ob as KeyTap_Gesture;
                if (temp3._keytab_gesture.IsValid)
                {
                    if (temp3._keytab_gesture.Hands.Rightmost.IsRight)
                    {
                        temp3._isRight = true;
                    }
                    else if (temp3._keytab_gesture.Hands.Leftmost.IsLeft)
                    {
                        temp3._isRight = false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
                
            case GestureType.screentab :
                ScreenTap_Gesture temp4 = ob as ScreenTap_Gesture;
                if (temp4._screentap_gesture.IsValid)
                {
                    if (temp4._screentap_gesture.Hands.Rightmost.IsRight)
                    {
                        temp4._isRight = true;
                    }
                    else if (temp4._screentap_gesture.Hands.Leftmost.IsLeft)
                    {
                        temp4._isRight = false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
                
            default :
                return false;
        }
    }
}
