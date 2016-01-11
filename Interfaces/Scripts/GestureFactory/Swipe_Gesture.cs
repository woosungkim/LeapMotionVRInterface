using UnityEngine;
using System.Collections;
using Leap;



public class Swipe_Gesture : MonoBehaviour, IGesture
{
    protected float _minLength = 140;
    protected float _minVelocity = 1000;
    protected Vector3 _boundary;
    protected float _maxVelocity;
    protected float _maxLength;
    protected Vector _direction;
    protected SwipeGesture _swipe_gestrue;

    protected HandList _hands;
    protected GestureList _gestures;
    protected FingerList _fingers;

    protected Vector _startPoint;
    protected Vector _endPoint;
    protected Gesture.GestureState _state;

    //----------------------------------------------------------

    protected int _useDirection = 0;
    protected float _minX;
    protected float _maxX;
    protected float _minY = 50;
    protected float _maxY = 130;
    protected float _minZ;
    protected float _maxZ;
    protected char _useAxis;

    //----------------------------------------------------------

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

    void Update()
    {
        if (!isChecked)
        {
            CheckGesture();
        }
        UnCheck();
    }

    public bool SetConfig()
    {
        _leap_controller = new Controller();
        _leap_controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
        _leap_controller.Config.SetFloat("Gesture.Swipe.MinLength", this._minLength);
        _leap_controller.Config.SetFloat("Gesture.Swipe.MinVelocity", this._minVelocity);
        _leap_controller.Config.Save();

        this.isChecked = false;

        this._direction = Vector.Zero;
        this._boundary = Vector3.zero;
        this._maxVelocity = 100000;
        this._maxLength = 100000;
        this._state = Gesture.GestureState.STATE_INVALID;
        this.isRight = false;
        this.isPlaying = false;

        return true;
    }

    public virtual void CheckGesture()
    {
        lastFrame = _leap_controller.Frame(0);
        _hands = lastFrame.Hands;
        _gestures = lastFrame.Gestures();

        foreach (Hand hand in _hands)
        {
            
            FingerList fingers = hand.Fingers;

            foreach (Gesture gesture in _gestures)
            {
                int id = gesture.Id;

                if (gesture.Type == Gesture.GestureType.TYPE_SWIPE)
                {

                    print("Swipe Gesture");
                    _swipe_gestrue = new SwipeGesture(gesture);
                    AnyHand();
                    if (!isPlaying && gesture.State == Gesture.GestureState.STATE_START)
                    {
                        isPlaying = true;
                        _startPoint = hand.PalmPosition;
                        print("start");
                 

                    }

                    if (isPlaying && gesture.State == Gesture.GestureState.STATE_STOP)
                    {
                        _endPoint = hand.PalmPosition;
                        
                        print("stop");

                        switch (_useAxis)
                        {
                            case 'x':
                                if (_startPoint.y < this._maxY && _endPoint.y < this._maxY &&
                                    (_startPoint.x) * this._useDirection < (_endPoint.x) * this._useDirection )
                                {

                                    this.isChecked = true;
                                    isPlaying = !isPlaying;
                                    break;
                                }
                                _state = gesture.State;
                                break;
                            case 'y':
                                if ((_startPoint.y) * this._useDirection < (_endPoint.y) * this._useDirection)
                                {

                                    this.isChecked = true;
                                    isPlaying = !isPlaying;
                                    break;
                                }
                                _state = gesture.State;
                                break;
                            case 'z':
                                if (_startPoint.y < this._maxY && _endPoint.y < this._maxY &&
                                    ( _startPoint.z * this._useDirection ) < ( _endPoint.z * this._useDirection ) )
                                {
                                    print(this._useDirection);
                                    this.isChecked = true;
                                    isPlaying = !isPlaying;
                                    _state = gesture.State;
                                    break;
                                }
                                _state = gesture.State;
                                break;
                            default:
                                break;
                        }
                        
                        
                    }

                    _state = gesture.State;
                }
            }
            if (isPlaying && _state == Gesture.GestureState.STATE_UPDATE)
            {
                print("update");
                _endPoint = hand.PalmPosition;

                switch (_useAxis)
                {
                    case 'x':
                        if (_startPoint.y < this._maxY && _endPoint.y < this._maxY &&
                            (_startPoint.x) * this._useDirection < (_endPoint.x) * this._useDirection)
                        {

                            this.isChecked = true;
                            isPlaying = !isPlaying;
                            break;
                        }
                        break;
                    case 'y':
                        if ((_startPoint.y) * this._useDirection < (_endPoint.y) * this._useDirection)
                        {

                            this.isChecked = true;
                            isPlaying = !isPlaying;
                            break;
                        }
                        break;
                    case 'z':
                        if (_startPoint.y < this._maxY && _endPoint.y < this._maxY &&
                            ( _startPoint.z * this._useDirection ) < ( _endPoint.z * this._useDirection ))
                        {
                            print(this._useDirection);
                            this.isChecked = true;
                            isPlaying = !isPlaying;
                            break;
                        }
                        break;
                    default:
                        break;
                }
                
            }

        }

        if(isChecked)
        {
            DoAction();
        }
        
    }

