using UnityEngine;
using System.Collections;
using Leap;


public class Circle_Gesture : MonoBehaviour,IGesture 
{
    protected float _minRadius = 5.0f;
    protected float _maxRadius;

    protected float _minArc = 4.71f;
    protected float _maxArc;

    protected int _isClockwise;
    protected Vector _normal;

    protected float _progress;
    
    protected Pointable _pointable;
    protected float _radius;
    protected CircleGesture _circle_gesture;

    protected HandList _hands;
    protected GestureList _gestures;
    protected FingerList _fingers;
    protected float _startProgress;
    protected float _endProgress;
    protected Gesture.GestureState _state;

    //-------------------------------------------------

    protected int _useDirection = 0;
    protected float _minProgress;

    //------------------------------------------------

    public bool isRight
    { get; set; }

    public Controller _leap_controller
    { get; set; }

    public Frame lastFrame
    { get; set; }

    public bool isChecked
    { get; set; }

    public bool isPlaying
    { get; set; }

    public virtual void Update()
    {
        if(!isChecked)
        {
            CheckGesture();
        }
        UnCheck();
    }

    public bool SetConfig()
    {
        _leap_controller = new Controller();
        _leap_controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
        _leap_controller.Config.SetFloat("Gesture.Circle.MinRadius", this._minRadius);
        _leap_controller.Config.SetFloat("Gesture.Circle.MinArc", this._minArc);
        _leap_controller.Config.Save();

        this._maxArc = 100000;
        this._maxRadius = 100000;
        this._state = Gesture.GestureState.STATE_INVALID;
        this.isChecked = false;
        this._isClockwise = -1;
        this.isRight = false;
        this.isPlaying = false;

        return true;
    }

    protected virtual Pointable GetPointable()
    {
        _pointable = _circle_gesture.Pointable;
        return _pointable;
    }

    protected virtual float GetProgress()
    {
        _progress = _circle_gesture.Progress;
        return _progress;
    }
    protected virtual Vector GetNormal()
    {
        return _circle_gesture.Normal;
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

    protected bool SetMinRadius(float radius)
    {
        this._minRadius = radius;
        return true;
    }

    public virtual void CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        _hands = lastFrame.Hands;
        _gestures = lastFrame.Gestures();

        foreach( Gesture gesture in _gestures)
        {
            int id = gesture.Id;

            if( gesture.Type == Gesture.GestureType.TYPE_CIRCLE )
            {
                print("Circle Gesture");
                
                _circle_gesture = new CircleGesture(gesture);
                this.AnyHand();
                if(!isPlaying && gesture.State == Gesture.GestureState.STATE_START)
                {
                    isPlaying = !isPlaying;
                    this._startProgress = _circle_gesture.Progress;
                    //print("start");
                    print("start progress : " + this._startProgress);
                }

                if(isPlaying && gesture.State == Gesture.GestureState.STATE_STOP)
                {
                    int direc = this.IsClockWise();
                    this._endProgress = _circle_gesture.Progress;
                    print("stop progress : " + this._endProgress);
                    if( this._endProgress  >= this._minProgress && direc == _useDirection)
                    {
                        this.isChecked = true;
                        this.isPlaying = !this.isPlaying;
                    }
                    this._state = gesture.State;
                    
                    break;
                }
                this._state = gesture.State;
                
            }
        }

        if(isPlaying && _state == Gesture.GestureState.STATE_UPDATE)
        {
            int direc = this.IsClockWise();
            this._endProgress = _circle_gesture.Progress;
            print("update progress : " + this._endProgress);
            if ( this._endProgress >= this._minProgress && direc == _useDirection)
            {
                this.isChecked = true;
                this.isPlaying = !this.isPlaying;
            }
                    
        }
        

        if(isChecked)
        {
            DoAction();
        }
       // return isChecked;
    }

    protected virtual void DoAction()
    {
        print("핸들러 만들어라 이 자식아");
    }

    public virtual void UnCheck()
    {
        isChecked = false;
    }

    protected virtual bool SetGestureCondition(int direction, float progress)
    {
        this._useDirection = direction;
        this.SetMinProgress(progress);

        return true;
    }

    //1이면 오른손 0이면 왼손
    public int AnyHand()
    {

        if (_circle_gesture.IsValid)
        {
            if (_circle_gesture.Hands.Rightmost.IsRight)
            {
                this.isRight = true;
            }
            else if (_circle_gesture.Hands.Leftmost.IsLeft)
            {
                this.isRight = false;
            }
            return 1;
        }
        else
        {
            return 0;
        }
        
    }

    protected bool SetMinProgress(float times)
    {
        this._minProgress = times;
        return true;
    }

    protected bool SetMaxRadius(float radius)
    {
        this._maxRadius = radius;
        return true;
    }

    protected bool SetMaxArc(float arc)
    {
        this._maxArc = arc;
        return true;
    }
}

