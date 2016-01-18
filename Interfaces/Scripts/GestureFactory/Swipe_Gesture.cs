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
    protected int _sensitivity = 0;
    //----------------------------------------------------------

    //all:0, left:1, right:2, up:3, down:4.
    public int _userSide
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

    public bool _isVR
    { get; set; }

    void Update()
    {
        if (!_isChecked)
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

        this._isVR = false;
        this._isChecked = false;
        this._userSide = 0;
        this._direction = Vector.Zero;
        this._boundary = Vector3.zero;
        this._maxVelocity = 100000;
        this._maxLength = 100000;
        this._state = Gesture.GestureState.STATE_INVALID;
        this._isRight = false;
        this._isPlaying = false;

        return true;
    }

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
                    
                    if (!_isPlaying && gesture.State == Gesture.GestureState.STATE_START &&WhichSide(hand))
                    {
                        _isPlaying = true;
                        _startPoint = hand.PalmPosition;
                        //print("start");
                    }

                    if (_isPlaying && gesture.State == Gesture.GestureState.STATE_STOP)
                    {
                        _endPoint = hand.PalmPosition;
                        
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

            if (_isPlaying && _state == Gesture.GestureState.STATE_UPDATE && WhichSide(hand))
            {
                print("update");
                _endPoint = hand.PalmPosition;

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

        if(_isChecked)
        {
            DoAction();
        }
        
    }

    protected virtual void DoAction()
    {
        print("Please override this method");
    }

    public virtual void UnCheck()
    {
        _isChecked = false;
    }

    //1이면 오른손 0이면 왼손 제스처
    public bool AnyHand()
    {
        if (_swipe_gestrue.IsValid)
        {
            if (_swipe_gestrue.Hands.Rightmost.IsRight)
            {
                this._isRight = true;
            }
            else if (_swipe_gestrue.Hands.Leftmost.IsLeft)
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
            //print("leap : " + position);
            Vector3 unityPosition = position.ToUnity();
            Vector3 toPos = new Vector3( 1-((unityPosition.x + 150.0f) / 300.0f), ((unityPosition.z + 150.0f) / 300.0f), (unityPosition.y / 300.0f) );
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

    // 사용자 임의의 제스처 설정 함수 오버라이딩 4가지 만들어 놓았고
    // 필요시 사용자가 다시 만들 수 있다.
    protected virtual bool SetGestureCondition(char axis, int direction, int side)
    {
        this.SetConfig();
        this._useAxis = axis;
        this._useDirection = direction;
        this._userSide = side;
        if (axis == 'x')
        {}
        else if (axis == 'y')
        {}
        else if (axis == 'z')
        {}
        else
        {
            print("Set Axis what you want to use");
            return false;
        }

        return true;
    }

    protected virtual bool SetGestureCondition(char axis, int direction, int sensitivity, int side)
    {
        this.SetConfig();
        this._useAxis = axis;
        this._useDirection = direction;
        this._sensitivity = sensitivity;
        this._userSide = side;
        if (axis == 'x')
        {}
        else if (axis == 'y')
        {}
        else if (axis == 'z')
        {}
        else
        {
            print("Set Axis what you want to use");
            return false;
        }

        return true;
    }

    protected virtual bool SetGestureCondition(char axis, float max, int direction, int side)
    {
        this.SetConfig();
        this._useAxis = axis;
        this._useDirection = direction;
        this._userSide = side;
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

    protected virtual bool SetGestureCondition(char axis, float max, int direction, int sensitivity, int side)
    {
        this.SetConfig();
        this._useAxis = axis;
        this._useDirection = direction;
        this._sensitivity = sensitivity;
        this._userSide = side;
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

    protected virtual Vector GetDirection()
    {
        if(_swipe_gestrue != null)
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
        else
        {
            return null;
        }
    }

    protected HandList GetHandList()
    {
        if(_swipe_gestrue != null)
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
        if(_swipe_gestrue != null)
        {
            return _fingers;
        }
        else
        {
            return new FingerList();
        }
    }
}


