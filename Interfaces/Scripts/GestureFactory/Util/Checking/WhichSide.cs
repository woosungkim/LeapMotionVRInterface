using UnityEngine;
using System.Collections;
using Leap;

//This static class calculate several value.
//Almost gesture classes have similar processes. So using this, you can get gesture option easily.
public static class WhichSide{

    //This static method indicates that the gesture is captured on the desired area.
	public static bool capturedSide(Hand hand, UseArea useArea, MountType mountType)
    {
        if (mountType == MountType.TableMount) // If application mount type is table mount.
        {                                      // left, right : x, up,down : z
            //Modified coordinate to relative axis of unity camera.
            Vector position = hand.PalmPosition;
            Vector3 unityPosition = position.ToUnity(); // Set coordinate value to unity scale.
            Vector3 toPos = new Vector3(((unityPosition.x + 150.0f) / 300.0f), (unityPosition.y / 300.0f), ((unityPosition.z + 150.0f) / 300.0f));
            Vector3 pos = Camera.main.ViewportToWorldPoint(toPos);
            Vector3 tempos = Camera.main.WorldToViewportPoint(pos);//From now, coordinate values are setted range(0~1)
            //----------------------------------------------------

            // Check condition of using area.
            switch (useArea)
            {
                case UseArea.All:
                    return true;
                case UseArea.Left:
                    if (tempos.x < 0.5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case UseArea.Right:
                    if (tempos.x >= 0.5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case UseArea.Up:
                    if (tempos.y >= 0.5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case UseArea.Down:
                    if (tempos.y < 0.5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
        // If application mount type is head mount.
        // left, right : x, up,down : y
        else
        {
            //Modified coordinate to relative axis of unity camera.
            //And change coordinate mode table mount to head mount.
            Vector position = hand.PalmPosition;
            Vector3 unityPosition = position.ToUnity();
            Vector3 toPos = new Vector3(1 - ((unityPosition.x + 150.0f) / 300.0f), ((unityPosition.z + 150.0f) / 300.0f), (unityPosition.y / 300.0f));
            Vector3 pos = Camera.main.ViewportToWorldPoint(toPos);
            Vector3 tempos = Camera.main.WorldToViewportPoint(pos);
            //------------------------------------------------------

            switch (useArea)
            {
                case UseArea.All:
                    return true;
                case UseArea.Left:
                    if (tempos.x < 0.5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case UseArea.Right:
                    if (tempos.x >= 0.5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case UseArea.Up:
                    if (tempos.y >= 0.5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case UseArea.Down:
                    if (tempos.y < 0.5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }


        }
    }


    // This method check whether hand user want to use direction is captured.
    public static bool IsEnableGestureHand<T>(T ob) where T : IGesture
    {
        if (ob._usingHand == UsingHand.All) // If UsingHand value is All.
        {
            return true;
        }
        else if ((ob._usingHand == UsingHand.Left) && (ob.tempHand.IsLeft)) //If UsingHand value is Left and captured hand side is left.
        {
            return true;
        }
        else if ((ob._usingHand == UsingHand.Right) && ob.tempHand.IsRight)//If UsingHand value is Right and captured hand side is Right.
        {
            return true;
        }
        return false;
    }
}
