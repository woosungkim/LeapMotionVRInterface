using UnityEngine;
using System.Collections;
using Leap;


public class Circle_Gesture : MonoBehaviour,IGesture 
{
    protected float minRadius = 5.0f;
    protected float maxRadius;

    protected float minArc = 4.71f;
    protected float maxArc;

    protected bool isClockwise;
    protected Vector normal;

    protected float progress;
    protected float minProgress;

    protected Pointable pointable;
    protected float radius;
    protected CircleGesture circle_gesture;

    protected HandList hands;
    protected GestureList gestures;
    protected FingerList fingers;


    public bool isRight
    { get; set; }

    public Controller _leap_controller
    { get; set; }

    public Frame lastFrame
    { get; set; }

    public bool isChecked
    { get; set; }

    public bool isPlaying
    { get; set; }

    public bool SetConfig()
    {
        _leap_controller = new Controller();
        _leap_controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
        _leap_controller.Config.SetFloat("Gesture.Circle.MinRadius", this.minRadius);
        _leap_controller.Config.SetFloat("Gesture.Circle.MinArc", this.minArc);
        _leap_controller.Config.Save();

        this.isChecked = false;
        this.isClockwise = false;
        this.isRight = false;
        this.isPlaying = false;

        return true;
    }

    protected virtual Pointable GetPointable()
    {
        pointable = circle_gesture.Pointable;
        return pointable;
    }

    protected virtual float GetProgress()
    {
        progress = circle_gesture.Progress;
        return progress;
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
        this.minRadius = radius;
        return true;
    }

    public virtual bool CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        hands = lastFrame.Hands;
        gestures = lastFrame.Gestures();

        for(int g = 0; g<lastFrame.Gestures().Count; g++)
        {
            if(lastFrame.Gestures()[g].Type == Gesture.GestureType.TYPE_CIRCLE)
            {
                circle_gesture = new CircleGesture(lastFrame.Gestures()[g]);

                AnyHand();
                
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

    //1이면 오른손 0이면 왼손
    public int AnyHand()
    {

        if (circle_gesture.IsValid)
        {
            if (circle_gesture.Hands.Rightmost.IsRight)
            {
                this.isRight = true;
            }
            else if (circle_gesture.Hands.Leftmost.IsLeft)
            {
                this.isRight = false;
            }
            return 1;
        }
        else
        {
            return 0;
        }
        
    }

    protected bool SetMinProgress(float times)
    {
        this.minProgress = times;
        return true;
    }

    protected bool SetMaxRadius(float radius)
    {
        this.maxRadius = radius;
        return true;
    }

    protected bool SetMaxArc(float arc)
    {
        this.maxArc = arc;
        return true;
    }
}

