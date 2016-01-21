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

    public static bool IsEnableGestureHand<T>(T ob) where T : IGesture
    {
        switch(ob._gestureType)
        {
            case GestureType.swipe:
                Swipe_Gesture temp1 = ob as Swipe_Gesture;
                if (temp1._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((temp1._usingHand == UsingHand.Left) && (temp1.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((temp1._usingHand == UsingHand.Right) && temp1.Hands.Frontmost.IsRight)
                {
                    return true;
                }
                return false;
            case GestureType.circle:
                Circle_Gesture temp2 = ob as Circle_Gesture;

                if (temp2._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((temp2._usingHand == UsingHand.Left) && (temp2.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((temp2._usingHand == UsingHand.Right) && temp2.Hands.Frontmost.IsRight)
                {
                    return true;
                }
                return false;

            case GestureType.keytab:
                KeyTap_Gesture temp3 = ob as KeyTap_Gesture;

                if (temp3._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((temp3._usingHand == UsingHand.Left) && (temp3.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((temp3._usingHand == UsingHand.Right) && temp3.Hands.Frontmost.IsRight)
                {
                    return true;
                }
                return false;

            case GestureType.screentab:
                ScreenTap_Gesture temp4 = ob as ScreenTap_Gesture;

                if (temp4._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((temp4._usingHand == UsingHand.Left) && (temp4.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((temp4._usingHand == UsingHand.Right) && temp4.Hands.Frontmost.IsRight)
                {
                    return true;
                }
                return false;
            case GestureType.grabbinghand:
                GrabbingHand_Gesture temp5 = ob as GrabbingHand_Gesture;

                if (temp5._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((temp5._usingHand == UsingHand.Left) && (temp5.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((temp5._usingHand == UsingHand.Right) && temp5.Hands.Frontmost.IsRight)
                {
                    return true;
                }
                return false;
            case GestureType.fliphand:
                FlipHand_Gesture temp6 = ob as FlipHand_Gesture;

                if (temp6._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((temp6._usingHand == UsingHand.Left) && (temp6.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((temp6._usingHand == UsingHand.Right) && temp6.Hands.Frontmost.IsRight)
                {
                    return true;
                }
                return false;
            default:
                return false;
        }
    }


    public static int IsClockWise<T>(T ob) where T : IGesture
    {
        if(ob._gestureType == GestureType.circle)
        {
            Circle_Gesture temp = ob as Circle_Gesture;

            if (temp._circle_gesture.Pointable.Direction.AngleTo(temp.GetNormal()) <= 3.14 / 2)
            {
                temp._isClockwise = 1;
            }
            else
            {
                temp._isClockwise = -1;
            }

            return temp._isClockwise;
        }

        return 0;
    }
}
