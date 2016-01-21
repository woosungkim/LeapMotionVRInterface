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
                Swipe_Gesture tempSwipe = ob as Swipe_Gesture;
                if (tempSwipe._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((tempSwipe._usingHand == UsingHand.Left) && (tempSwipe.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((tempSwipe._usingHand == UsingHand.Right) && tempSwipe.Hands.Frontmost.IsRight)
                {
                    return true;
                }
                return false;
            case GestureType.circle:
                Circle_Gesture tempCircle = ob as Circle_Gesture;

                if (tempCircle._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((tempCircle._usingHand == UsingHand.Left) && (tempCircle.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((tempCircle._usingHand == UsingHand.Right) && tempCircle.Hands.Frontmost.IsRight)
                {
                    return true;
                }
                return false;

            case GestureType.keytab:
                KeyTap_Gesture tempKeytab = ob as KeyTap_Gesture;

                if (tempKeytab._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((tempKeytab._usingHand == UsingHand.Left) && (tempKeytab.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((tempKeytab._usingHand == UsingHand.Right) && tempKeytab.Hands.Frontmost.IsRight)
                {
                    return true;
                }
                return false;

            case GestureType.screentab:
                ScreenTap_Gesture tempScreenTab = ob as ScreenTap_Gesture;

                if (tempScreenTab._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((tempScreenTab._usingHand == UsingHand.Left) && (tempScreenTab.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((tempScreenTab._usingHand == UsingHand.Right) && tempScreenTab.Hands.Frontmost.IsRight)
                {
                    return true;
                }
                return false;
            case GestureType.grabbinghand:
                GrabbingHand_Gesture tempGrabbingHand = ob as GrabbingHand_Gesture;

                if (tempGrabbingHand._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((tempGrabbingHand._usingHand == UsingHand.Left) && (tempGrabbingHand.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((tempGrabbingHand._usingHand == UsingHand.Right) && tempGrabbingHand.Hands.Frontmost.IsRight)
                {
                    return true;
                }
                return false;
            case GestureType.fliphand:
                FlipHand_Gesture tempFlipHand = ob as FlipHand_Gesture;

                if (tempFlipHand._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((tempFlipHand._usingHand == UsingHand.Left) && (tempFlipHand.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((tempFlipHand._usingHand == UsingHand.Right) && tempFlipHand.Hands.Frontmost.IsRight)
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
            Circle_Gesture tempCircle = ob as Circle_Gesture;

            if (tempCircle._circle_gesture.Pointable.Direction.AngleTo(tempCircle.GetNormal()) <= 3.14 / 2)
            {
                tempCircle._isClockwise = 1;
            }
            else
            {
                tempCircle._isClockwise = -1;
            }

            return tempCircle._isClockwise;
        }

        return 0;
    }
}
