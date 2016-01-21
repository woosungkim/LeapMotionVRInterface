using UnityEngine;
using System.Collections;
using Leap;

public class Swipe_Gesture : MonoBehaviour, IMultiStepCheckGesture
{
    public Vector _direction;
    public SwipeGesture _swipe_gestrue;

    protected GestureList _gestures;
    protected FingerList _fingers;

    protected Vector _startPoint;
    protected Vector _endPoint;
    

    //----------------------------------------------------------
    // User option for 'SetGestureCondition()'
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
    { get; set;}

    public Controller _leap_controller
    { get; set; }

    public Frame _lastFrame
    { get; set; }

    public bool _isChecked
    { get; set; }

    public bool _isPlaying
    { get; set; }


    void Start()
    {
        this.SetGestureCondition( this._swipeDirection, this.Sensitivity, this.UseArea );
    }

    void Update()
    {
        print(this._useArea);
        print(this.UseAxis);
        print(this._swipeDirection);
        print(this.Sensitivity);
        print(this._usingHand);
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

        if(IsEnableGestureHand())
        {
            print(IsEnableGestureHand());
            print(this._useDirection);
            foreach (Hand hand in Hands)
            {
                foreach (Gesture gesture in _gestures)
                {

                    if (gesture.Type == Gesture.GestureType.TYPE_SWIPE)
                    {

                        //print("Swipe Gesture");
                        _swipe_gestrue = new SwipeGesture(gesture);
                        IsEnableGestureHand();

                        if (!_isPlaying && gesture.State == Gesture.GestureState.STATE_START && WhichSide.capturedSide(hand, _useArea, this._mountType))
                        {
                            _isPlaying = true;
                            _startPoint = hand.PalmPosition;
                            print("start point1 : " + _startPoint);
                        }

                        if (_isPlaying && gesture.State == Gesture.GestureState.STATE_STOP)
                        {
                            _endPoint = hand.PalmPosition;
                           // print(this.GetDirection());
                            print("end point1 : " + _endPoint);
                            switch (UseAxis)
                            {
                                case 'x':
                                    if (/*_startPoint.y < this._maxY && _endPoint.y < this._maxY &&*/
                                         ((_endPoint.x - _startPoint.x) * _useDirection) > Sensitivity)
                                    {
                                        print("end point2 : " + _endPoint);
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
                                        print((_endPoint.z * this._useDirection) - (_startPoint.z * this._useDirection));
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
                    print("update");
                    _endPoint = hand.PalmPosition;
                    //print(this.GetDirection());
                    switch (UseAxis)
                    {
                        case 'x':
                            if (/*_startPoint.y < this._maxY && _endPoint.y < this._maxY &&*/
                                 ((_endPoint.x - _startPoint.x) * _useDirection) > Sensitivity)
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
                                print((_endPoint.z * this._useDirection) - (_startPoint.z * this._useDirection));
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

                if (!this._isPlaying || this._isChecked)
                {
                    break;
                }

            }
        }
        
        // -------------------- 
        // If Gesture is captured, call the handler function 'DoAction()'
        if(_isChecked)
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


    //---------------------------------------------------------------------
    // Function of getting hand which side user used.
    // '1' is right hand, '0' is left hand gesture.
    // Default value is 0.
    public bool IsEnableGestureHand()
    {
        return PropertyGetter.IsEnableGestureHand(this);
    }
    //---------------------------------------------------------------------

    protected void SetGestureCondition(SwipeDirection swipeDirection, int sensitivity, UseArea useArea)
    {
        _gestureType = GestureType.swipe;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this, swipeDirection, sensitivity, useArea, UsingHand);
    }
    
    protected virtual Vector GetDirection()
    {
       
        //print( PropertyGetter.GetDirection(this));
        return this._direction;
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
}


