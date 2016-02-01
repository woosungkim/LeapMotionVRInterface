using UnityEngine;
using System.Collections;
using Leap;

public class KeyTap_Gesture : MonoBehaviour, ISingleStepCheckGesture 
{
    [HideInInspector]
    public Hand tempHand;

    public KeyTapGesture _keytab_gesture;

    public Vector _direction;
    public Pointable _pointable;
    public Vector _position;
    protected float _progress;
    protected GestureList _gestures;
    protected FingerList _fingers;
    public MountType MountType;
    public UsingHand UsingHand;
    public UseArea UseArea;


    public UsingHand _usingHand
    { get; set; }

    public MountType _mountType
    { get; set; }

    public GestureType _gestureType
    { get; set; }

    public UseArea _useArea
    { get; set; }

    public HandList Hands
    { get; set; }

    public Controller _leap_controller
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
        this._lastFrame = this._leap_controller.Frame(0);
        this.Hands = this._lastFrame.Hands;
        this._gestures = this._lastFrame.Gestures();

        foreach(Hand hand in Hands)
        {
            tempHand = hand;
            _fingers = hand.Fingers;

            if (WhichSide.IsEnableGestureHand(this))
            {
                this._fingers = hand.Fingers;

                foreach (Gesture gesture in _gestures)
                {

                    if ((gesture.Type == Gesture.GestureType.TYPE_KEY_TAP) && WhichSide.capturedSide(hand, _useArea, _mountType))
                    {
                        _keytab_gesture = new KeyTapGesture(gesture);
                        this.GetDirection();
                        this.GetPointable();
                        this.GestureInvokePosition();

                        this._isChecked = true;
                        break;

                    }
                }
            }

            if (this._isChecked)
                break;
        }
       
        if (this._isChecked)
        {
            DoAction();
        }
    }

    public virtual void UnCheck()
    {
        this._isChecked = false;
    }

    public virtual void DoAction()
    {
        print("Please override this method");
    }

    protected void SetGestureCondition()
    {
        _gestureType = GestureType.keytab;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this, MountType, UseArea, UsingHand);
    }

    protected virtual Vector GetDirection()
    {
        return PropertyGetter.GetDirection(this);
    }

    protected HandList GetHandList()
    {
        if(_keytab_gesture != null)
        { return Hands; }
        else{ return new HandList(); }
    }

    protected FingerList GetFingerList()
    {
        if(_keytab_gesture != null)
        { return _fingers; }
        else{ return new FingerList(); }
    }

    protected Pointable GetPointable()
    {
        return PropertyGetter.GetPointable(this);
    }

    protected Vector GestureInvokePosition()
    {
        return PropertyGetter.GetGestureInvokePosition(this);
    }

}


