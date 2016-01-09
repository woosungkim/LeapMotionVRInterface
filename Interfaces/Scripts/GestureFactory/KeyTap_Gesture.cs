using UnityEngine;
using System.Collections;
using Leap;


public class KeyTap_Gesture : MonoBehaviour, IGesture 
{
    protected float minDownVelocity = 50f;
    protected float maxDownVelocity;

    protected float historySeconds = 0.1f;

    protected float minDistance = 3.0f;
    protected float maxDistance;

    protected KeyTapGesture keytab_gesture;
    protected Vector direction;
    protected Pointable pointable;
    protected Vector position;
    protected float progress;

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
        _leap_controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
        _leap_controller.Config.SetFloat("Gesture.KeyTap.MinDownVelocity", this.minDownVelocity);
        _leap_controller.Config.SetFloat("Gesture.KeyTap.HistorySeconds", this.historySeconds);
        _leap_controller.Config.SetFloat("Gesture.KeyTap.MinDistance", this.minDistance);
        _leap_controller.Config.Save();

    
        this.isRight = false;
        this.isChecked = false;
        this.maxDistance = 100000;
        this.maxDownVelocity = 100000;
        this.direction = Vector.Zero;

        return true;
    }

    protected virtual Vector GetDirection()
    {
        Vector tempDirection = keytab_gesture.Direction;
        float x = Mathf.Abs(tempDirection.x);
        float y = Mathf.Abs(tempDirection.y);
        float z = Mathf.Abs(tempDirection.z);
        if(x>y && x>z)
        {
            if (tempDirection.x > 0) this.direction = new Vector(1, 0, 0);
            else if (tempDirection.x < 0) this.direction = new Vector(-1, 0, 0);
        }
        else if(y>x && y>z)
        {
            if (tempDirection.y > 0) this.direction = new Vector(0, 1, 0);
            else if (tempDirection.y < 0) this.direction = new Vector(0, -1, 0);
        }
        else if(z>x && z>y)
        {
            if (tempDirection.z > 0) this.direction = new Vector(0, 0, 1);
            else if (tempDirection.z < 0) this.direction = new Vector(0, 0, -1);
        }

        return this.direction;
    }

    protected virtual Pointable GetPointable()
    {
        pointable = keytab_gesture.Pointable;
        return pointable;
    }

    protected virtual Vector GetPosition()
    {
        position = keytab_gesture.Position;
        return position;
    }

    public virtual bool CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        hands = lastFrame.Hands;
        gestures = lastFrame.Gestures();
        for (int g = 0; g < gestures.Count; g++ )
        {
            if(gestures[g].Type == Gesture.GestureType.TYPE_KEY_TAP)
            {
                keytab_gesture = new KeyTapGesture(gestures[g]);

                AnyHand();

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

    public int AnyHand()
    {
        if(keytab_gesture.IsValid)
        {
            if(keytab_gesture.Hands.Rightmost.IsRight)
            {
                this.isRight = true;
            }
            else if(keytab_gesture.Hands.Leftmost.IsLeft)
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


