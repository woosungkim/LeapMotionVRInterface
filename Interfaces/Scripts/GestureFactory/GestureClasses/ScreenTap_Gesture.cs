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

    public MountType MountType;
    public UseArea UseArea;
    public UsingHand UsingHand;

    public UsingHand _usingHand
    { get; set; }

    public MountType _mountType
    { get; set; }

    public GestureType _gestureType
    { get; set; }

    public UseArea _useArea
    { get; set; }

    public Controller _leap_controller
    { get; set; }

    public HandList Hands
    { get; set; }

    public Frame _lastFrame
    { get; set; }

    public bool _isChecked
    { get; set; }

    public virtual void Start()
    {
        this.SetGestureCondition();
    }

    public virtual void Update()
    {
        if(!this._isChecked)
        {
            CheckGesture();
        }
        UnCheck();
    }

    public virtual void CheckGesture()
    {
        _lastFrame = _leap_controller.Frame(0);
        Hands = _lastFrame.Hands;
        _gestures = _lastFrame.Gestures();

        if((!this._isChecked) && IsEnableGestureHand())
        {
            foreach (Hand hand in Hands)
            {
                this._fingers = hand.Fingers;

                foreach (Gesture gesture in _gestures)
                {
                    if ((gesture.Type == Gesture.GestureType.TYPE_SCREEN_TAP) && WhichSide.capturedSide(hand, _useArea, this._mountType ))
                    {
                        _screentap_gesture = new ScreenTapGesture(gesture);

                        this.GetDirection();
                        this.IsEnableGestureHand();
                        this.GetPointable();
                        this.GetPosition();

                        this._isChecked = true;
                        break;
                    }
                }

                if (this._isChecked)
                { break; }
            }
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

    public bool IsEnableGestureHand()
    {
        return PropertyGetter.IsEnableGestureHand(this);
    }

    protected void SetGestureCondition()
    {
        _gestureType = GestureType.screentab;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this, MountType, UseArea, UsingHand);
    }

    public virtual void DoAction()
    {
        print("Please code this method");
    }


    protected virtual Vector GetDirection()
    {

        print(PropertyGetter.GetDirection(this));
        return this._direction;
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

