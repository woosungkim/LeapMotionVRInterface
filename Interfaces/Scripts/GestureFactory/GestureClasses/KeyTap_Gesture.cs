using UnityEngine;
using System.Collections;
using Leap;

public class KeyTap_Gesture : MonoBehaviour, ISingleStepCheckGesture 
{
    public KeyTapGesture _keytab_gesture;
    public Vector _direction;
    protected Pointable _pointable;
    protected Vector _position;
    protected float _progress;

    
    protected GestureList _gestures;
    protected FingerList _fingers;

    public MountType mt;

    public HandList Hands
    { get; set; }

    public bool _isRight
    { get; set; }

    public Controller _leap_controller
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
        _leap_controller = ControllerSetter.SetConfig(GestureType.keytab);

        this.SetMount();
        this._userSide = 0;
        this._isRight = false;
        this._isChecked = false;
        this._direction = Vector.Zero;

        return true;
    }

    protected virtual Vector GetDirection()
    {
        Vector tempDirection = _keytab_gesture.Direction;
        float x = Mathf.Abs(tempDirection.x);
        float y = Mathf.Abs(tempDirection.y);
        float z = Mathf.Abs(tempDirection.z);
        if(x>y && x>z)
        {
            if (tempDirection.x > 0) this._direction = new Vector(1, 0, 0);
            else if (tempDirection.x < 0) this._direction = new Vector(-1, 0, 0);
        }
        else if(y>x && y>z)
        {
            if (tempDirection.y > 0) this._direction = new Vector(0, 1, 0);
            else if (tempDirection.y < 0) this._direction = new Vector(0, -1, 0);
        }
        else if(z>x && z>y)
        {
            if (tempDirection.z > 0) this._direction = new Vector(0, 0, 1);
            else if (tempDirection.z < 0) this._direction = new Vector(0, 0, -1);
        }

        return this._direction;
    }

    public virtual void CheckGesture()
    {
        this._lastFrame = this._leap_controller.Frame(0);
        this.Hands = this._lastFrame.Hands;
        this._gestures = this._lastFrame.Gestures();
        
        foreach(Hand hand in Hands)
        {
            this._fingers = hand.Fingers;

            foreach(Gesture gesture in _gestures)
            {
                if ((gesture.Type == Gesture.GestureType.TYPE_KEY_TAP) && WhichSide.capturedSide(hand, _userSide, _isHeadMount))
                {
                    _keytab_gesture = new KeyTapGesture(gesture);

                    this.GetDirection();
                    this.AnyHand();
                    this.GetPointable();
                    this.GetPosition();
                    this._isChecked = true;

                    break;
                }
            }

            if(this._isChecked)
            { break; }

        }
       
        if(this._isChecked)
        {
            DoAction();
        }
    }

    public virtual void UnCheck()
    {
        this._isChecked = false;
    }

    protected virtual void DoAction()
    {
        print("Please code this method");
    }

    public bool AnyHand()
    {
        if(_keytab_gesture.IsValid)
        {
            if(_keytab_gesture.Hands.Rightmost.IsRight)
            {
                this._isRight = true;
            }
            else if(_keytab_gesture.Hands.Leftmost.IsLeft)
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

    protected HandList GetHandList()
    {
        if(_keytab_gesture != null)
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
        if(_keytab_gesture != null)
        {
            return _fingers;
        }
        else
        {
            return new FingerList();
        }
        
    }

    protected Pointable GetPointable()
    {
        if(_keytab_gesture != null)
        {
            _pointable = _keytab_gesture.Pointable;
            return _pointable;
        }
        else
        {
            return null;
        }
        
    }

    protected Vector GetPosition()
    {
        if(_keytab_gesture != null)
        {
            _position = _keytab_gesture.Position;
            return _position;
        }
        else
        {
            return null;
        }
        
    }

}


