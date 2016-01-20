using UnityEngine;
using System.Collections;
using Leap;

public static class ControllerSetter {
    

    public static Gesture.GestureType UseGestureType(GestureType gestureType)
    {
        if(gestureType != null)
        {
            switch(gestureType)
            {
                case GestureType.swipe : return Gesture.GestureType.TYPE_SWIPE;
                case GestureType.circle : return Gesture.GestureType.TYPE_CIRCLE;
                case GestureType.keytab : return Gesture.GestureType.TYPE_KEY_TAP;
                case GestureType.screentab: return Gesture.GestureType.TYPE_SCREEN_TAP;
                default: return Gesture.GestureType.TYPE_INVALID;
            }
        }
        else
        {
            return Gesture.GestureType.TYPE_INVALID;
        }
    }

    public static Controller SetConfig(GestureType gestureType)
    {
        
        Controller _leap_controller = new Controller();
        _leap_controller.EnableGesture(UseGestureType(gestureType));
        
        switch(gestureType)
        {
            case GestureType.swipe: 
                _leap_controller.Config.SetFloat("Gesture.Swipe.MinLength", 140);
                _leap_controller.Config.SetFloat("Gesture.Swipe.MinVelocity", 1000);
                break;
            case GestureType.circle: 
                _leap_controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
                _leap_controller.Config.SetFloat("Gesture.Circle.MinRadius", 5.0f);
                _leap_controller.Config.SetFloat("Gesture.Circle.MinArc", 4.71f);
                break;
            case GestureType.keytab:
                _leap_controller.Config.SetFloat("Gesture.KeyTap.MinDownVelocity", 50f);
                _leap_controller.Config.SetFloat("Gesture.KeyTap.HistorySeconds", 0.1f);
                _leap_controller.Config.SetFloat("Gesture.KeyTap.MinDistance", 3.0f);
                break;
            case GestureType.screentab:
                _leap_controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
                _leap_controller.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", 50f);
                _leap_controller.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", 0.1f);
                _leap_controller.Config.SetFloat("Gesture.ScreenTap.MinDistance", 5.0f);
                break;
            default: break;
        }
        _leap_controller.Config.Save();

        return _leap_controller;
    }

}
