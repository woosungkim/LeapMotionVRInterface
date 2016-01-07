using UnityEngine;
using System.Collections;
using Leap;


public class ScreenTap_Gesture : MonoBehaviour, GestureInterface 
{
    protected float MinForwardVelocity = 50f;
    protected float maxForwardVelocity;

    protected float HistorySeconds = 0.1f;

    protected float MinDistance = 5.0f;
    protected float maxDistance;

    protected ScreenTapGesture screentap_gesture;
    protected Vector Direction;
    protected Pointable Pointable;
    protected Vector Position;

    public Controller _leap_controller
    { get; set; }

    public Frame lastFrame
    { get; set; }

    public bool isChecked
    { get; set; }

    public bool SetConfig()
    {
        _leap_controller = new Controller();
        _leap_controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
        _leap_controller.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", this.MinForwardVelocity);
        _leap_controller.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", this.HistorySeconds);
        _leap_controller.Config.SetFloat("Gesture.ScreenTap.MinDistance", this.MinDistance);
        _leap_controller.Config.Save();
        this.maxDistance = 100000;
        this.maxForwardVelocity = 100000;
        this.isChecked = false;

        return true;
    }

    public virtual bool CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        for (int g = 0; g < lastFrame.Gestures().Count; g++ )
        {
            screentap_gesture = new ScreenTapGesture(lastFrame.Gestures()[g]);
            this.isChecked = true;
            break;
        }
        return isChecked;
    }

    public virtual void UnCheck()
    {
        isChecked = false;
    }
    
    protected virtual Vector GetPosition()
    {
        this.Position = screentap_gesture.Position;
        return this.Position;
    }
    protected virtual Pointable GetPointable()
    {
        this.Pointable = screentap_gesture.Pointable;
        return this.Pointable;
    }
    protected virtual Vector GetDirection()
    {
        this.Direction = screentap_gesture.Direction;
        return this.Direction;
    }

    protected bool SetMaxVelocity(float velocity)
    {
        this.maxForwardVelocity = velocity;
        return true;
    }

    protected bool SetMaxDistance(float distance)
    {
        this.maxDistance = distance;
        return true;
    }
}

