using UnityEngine;
using System.Collections;
using Leap;

//All of gesture classes has configurations.
//And for using gesture function, you should create and set up options Leap Motion controller.
//This method is setter method for Leap Motion controller.
public static class ControllerSetter {

    // The Role of this method modified correctly GestureType value.
    // This method is for the primary gestures.
    public static Gesture.GestureType UseGestureType(GestureType gestureType)
    {
        switch (gestureType)
        {
            case GestureType.swipe: return Gesture.GestureType.TYPE_SWIPE;
            case GestureType.circle: return Gesture.GestureType.TYPE_CIRCLE;
            case GestureType.keytab: return Gesture.GestureType.TYPE_KEY_TAP;
            case GestureType.screentab: return Gesture.GestureType.TYPE_SCREEN_TAP;
            default: return Gesture.GestureType.TYPE_INVALID;
        }
    }

    // The method for setting controller options.
    public static Controller SetConfig(GestureType gestureType)
    {
        Controller _leap_controller = new Controller();
        
        switch(gestureType)
        {
            case GestureType.swipe://For swipe gesture
                _leap_controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
                _leap_controller.Config.SetFloat("Gesture.Swipe.MinLength", 140);
                _leap_controller.Config.SetFloat("Gesture.Swipe.MinVelocity", 1000);
                break;
            case GestureType.circle: //For circle gesture
                _leap_controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
                _leap_controller.Config.SetFloat("Gesture.Circle.MinRadius", 5.0f);
                _leap_controller.Config.SetFloat("Gesture.Circle.MinArc", 4.71f);
                break;
            case GestureType.keytab://For key tab gesture
                _leap_controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
                _leap_controller.Config.SetFloat("Gesture.KeyTap.MinDownVelocity", 50f);
                _leap_controller.Config.SetFloat("Gesture.KeyTap.HistorySeconds", 0.1f);
                _leap_controller.Config.SetFloat("Gesture.KeyTap.MinDistance", 3.0f);
                break;
            case GestureType.screentab://For screen tab gesture
                _leap_controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
                _leap_controller.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", 50f);
                _leap_controller.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", 0.1f);
                _leap_controller.Config.SetFloat("Gesture.ScreenTap.MinDistance", 5.0f);
                break;
            case GestureType.grabbinghand://For grabbing hand gesture
                return _leap_controller;
            case GestureType.fliphand://For flip hand gesture
                return _leap_controller;
            default: break;
        }
        _leap_controller.Config.Save();// You must call this method for save controller configurations.

        return _leap_controller; // return the setted controller instance.
    }

}
