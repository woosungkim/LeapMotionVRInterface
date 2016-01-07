using UnityEngine;
using System.Collections;
using Leap;



public class Swipe_Gesture : MonoBehaviour, GestureInterface 
{
    protected float MinLength = 150;
    protected float MinVelocity = 1000;
    protected Vector3 boundary;
    protected float maxVelocity;
    protected float maxLength;
    protected Vector Direction;
    protected SwipeGesture swipe_gestrue;

    public Controller _leap_controller
    { get; set; }

    public Frame lastFrame
    { get;  set; }

    public bool isChecked
    { get; set; }

    public bool SetConfig()
    {
        _leap_controller = new Controller();
        _leap_controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
        _leap_controller.Config.SetFloat("Gesture.Swipe.MinLength", this.MinLength);
        _leap_controller.Config.SetFloat("Gesture.Swipe.MinVelocity", this.MinVelocity);
        isChecked = false;
        this.boundary = Vector3.zero;
        this.maxVelocity = 100000;
        this.maxLength = 100000;
        _leap_controller.Config.Save();
        return true;
    }

    public virtual bool CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        for (int g = 0; g < lastFrame.Gestures().Count; g++ )
        {
            if(lastFrame.Gestures()[g].Type == Gesture.GestureType.TYPE_SWIPE)
            {
                swipe_gestrue = new SwipeGesture(lastFrame.Gestures()[g]);
                this.isChecked = true;
                break;
            }
        }
        return isChecked;
    }

    public virtual void UnCheck()
    {
        isChecked = false;
    }

    protected virtual Vector GetDirection()
    {
        Direction = swipe_gestrue.Direction;
        return Direction;
    }

    protected bool SetBoundary(Vector3 boundary)
    {
        this.boundary = boundary;
        return true;
    }

    protected bool SetMaxVelocity(float velocity)
    {
        this.maxVelocity = velocity;
        return true;
    }

    protected bool SetMaxLength(float length)
    {

        this.maxLength = length;
        return true;
    }
}

