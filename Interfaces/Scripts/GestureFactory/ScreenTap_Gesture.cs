using UnityEngine;
using System.Collections;
using Leap;


public class ScreenTap_Gesture : MonoBehaviour, IGesture 
{
    protected float _minForwardVelocity = 50f;
    protected float _maxForwardVelocity;

    protected float _historySeconds = 0.1f;

    protected float _minDistance = 5.0f;
    protected float _maxDistance;

    protected ScreenTapGesture _screentap_gesture;
    protected Vector _direction;
    protected Pointable _pointable;
    protected Vector _position;

    protected GestureList _gestures;
    protected FingerList _fingers;


    public bool _isRight
    { get; set; }

    public Controller _leap_controller
    { get; set; }

    public HandList Hands
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
        if(!this._isChecked)
        {
            CheckGesture();
        }
        UnCheck();
    }

    public bool SetConfig()
    {
        _leap_controller = new Controller();
        _leap_controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
        _leap_controller.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", this._minForwardVelocity);
        _leap_controller.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", this._historySeconds);
        _leap_controller.Config.SetFloat("Gesture.ScreenTap.MinDistance", this._minDistance);
        _leap_controller.Config.Save();

        this._userSide = 0;
        this._isVR = false;
        this._maxDistance = 100000;
        this._maxForwardVelocity = 100000;
        this._isChecked = false;
        this._isRight = false;

        return true;
    }

    public virtual void CheckGesture()
    {
        _lastFrame = _leap_controller.Frame(0);
        Hands = _lastFrame.Hands;
        _gestures = _lastFrame.Gestures();

        foreach(Hand hand in Hands)
        {
            this._fingers = hand.Fingers;

            foreach(Gesture gesture in _gestures)
            {
                if ( (gesture.Type == Gesture.GestureType.TYPE_SCREEN_TAP) && WhichSide(hand) )
                {
                    _screentap_gesture = new ScreenTapGesture(gesture);

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

        if(this._isChecked)
        {
            DoAction();
        }
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
            //print("leap : " + position);
            Vector3 unityPosition = position.ToUnity();
            Vector3 toPos = new Vector3(((unityPosition.x + 150.0f) / 300.0f), (unityPosition.y / 300.0f), ((unityPosition.z + 150.0f) / 300.0f));
            Vector3 pos = Camera.main.ViewportToWorldPoint(toPos);
            Vector3 tempos = Camera.main.WorldToViewportPoint(pos);
            //print(tempos);

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

    public bool AnyHand()
    {
        if(_screentap_gesture.IsValid)
        {
            if (_screentap_gesture.Hands.Rightmost.IsRight)
            {
                this._isRight = true;
            }
            else if (_screentap_gesture.Hands.Leftmost.IsLeft)
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

    protected virtual void DoAction()
    {
        print("Please code this method");
    }

    protected bool SetMaxVelocity(float velocity)
    {
        this._maxForwardVelocity = velocity;
        return true;
    }

    protected bool SetMaxDistance(float distance)
    {
        this._maxDistance = distance;
        return true;
    }

    protected Vector GetDirection()
    {
        if(_screentap_gesture != null)
        {
            Vector tempDirection = _screentap_gesture.Direction;
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

    protected Vector GetPosition()
    {
        if(_screentap_gesture != null)
        {
            this._position = _screentap_gesture.Position;
            return this._position;
        }
        else
        {
            return null;
        }
    }

    protected Pointable GetPointable()
    {
        if(_screentap_gesture != null)
        {
            this._pointable = _screentap_gesture.Pointable;
            return this._pointable;
        }
        else
        {
            return null;
        }
        
    }

    protected HandList GetHandList()
    {
        if(_screentap_gesture != null)
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
        if(_screentap_gesture != null)
        {
            return _fingers;
        }
        else
        {
            return new FingerList();
        }
    }
}

