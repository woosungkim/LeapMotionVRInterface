using UnityEngine;
using System.Collections;
using Leap;

public static class PropertyGetter {

	public static Vector GetDirection<T>(T ob) where T : IGesture
    {
        string t = ob.GetType().ToString();
        
        if(ob.gt == GestureType.swipe)
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
        else if (ob.gt == GestureType.keytab)
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
        else if (ob.gt == GestureType.screentab)
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
}