    protected virtual void DoAction()
    {
        print("행동 적어라 이자식아");
    }

    public virtual void UnCheck()
    {
        isChecked = false;
    }

    //1이면 오른손 0이면 왼손 제스처
    public int AnyHand()
    {
        if (_swipe_gestrue.IsValid)
        {
            if (_swipe_gestrue.Hands.Rightmost.IsRight)
            {
                this.isRight = true;
            }
            else if (_swipe_gestrue.Hands.Leftmost.IsLeft)
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

    // 사용자 임의의 제스처 설정 함수 오버라이딩 3가지 만들어 놓았고
    // 필요시 사용자가 다시 만들 수 있다.
    protected virtual bool SetGestureCondition(char axis, int direction)
    {
        this._useAxis = axis;
        this._useDirection = direction;

        if (axis == 'x')
        {

        }
        else if (axis == 'y')
        {

        }
        else if (axis == 'z')
        {

        }
        else
        {
            print("Set Axis what you want to use");
            return false;
        }

        return true;
    }

    protected virtual bool SetGestureCondition(char axis, float max, int direction)
    {
        this._useAxis = axis;
        this._useDirection = direction;
        if (axis == 'x')
        {
            _maxY = max;
        }
        else if (axis == 'y')
        {
            _maxY = max;
        }
        else if (axis == 'z')
        {
            _maxY = max;
        }
        else
        {
            print("Set Axis what you want to use");
            return false;
        }

        return true;
    }

    protected virtual Vector GetDirection()
    {
        Vector tempDirection = _swipe_gestrue.Direction;
        float x = Mathf.Abs(tempDirection.x);
        float y = Mathf.Abs(tempDirection.y);
        float z = Mathf.Abs(tempDirection.z);
        if (x > y && x > z)
        {
            if (tempDirection.x > 0) this._direction = new Vector(1, 0, 0);
            else if (tempDirection.x < 0) this._direction = new Vector(-1, 0, 0);
        }
        else if (y > x && y > z)
        {
            if (tempDirection.x > 0) this._direction = new Vector(0, 1, 0);
            else if (tempDirection.x < 0) this._direction = new Vector(0, -1, 0);
        }
        else if (z > x && z > y)
        {
            if (tempDirection.x > 0) this._direction = new Vector(0, 0, 1);
            else if (tempDirection.x < 0) this._direction = new Vector(0, 0, -1);
        }
        return this._direction;
    }

    protected bool SetBoundary(Vector3 boundary)
    {
        this._boundary = boundary;
        return true;
    }

    protected bool SetMaxVelocity(float velocity)
    {
        this._maxVelocity = velocity;
        return true;
    }

    protected bool SetMaxLength(float length)
    {

        this._maxLength = length;
        return true;
    }
}

