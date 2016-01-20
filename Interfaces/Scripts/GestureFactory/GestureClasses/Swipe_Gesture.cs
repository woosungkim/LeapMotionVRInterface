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
    public Gesture.GestureState _state
    { get; set; }

    //----------------------------------------------------------
    // User option for 'SetGestureCondition()'
    public int _useDirection = 0;
    protected float _minX  = 50;
    protected float _maxX = 130;
    protected float _minY = 50;
    protected float _maxY = 130;
    protected float _minZ = 50;
    protected float _maxZ = 130;
    public char _useAxis;
    public int _sensitivity = 0;
    //----------------------------------------------------------


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

        foreach (Hand hand in Hands)
        {
            foreach (Gesture gesture in _gestures)
            {

                if (gesture.Type == Gesture.GestureType.TYPE_SWIPE)
                {

                    //print("Swipe Gesture");
                    _swipe_gestrue = new SwipeGesture(gesture);
                    AnyHand();
                 
                    if (!_isPlaying && gesture.State == Gesture.GestureState.STATE_START && WhichSide.capturedSide(hand, _useArea, _isHeadMount))
                    {
                        _isPlaying = true;
                        _startPoint = hand.PalmPosition;
                        //print("start");
                    }

                    if (_isPlaying && gesture.State == Gesture.GestureState.STATE_STOP)
                    {
                        _endPoint = hand.PalmPosition;
                        print(this.GetDirection());
                        //print("stop");

                        switch (_useAxis)
                        {
                            case 'x':
                                if (/*_startPoint.y < this._maxY && _endPoint.y < this._maxY &&*/
                                     ((_endPoint.x - _startPoint.x) * _useDirection) > _sensitivity)
                                {

                                    this._isChecked = true;
                                    _isPlaying = !_isPlaying;
                                    break;
                                }
                                _state = gesture.State;
                                break;
                            case 'y':
                                if (((_endPoint.y - _startPoint.y) * _useDirection) > _sensitivity)
                                {

                                    this._isChecked = true;
                                    _isPlaying = !_isPlaying;
                                    break;
                                }
                                _state = gesture.State;
                                break;
                            case 'z':
                                if (_startPoint.y < this._maxY && _endPoint.y < this._maxY &&
                                    ((_endPoint.z - _startPoint.z)*_useDirection) > _sensitivity)
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

            if (_isPlaying && _state == Gesture.GestureState.STATE_UPDATE && WhichSide.capturedSide(hand, _useArea, _isHeadMount))
            {
                print("update");
                _endPoint = hand.PalmPosition;
                print(this.GetDirection());
                switch (_useAxis)
                {
                    case 'x':
                        if (/*_startPoint.y < this._maxY && _endPoint.y < this._maxY &&*/
                             ((_endPoint.x - _startPoint.x) * _useDirection) > _sensitivity)
                        {

                            this._isChecked = true;
                            _isPlaying = !_isPlaying;
                            break;
                        }
                        break;
                    case 'y':
                        if (((_endPoint.y - _startPoint.y) * _useDirection) > _sensitivity)
                        {

                            this._isChecked = true;
                            _isPlaying = !_isPlaying;
                            break;
                        }
                        break;
                    case 'z':
                        if (_startPoint.y < this._maxY && _endPoint.y < this._maxY &&
                            ((_endPoint.z - _startPoint.z) * _useDirection) > _sensitivity)
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
    protected virtual void DoAction()
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
    public bool AnyHand()
    {
        if(PropertyGetter.AnyHand(this))
        { return _isRight; }
        else
        { print("AnyHand() : This object is not valid");
          return false;
        }
    }
    //---------------------------------------------------------------------

    protected void SetGestureCondition(char axis, int direction)
    {
        _gestureType = GestureType.swipe;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this, axis, direction);
    }

    protected void SetGestureCondition(char axis, int direction, UseArea useArea)
    {
        _gestureType = GestureType.swipe;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this, axis, direction, useArea);
    }
    
    protected void SetGestureCondition(char axis, int direction, int sensitivity, UseArea useArea)
    {
        _gestureType = GestureType.swipe;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this, axis, direction, sensitivity, useArea);
    }
    

    
    protected virtual Vector GetDirection()
    {
       
        print( PropertyGetter.GetDirection(this));
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


