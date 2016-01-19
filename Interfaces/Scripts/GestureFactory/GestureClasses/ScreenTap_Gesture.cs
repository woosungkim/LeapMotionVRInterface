using UnityEngine;
using System.Collections;
using Leap;


public class ScreenTap_Gesture : MonoBehaviour, ISingleStepCheckGesture 
{
    public ScreenTapGesture _screentap_gesture;
    public Vector _direction;
    protected Pointable _pointable;
    protected Vector _position;

    protected GestureList _gestures;
    protected FingerList _fingers;

    public MountType mt;

    public bool _isRight
    { get; set; }

    public Controller _leap_controller
    { get; set; }

    public HandList Hands
    { get; set; }

    public Frame _lastFrame
    { get; set; }

    public bool _isChecked
    { get; set; }

    public bool _isPlaying
    { get; set; }

    public int _userSide
    { get; set; }

    public bool _isHeadMount
    { get; set; }

    public GestureType gt
    { get; set; }

    public virtual void Update()
    {
        if(!this._isChecked)
        {
            CheckGesture();
        }
        UnCheck();
    }

    public bool SetConfig()
    {
        _leap_controller = ControllerSetter.SetConfig(GestureType.screentab);

        this.SetMount();
        this._userSide = 0;
        this._isChecked = false;
        this._isRight = false;

        return true;
    }

    public virtual void CheckGesture()
    {
        _lastFrame = _leap_controller.Frame(0);
        Hands = _lastFrame.Hands;
        _gestures = _lastFrame.Gestures();

        foreach(Hand hand in Hands)
        {
            this._fingers = hand.Fingers;

            foreach(Gesture gesture in _gestures)
            {
                if ( (gesture.Type == Gesture.GestureType.TYPE_SCREEN_TAP) && WhichSide.capturedSide(hand, _userSide, _isHeadMount) )
                {
                    _screentap_gesture = new ScreenTapGesture(gesture);

                    this.GetDirection();
                    this.AnyHand();
                    this.GetPointable();
                    this.GetPosition();

                    this._isChecked = true;
                    break;
                }
            }

            if (this._isChecked)
            { break; }
        }

        if(this._isChecked)
        {
            DoAction();
        }
    }

    public virtual void UnCheck()
    {
        _isChecked = false;
    }

    public void SetMount()
    {
        if (mt == MountType.HeadMount)
        {
            _isHeadMount = true;
        }
        else
        {
            _isHeadMount = false;
        }
    }

    public bool AnyHand()
    {
        if(_screentap_gesture.IsValid)
        {
            if (_screentap_gesture.Hands.Rightmost.IsRight)
            {
                this._isRight = true;
            }
            else if (_screentap_gesture.Hands.Leftmost.IsLeft)
            {
                this._isRight = false;
            }
            return this._isRight;
        }
        else
        {
            print("This object is not valid");
            return false;
        }
        
    }

    protected virtual void DoAction()
    {
        print("Please code this method");
    }

    protected Vector GetDirection()
    {
        if(_screentap_gesture != null)
        {
            Vector tempDirection = _screentap_gesture.Direction;
            float x = Mathf.Abs(tempDirection.x);
            float y = Mathf.Abs(tempDirection.y);
            float z = Mathf.Abs(tempDirection.z);
            if (x > y && x > z)
            {
                if (tempDirection.x > 0) this._direction = new Vector(1, 0, 0);
                else if (tempDirection.x < 0) this._direction = new Vector(-1, 0, 0);
            }
            else if (y > x && y > z)
            {
                if (tempDirection.x > 0) this._direction = new Vector(0, 1, 0);
                else if (tempDirection.x < 0) this._direction = new Vector(0, -1, 0);
            }
            else if (z > x && z > y)
            {
                if (tempDirection.x > 0) this._direction = new Vector(0, 0, 1);
                else if (tempDirection.x < 0) this._direction = new Vector(0, 0, -1);
            }

            return this._direction;
        }
        else
        {
            return null;
        }
        
    }

    protected Vector GetPosition()
    {
        if(_screentap_gesture != null)
        {
            this._position = _screentap_gesture.Position;
            return this._position;
        }
        else
        {
            return null;
        }
    }

    protected Pointable GetPointable()
    {
        if(_screentap_gesture != null)
        {
            this._pointable = _screentap_gesture.Pointable;
            return this._pointable;
        }
        else
        {
            return null;
        }
        
    }

    protected HandList GetHandList()
    {
        if(_screentap_gesture != null)
        {
            return Hands;
        }
        else
        {
            return new HandList();
        }
    }

    protected FingerList GetFingerList()
    {
        if(_screentap_gesture != null)
        {
            return _fingers;
        }
        else
        {
            return new FingerList();
        }
    }
}

