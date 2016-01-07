using UnityEngine;
using System.Collections;
using Leap;


public class Circle_Gesture : MonoBehaviour,GestureInterface 
{
    protected float MinRadius = 5.0f;
    protected float MaxRadius;

    protected float MinArc = 4.71f;
    protected float MaxArc;

    protected bool isClockwise;
    protected Vector Normal;

    protected float Progress;
    protected float minProgress;

    protected Pointable Pointable;
    protected float Radius;
    protected CircleGesture circle_gesture;

    public Controller _leap_controller
    { get; set; }
    public Frame lastFrame
    { get; set; }
    public bool isChecked
    { get; set; }

    public bool SetConfig()
    {
        _leap_controller = new Controller();
        _leap_controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
        _leap_controller.Config.SetFloat("Gesture.Circle.MinRadius", this.MinRadius);
        _leap_controller.Config.SetFloat("Gesture.Circle.MinArc", this.MinArc);
        _leap_controller.Config.Save();
        isChecked = false;
        isClockwise = false;
        _leap_controller.Config.Save();
        return true;
    }

    protected virtual Pointable GetPointable()
    {
        Pointable = circle_gesture.Pointable;
        return Pointable;
    }

    protected virtual float GetProgress()
    {
        Progress = circle_gesture.Progress;
        return Progress;
    }
    protected virtual Vector GetNormal()
    {
        return circle_gesture.Normal;
    }

    protected bool IsClockWise()
    {
        if(circle_gesture.Pointable.Direction.AngleTo(this.GetNormal()) <= 3.14/2)
        {
            isClockwise = true;
        }
        else
        {
            isClockwise = false;
        }

        return isClockwise;
    }

    protected bool SetMinRadius(float radius)
    {
        this.MinRadius = radius;
        return true;
    }

    public virtual bool CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        for(int g = 0; g<lastFrame.Gestures().Count; g++)
        {
            if(lastFrame.Gestures()[g].Type == Gesture.GestureType.TYPE_CIRCLE)
            {
                circle_gesture = new CircleGesture(lastFrame.Gestures()[g]);
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
    protected bool SetMinProgress(float times)
    {
        this.minProgress = times;
        return true;
    }

    protected bool SetMaxRadius(float radius)
    {
        this.MaxRadius = radius;
        return true;
    }

    protected bool SetMaxArc(float arc)
    {
        this.MaxArc = arc;
        return true;
    }
}

