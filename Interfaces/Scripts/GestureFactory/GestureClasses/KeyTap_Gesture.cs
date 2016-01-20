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

    public MountType _mountType
    { get; set; }

    public GestureType _gestureType
    { get; set; }

    public UseArea _useArea
    { get; set; }

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

    public bool _isHeadMount
    { get; set; }

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
        this._lastFrame = this._leap_controller.Frame(0);
        this.Hands = this._lastFrame.Hands;
        this._gestures = this._lastFrame.Gestures();
        
        if(!this._isChecked)
        {
            foreach (Hand hand in Hands)
            {
                this._fingers = hand.Fingers;

                foreach (Gesture gesture in _gestures)
                {

                    if ((gesture.Type == Gesture.GestureType.TYPE_KEY_TAP) && WhichSide.capturedSide(hand, _useArea, _isHeadMount))
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

                if (this._isChecked)
                { break; }

            }
        }
        else if(this._isChecked)
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

    protected void SetGestureCondition()
    {
        _gestureType = GestureType.keytab;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this);
    }

    protected void SetGestureCondition(UseArea useArea)
    {
        _gestureType = GestureType.keytab;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this, useArea);
    }

    public bool AnyHand()
    {
        if (PropertyGetter.AnyHand(this))
        { return _isRight; }
        else
        {
            print("AnyHand() : This object is not valid");
            return false;
        }
    }

    protected virtual Vector GetDirection()
    {

        print(PropertyGetter.GetDirection(this));
        return this._direction;
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


