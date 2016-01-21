using UnityEngine;
using System.Collections;
using Leap;


public class Circle_Gesture : MonoBehaviour, IMultiStepCheckGesture 
{
    [HideInInspector]
    public int _isClockwise;
    [HideInInspector]
    public float _progress;
    [HideInInspector]
    public int _useDirection = 0;
    protected Vector _normal;
    
    protected Pointable _pointable;
    protected float _radius;
    public CircleGesture _circle_gesture = null;

    
    protected GestureList _gestures;
    protected FingerList _fingers;
    protected float _startProgress;
    protected float _endProgress;
    

    //-------------------------------------------------

    public CircleDirection CircleDirection;
    [Range(0,2)]
    public float MinProgress;
    public MountType MountType;
    public UsingHand UsingHand;
    public UseArea UseArea;
    //------------------------------------------------

    public Gesture.GestureState _state
    { get; set; }

    public MountType _mountType
    { get; set; }

    public UseArea _useArea
    { get; set; }

    public GestureType _gestureType
    { get; set; }

    public UsingHand _usingHand
    { get; set; }

    public HandList Hands
    { get; set; }

    public Controller _leap_controller
    { get; set; }

    public Frame _lastFrame
    { get; set; }

    public bool _isChecked
    { get; set; }

    public bool _isPlaying
    { get; set; }

    public virtual void Start()
    {
        this.SetGestureCondition();
    }

    public virtual void Update()
    {
        if(!_isChecked)
        {
            CheckGesture();
        }
        UnCheck();
    }


    protected int IsClockWise()
    {
        if(_circle_gesture.Pointable.Direction.AngleTo(this.GetNormal()) <= 3.14/2)
        {
            _isClockwise = 1;
        }
        else
        {
            _isClockwise = -1;
        }

        return _isClockwise;
    }

    public virtual void CheckGesture()
    {
        _lastFrame = _leap_controller.Frame(0);
        Hands = _lastFrame.Hands;
        _gestures = _lastFrame.Gestures();

        if(IsEnableGestureHand())
        {
            foreach (Hand hand in Hands)
            {
                foreach (Gesture gesture in _gestures)
                {
                    _fingers = hand.Fingers;
                    if (gesture.Type == Gesture.GestureType.TYPE_CIRCLE)
                    {
                        print("Circle Gesture");

                        _circle_gesture = new CircleGesture(gesture);
                        this.IsEnableGestureHand();
                        if (!_isPlaying && (gesture.State == Gesture.GestureState.STATE_START) && WhichSide.capturedSide(hand, _useArea, _mountType))
                        {
                            _isPlaying = !_isPlaying;
                            this._startProgress = _circle_gesture.Progress;
                        }

                        if (_isPlaying && gesture.State == Gesture.GestureState.STATE_STOP)
                        {
                            int direc = PropertyGetter.IsClockWise(this);
                            this._endProgress = _circle_gesture.Progress;
                            //print("stop progress : " + this._endProgress);
                            if (this._endProgress >= this.MinProgress && direc == _useDirection)
                            {
                                this._isChecked = true;
                                this._isPlaying = !this._isPlaying;
                            }
                            this._state = gesture.State;

                            break;
                        }
                        this._state = gesture.State;

                    }
                }

                if (_isPlaying && _state == Gesture.GestureState.STATE_UPDATE)
                {
                    int direc = this.IsClockWise();
                    this._endProgress = _circle_gesture.Progress;
                  //  print("update progress : " + this._endProgress);
                    if (this._endProgress >= this.MinProgress && direc == _useDirection)
                    {
                        this._isChecked = true;
                        this._isPlaying = !this._isPlaying;
                    }
                }

                if (!this._isPlaying || this._isChecked)
                {
                    break;
                }

            }
        }
        
        if (_isChecked)
        {
            print(this.MinProgress);
            DoAction();
        }
       
    }

    public virtual void DoAction()
    {
        print("핸들러 만들어라 이 자식아");
    }

    public virtual void UnCheck()
    {
        _isChecked = false;
    }

    protected void SetGestureCondition()
    {
        _gestureType = GestureType.circle;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this, MountType, CircleDirection, MinProgress, UseArea, UsingHand);      
    }

 
    public bool IsEnableGestureHand()
    {
        if (PropertyGetter.IsEnableGestureHand(this))
        { return true; }
        else
        { return false; }
    }

    protected bool SetMinProgress(float times)
    {
        this.MinProgress = times;
        return true;
    }

    protected Pointable GetPointable()
    {
        if(_circle_gesture != null)
        {
            _pointable = _circle_gesture.Pointable;
            return _pointable;
        }
        else
        {
            return null;
        }
        
    }

    protected float GetProgress()
    {
        if(_circle_gesture != null)
        {
            _progress = _circle_gesture.Progress;
            return _progress;
        }
        else
        {
            return -1;
        }
        
    }

    public  Vector GetNormal()
    {
        if(_circle_gesture != null)
        {
            this._normal = _circle_gesture.Normal;
            return _normal;
        }
        else
        {
            return null;
        }
    }

    protected HandList GetHandList()
    {
        if(_circle_gesture != null)
        {
            this.Hands = _circle_gesture.Hands;
            return Hands;
        }
        else
        {
            return new HandList();
        }
        
    }

    protected FingerList GetFingerList()
    {
        if(_circle_gesture != null)
        {
            
            return _fingers;
        }
        else
        {
            return new FingerList();
        }
    }
}

