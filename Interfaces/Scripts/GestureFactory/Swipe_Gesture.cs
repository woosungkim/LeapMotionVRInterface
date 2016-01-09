using UnityEngine;
using System.Collections;
using Leap;



public class Swipe_Gesture : MonoBehaviour, IGesture 
{
    protected float minLength = 100;
    protected float minVelocity = 1000;
    protected Vector3 boundary;
    protected float maxVelocity;
    protected float maxLength;
    protected Vector direction;
    protected SwipeGesture swipe_gestrue;

    protected HandList hands;
    protected GestureList gestures;
    protected FingerList fingers;

    protected Vector startPoint;
    protected Vector endPoint;


    public bool isRight
    { get; set; }
    public bool isLeft
    { get; set; }

    public Controller _leap_controller
    { get; set; }

    public Frame lastFrame
    { get;  set; }

    public bool isChecked
    { get; set; }

    public bool isPlaying
    { get; set; }

    public bool SetConfig()
    {
        _leap_controller = new Controller();
        _leap_controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
        _leap_controller.Config.SetFloat("Gesture.Swipe.MinLength", this.minLength);
        _leap_controller.Config.SetFloat("Gesture.Swipe.MinVelocity", this.minVelocity);
        _leap_controller.Config.Save();

        isChecked = false;
        this.direction = Vector.Zero;
        this.boundary = Vector3.zero;
        this.maxVelocity = 100000;
        this.maxLength = 100000;
        this.isRight = false;
        this.isLeft = false;
        this.isPlaying = false;
        return true;
    }

    public virtual bool CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        hands = lastFrame.Hands;
        gestures = lastFrame.Gestures();

        for (int g = 0; g < gestures.Count; g++ )
        {
            if(gestures[g].Type == Gesture.GestureType.TYPE_SWIPE)
            {
               
                swipe_gestrue = new SwipeGesture(gestures[g]);

                if (gestures[g].Hands.Rightmost.IsRight)
                {
                    this.isRight = true;
                }
                else if(gestures[g].Hands.Leftmost.IsLeft)
                {
                    this.isLeft = true;
                }

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

    //1이면 오른손 0이면 왼손 제스처
    public int AnyHand()
    {
        if (this.isRight)
        {
            this.isRight = false;
            return 1;
        }
        else
        {
            this.isLeft = false;
            return 0;
        }
    }

    protected virtual Vector GetDirection()
    {
        Vector tempDirection = swipe_gestrue.Direction;
        float x = Mathf.Abs(tempDirection.x);
        float y = Mathf.Abs(tempDirection.y);
        float z = Mathf.Abs(tempDirection.z);
        if(x>y&&x>z)
        {
            if (tempDirection.x > 0) this.direction = new Vector(1, 0, 0);
            else if (tempDirection.x < 0) this.direction = new Vector(-1, 0, 0);
        }
        else if(y>x&&y>z)
        {
            if (tempDirection.x > 0) this.direction = new Vector(0, 1, 0);
            else if (tempDirection.x < 0) this.direction = new Vector(0, -1, 0);
        }
        else if(z>x&&z>y)
        {
            if (tempDirection.x > 0) this.direction = new Vector(0, 0, 1);
            else if (tempDirection.x < 0) this.direction = new Vector(0, 0, -1);
        }
        return this.direction;
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

