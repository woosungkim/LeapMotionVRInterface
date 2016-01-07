using UnityEngine;
using System.Collections;
using Leap;


public class KeyTap_Gesture : MonoBehaviour, GestureInterface 
{
    protected float MinDownVelocity = 50f;
    protected float maxDownVelocity;

    protected float HistorySeconds = 0.1f;

    protected float MinDistance = 3.0f;
    protected float maxDistance;

    protected KeyTapGesture keytab_gesture;
    protected Vector Direction;
    protected Pointable Pointable;
    protected Vector Position;
    protected float Progress;

    public Controller _leap_controller
    { get; set; }

    public Frame lastFrame
    { get; set; }

    public bool isChecked
    { get; set; }

    public bool SetConfig()
    {
        _leap_controller = new Controller();
        _leap_controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
        _leap_controller.Config.SetFloat("Gesture.KeyTap.MinDownVelocity", this.MinDownVelocity);
        _leap_controller.Config.SetFloat("Gesture.KeyTap.HistorySeconds", this.HistorySeconds);
        _leap_controller.Config.SetFloat("Gesture.KeyTap.MinDistance", this.MinDistance);
        isChecked = false;
        maxDistance = 100000;
        maxDownVelocity = 100000;
        _leap_controller.Config.Save();
        return true;
    }

    protected virtual Vector GetDirection()
    {
        Direction = keytab_gesture.Direction;
        return Direction;
    }

    protected virtual Pointable GetPointable()
    {
        Pointable = keytab_gesture.Pointable;
        return Pointable;
    }

    protected virtual Vector GetPosition()
    {
        Position = keytab_gesture.Position;
        return Position;
    }

    public virtual bool CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        for (int g = 0; g < lastFrame.Gestures().Count; g++ )
        {
            if(lastFrame.Gestures()[g].Type == Gesture.GestureType.TYPE_KEY_TAP)
            {
                keytab_gesture = new KeyTapGesture(lastFrame.Gestures()[g]);
                this.isChecked = true;
                break;
            }
        }

        return isChecked;
    }

    public virtual void UnCheck()
    {
        this.isChecked = false;
    }

    protected bool SetMaxDownVelocity(float velocity)
    {
        this.maxDownVelocity = velocity;
        return true;
    }

    protected bool SetMaxDistance(float distance)
    {
        this.maxDistance = distance;
        return true;
    }
}


