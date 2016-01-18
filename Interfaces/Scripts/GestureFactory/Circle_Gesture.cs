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
    protected CircleGesture _circle_gesture = null;

    
    protected GestureList _gestures;
    protected FingerList _fingers;
    protected float _startProgress;
    protected float _endProgress;
    protected Gesture.GestureState _state;

    //-------------------------------------------------

    protected int _useDirection = 0;
    protected float _minProgress;

    //------------------------------------------------
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

    public bool _isVR
    { get; set; }

    public virtual void Update()
    {
        if(!_isChecked)
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

        this._isVR = false;
        this._userSide = 0;
        this._maxArc = 100000;
        this._maxRadius = 100000;
        this._state = Gesture.GestureState.STATE_INVALID;
        this._isChecked = false;
        this._isClockwise = -1;
        this._isRight = false;
        this._isPlaying = false;

        return true;
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

        foreach(Hand hand in Hands)
        {
            foreach (Gesture gesture in _gestures)
            {
                if (gesture.Type == Gesture.GestureType.TYPE_CIRCLE)
                {
                    print("Circle Gesture");

                    _circle_gesture = new CircleGesture(gesture);
                    this.AnyHand();
                    if (!_isPlaying && (gesture.State == Gesture.GestureState.STATE_START) && WhichSide(hand))
                    {
                        _isPlaying = !_isPlaying;
                        this._startProgress = _circle_gesture.Progress;
                        //print("start");
                        print("start progress : " + this._startProgress);
                    }

                    if (_isPlaying && gesture.State == Gesture.GestureState.STATE_STOP)
                    {
                        int direc = this.IsClockWise();
                        this._endProgress = _circle_gesture.Progress;
                        print("stop progress : " + this._endProgress);
                        if (this._endProgress >= this._minProgress && direc == _useDirection)
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
                print("update progress : " + this._endProgress);
                if (this._endProgress >= this._minProgress && direc == _useDirection)
                {
                    this._isChecked = true;
                    this._isPlaying = !this._isPlaying;
                }


            }


            if (_isChecked)
            {
                DoAction();
            }
        }
        
       // return isChecked;
    }

    protected virtual void DoAction()
    {
        print("핸들러 만들어라 이 자식아");
    }

    public virtual void UnCheck()
    {
        _isChecked = false;
    }

    //사용자가 원하는 구역에 제스처가 잡혔는지를 검사.
    //all:0, left:1, right:2, up:3, down:4.
    public bool WhichSide(Hand hand)
    {
        if (!_isVR)
        {
            Vector position = hand.PalmPosition;
            Vector3 unityPosition = position.ToUnity();
            Vector3 toPos = new Vector3(((unityPosition.x + 150.0f) / 300.0f), (unityPosition.y / 300.0f), ((unityPosition.z + 150.0f) / 300.0f));
            Vector3 pos = Camera.main.ViewportToWorldPoint(toPos);
            Vector3 tempos = Camera.main.WorldToViewportPoint(pos);

            switch (_userSide)
            {
                case 0:
                    return true;
                case 1:
                    if (tempos.x < 0.5 && tempos.x >= 0 && tempos.y >= 0 && tempos.y <= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if (tempos.x > 0.5 && tempos.x <= 1 && tempos.y >= 0 && tempos.y <= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if (tempos.y > 0.5 && tempos.y <= 1 && tempos.x >= 0 && tempos.x <= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 4:
                    if (tempos.y < 0.5 && tempos.y >= 0 && tempos.x >= 0 && tempos.x <= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
        // left, right : x, up,down : z
        else
        {
            Vector position = hand.PalmPosition;
            Vector3 unityPosition = position.ToUnity();
            Vector3 toPos = new Vector3(1 - ((unityPosition.x + 150.0f) / 300.0f), ((unityPosition.z + 150.0f) / 300.0f), (unityPosition.y / 300.0f));
            Vector3 pos = Camera.main.ViewportToWorldPoint(toPos);
            Vector3 tempos = Camera.main.WorldToViewportPoint(pos);

            switch (_userSide)
            {
                case 0:
                    return true;
                case 1:
                    if (tempos.x < 0.5 && tempos.x >= 0 && tempos.y >= 0 && tempos.y <= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if (tempos.x > 0.5 && tempos.x <= 1 && tempos.y >= 0 && tempos.y <= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if (tempos.y > 0.5 && tempos.y <= 1 && tempos.x >= 0 && tempos.x <= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 4:
                    if (tempos.y < 0.5 && tempos.y >= 0 && tempos.x >= 0 && tempos.x <= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }


    }

    public void OnVR()
    {
        _isVR = true;
    }
    protected virtual bool SetGestureCondition(int direction, float progress)
    {
        this._useDirection = direction;
        this.SetMinProgress(progress);

        return true;
    }

    //1이면 오른손 0이면 왼손
    public bool AnyHand()
    {

        if (_circle_gesture.IsValid)
        {
            if (_circle_gesture.Hands.Rightmost.IsRight)
            {
                this._isRight = true;
            }
            else if (_circle_gesture.Hands.Leftmost.IsLeft)
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

    protected bool SetMinRadius(float radius)
    {
        this._minRadius = radius;
        return true;
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

    protected  Vector GetNormal()
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

