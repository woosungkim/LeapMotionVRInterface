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
        switch (ob._gestureType) // Check the gesture type.
        {
            case GestureType.swipe: // If GestureType is swipe.
                Swipe_Gesture tempSwipe = ob as Swipe_Gesture; // Wrap gesture type.
                if (tempSwipe._usingHand == UsingHand.All) // If UsingHand value is All.
                {
                    return true;
                }
                else if ((tempSwipe._usingHand == UsingHand.Left) && (tempSwipe.Hands.Frontmost.IsLeft)) //If UsingHand value is Left and captured hand side is left.
                {
                    return true;
                }
                else if ((tempSwipe._usingHand == UsingHand.Right) && tempSwipe.Hands.Frontmost.IsRight)//If UsingHand value is Right and captured hand side is Right.
                {
                    return true;
                }
                return false;
            case GestureType.circle: // If GestureType is circle.
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

            case GestureType.keytab:// If GestureType is keytab.
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

            case GestureType.screentab:// If GestureType is screentab.
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
           
            case GestureType.grabhand: // If GestureType is grabbing hand.
                GrabHand_Gesture tempGrabHand = ob as GrabHand_Gesture;

                if (tempGrabHand._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((tempGrabHand._usingHand == UsingHand.Left))
                {
                    foreach(Hand hand in tempGrabHand.Hands)
                    {
                        if (hand.IsLeft)
                            return true;
                    }
                    return false;
                }
                else if ((tempGrabHand._usingHand == UsingHand.Right))
                {
                    foreach(Hand hand in tempGrabHand.Hands)
                    {
                        if (hand.IsRight)
                            return true;
                    }
                    return false;
                }
                return false;
            case GestureType.fliphand: // If GestureType is fliphand.
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
            case GestureType.usercustom: // If GestureType is user custom.
                UserGesture tempUser = ob as UserGesture;

                if (tempUser._usingHand == UsingHand.All)
                {
                    return true;
                }
                else if ((tempUser._usingHand == UsingHand.Left) && (tempUser.Hands.Frontmost.IsLeft))
                {
                    return true;
                }
                else if ((tempUser._usingHand == UsingHand.Right) && (tempUser.Hands.Frontmost.IsRight))
                {
                    return true;
                }
                return false;
            default:
                return false;
        }
    }
}
