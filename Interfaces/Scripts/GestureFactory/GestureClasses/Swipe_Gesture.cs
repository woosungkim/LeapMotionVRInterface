using UnityEngine;
using System.Collections;
using Leap;

public class Swipe_Gesture : MonoBehaviour, IMultiStepCheckGesture
{
    [HideInInspector]
    public Hand tempHand;

    public Vector _direction;
    public SwipeGesture _swipe_gestrue;
    public Vector _position;
    public Pointable _pointable;
    protected GestureList _gestures;
    protected FingerList _fingers;
    protected Vector _startPoint;
    protected Vector _endPoint;
    
    public Gesture.GestureState _state
    { get; set; }

    public MountType _mountType
    { get; set; }

    public GestureType _gestureType
    { get; set; }

    public UseArea _useArea
    { get; set; }

    public HandList Hands
    { get; set; }

    public UsingHand _usingHand
    { get; set; }

    public Controller _leap_controller
    { get; set; }

    public Frame _lastFrame
    { get; set; }

    public bool _isChecked
    { get; set; }

    public bool _isPlaying
    { get; set; }
    
    //----------------------------------------------------------
    // User option value for 'SetGestureCondition()'
    [HideInInspector]
    public int _useDirection = 0;
    [HideInInspector]
    public char UseAxis;
    protected float _minX  = 50;
    protected float _maxX = 130;
    protected float _minY = 50;
    protected float _maxY = 130;
    protected float _minZ = 50;
    protected float _maxZ = 130;
    
    
    [Range(0,50)]
    public int Sensitivity = 0;
    public MountType MountType;
    public UsingHand UsingHand;
    public SwipeDirection _swipeDirection;
    public UseArea UseArea;
    
    //----------------------------------------------------------

    void Start()
    {
        this.SetGestureCondition( this._swipeDirection, this.Sensitivity, this.UseArea );
    }

    void Update()
    {
        if (!_isChecked)
        {
            CheckGesture();
        }
        UnCheck();
    }


    //-----------------------------------------------------
    // Function for check gesture occur every frame.
    public virtual void CheckGesture()
    {
        _lastFrame = _leap_controller.Frame();
        Hands = _lastFrame.Hands;
        _gestures = _lastFrame.Gestures();


        foreach(Hand hand in Hands)
        {
            tempHand = hand;
            _fingers = hand.Fingers;
            
            if (WhichSide.IsEnableGestureHand(this))
            {
                foreach (Gesture gesture in _gestures)
                {

                    if (gesture.Type == Gesture.GestureType.TYPE_SWIPE)
                    {
                        _swipe_gestrue = new SwipeGesture(gesture);

                        if (!_isPlaying && gesture.State == Gesture.GestureState.STATE_START && WhichSide.capturedSide(hand, _useArea, this._mountType))
                        {
                            _isPlaying = true;
                            _startPoint = hand.PalmPosition;
                        }

                        if (_isPlaying && gesture.State == Gesture.GestureState.STATE_STOP)
                        {
                            _endPoint = hand.PalmPosition;
                            switch (UseAxis)
                            {
                                case 'x':
                                    if (((_endPoint.x - _startPoint.x) * _useDirection) > Sensitivity)
                                    {
                                        this._isChecked = true;
                                        _isPlaying = !_isPlaying;
                                        break;
                                    }
                                    _state = gesture.State;
                                    break;
                                case 'y':
                                    if (((_endPoint.y - _startPoint.y) * _useDirection) > Sensitivity)
                                    {

                                        this._isChecked = true;
                                        _isPlaying = !_isPlaying;
                                        break;
                                    }
                                    _state = gesture.State;
                                    break;
                                case 'z':
                                    if (_startPoint.y < this._maxY && _endPoint.y < this._maxY &&
                                        ((_endPoint.z - _startPoint.z) * _useDirection) > Sensitivity)
                                    {
                                        this._isChecked = true;
                                        _isPlaying = !_isPlaying;
                                        _state = gesture.State;
                                        break;
                                    }
                                    _state = gesture.State;
                                    break;
                                default:
                                    break;
                            }
                        }

                        if (!this._isPlaying || this._isChecked)
                        {
                            break;
                        }

                        _state = gesture.State;
                    }

                }

                if (_isPlaying && _state == Gesture.GestureState.STATE_UPDATE && WhichSide.capturedSide(hand, _useArea, this.MountType))
                {
                    _endPoint = hand.PalmPosition;
                    switch (UseAxis)
                    {
                        case 'x':
                            if (((_endPoint.x - _startPoint.x) * _useDirection) > Sensitivity)
                            {

                                this._isChecked = true;
                                _isPlaying = !_isPlaying;
                                break;
                            }
                            break;
                        case 'y':
                            if (((_endPoint.y - _startPoint.y) * _useDirection) > Sensitivity)
                            {

                                this._isChecked = true;
                                _isPlaying = !_isPlaying;
                                break;
                            }
                            break;
                        case 'z':
                            if (_startPoint.y < this._maxY && _endPoint.y < this._maxY &&
                                ((_endPoint.z - _startPoint.z) * _useDirection) > Sensitivity)
                            {
                                this._isChecked = true;
                                _isPlaying = !_isPlaying;
                                break;
                            }
                            else
                            {
                                _isPlaying = !_isPlaying;
                            }
                            break;
                        default:
                            break;
                    }

                }


            }
        }
       

        // -------------------- 
        // If Gesture is captured, call the handler function 'DoAction()'
        if (_isChecked)
        {
            DoAction();
        }
        //---------------------
    }
    //----------------------------------------------------------------------


    //----------------------------------------------------------------------
    //Handler function of gesture.
    public virtual void DoAction()
    {
        print("Please override this method");
    }
    //----------------------------------------------------------------------


    //----------------------------------------------------------------------
    // Function of unchecking _isChecked flag.
    public virtual void UnCheck()
    {
        _isChecked = false;
    }
    //-----------------------------------------------------------------------

    protected void SetGestureCondition(SwipeDirection swipeDirection, int sensitivity, UseArea useArea)
    {
        _gestureType = GestureType.swipe;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this, swipeDirection, sensitivity, useArea, UsingHand);
    }
    
    protected Vector GetDirection()
    {
        return PropertyGetter.GetDirection(this);
    }

    protected HandList GetHandList()
    {
        if(_swipe_gestrue != null)
        { return Hands; }
        else{return new HandList();}
    }

    protected FingerList GetFingerList()
    {
        if(_swipe_gestrue != null)
        {return _fingers;}
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


