using UnityEngine;
using System.Collections;
using Leap;


public class ScreenTap_Gesture : MonoBehaviour, IGesture 
{
    protected float minForwardVelocity = 50f;
    protected float maxForwardVelocity;

    protected float historySeconds = 0.1f;

    protected float minDistance = 5.0f;
    protected float maxDistance;

    protected ScreenTapGesture screentap_gesture;
    protected Vector direction;
    protected Pointable pointable;
    protected Vector position;

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
        _leap_controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
        _leap_controller.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", this.minForwardVelocity);
        _leap_controller.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", this.historySeconds);
        _leap_controller.Config.SetFloat("Gesture.ScreenTap.MinDistance", this.minDistance);
        _leap_controller.Config.Save();

        this.maxDistance = 100000;
        this.maxForwardVelocity = 100000;
        this.isChecked = false;
        isRight = false;

        return true;
    }

    public virtual bool CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        hands = lastFrame.Hands;
        gestures = lastFrame.Gestures();

        for (int g = 0; g < gestures.Count; g++ )
        {
            screentap_gesture = new ScreenTapGesture(gestures[g]);

            AnyHand();

            this.isChecked = true;
            break;
        }
        return isChecked;
    }

    public virtual void UnCheck()
    {
        isChecked = false;
    }
    
    public int AnyHand()
    {
        if(screentap_gesture.IsValid)
        {
            if (screentap_gesture.Hands.Rightmost.IsRight)
            {
                this.isRight = true;
            }
            else if (screentap_gesture.Hands.Leftmost.IsLeft)
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

    protected virtual Vector GetPosition()
    {
        this.position = screentap_gesture.Position;
        return this.position;
    }
    protected virtual Pointable GetPointable()
    {
        this.pointable = screentap_gesture.Pointable;
        return this.pointable;
    }
    protected virtual Vector GetDirection()
    {
        Vector tempDirection = screentap_gesture.Direction;
        float x = Mathf.Abs(tempDirection.x);
        float y = Mathf.Abs(tempDirection.y);
        float z = Mathf.Abs(tempDirection.z);
        if (x > y && x > z)
        {
            if (tempDirection.x > 0) this.direction = new Vector(1, 0, 0);
            else if (tempDirection.x < 0) this.direction = new Vector(-1, 0, 0);
        }
        else if (y > x && y > z)
        {
            if (tempDirection.x > 0) this.direction = new Vector(0, 1, 0);
            else if (tempDirection.x < 0) this.direction = new Vector(0, -1, 0);
        }
        else if (z > x && z > y)
        {
            if (tempDirection.x > 0) this.direction = new Vector(0, 0, 1);
            else if (tempDirection.x < 0) this.direction = new Vector(0, 0, -1);
        }
        
        return this.direction;
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

