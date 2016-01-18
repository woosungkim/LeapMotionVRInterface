using UnityEngine;
using System.Collections;
using Leap;


public class KeyTap_Gesture : MonoBehaviour, IGesture 
{
    protected float _minDownVelocity = 50f;
    protected float _maxDownVelocity;

    protected float _historySeconds = 0.1f;

    protected float _minDistance = 3.0f;
    protected float _maxDistance;

    protected KeyTapGesture _keytab_gesture;
    protected Vector _direction;
    protected Pointable _pointable;
    protected Vector _position;
    protected float _progress;

    
    protected GestureList _gestures;
    protected FingerList _fingers;

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
        if(!this._isChecked)
        {
            CheckGesture();
        }
        UnCheck();
    }

    public bool SetConfig()
    {
        _leap_controller = new Controller();
        _leap_controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
        _leap_controller.Config.SetFloat("Gesture.KeyTap.MinDownVelocity", this._minDownVelocity);
        _leap_controller.Config.SetFloat("Gesture.KeyTap.HistorySeconds", this._historySeconds);
        _leap_controller.Config.SetFloat("Gesture.KeyTap.MinDistance", this._minDistance);
        _leap_controller.Config.Save();

        this._isVR = false;
        this._userSide = 0;
        this._isRight = false;
        this._isChecked = false;
        this._maxDistance = 100000;
        this._maxDownVelocity = 100000;
        this._direction = Vector.Zero;

        return true;
    }

    protected virtual Vector GetDirection()
    {
        Vector tempDirection = _keytab_gesture.Direction;
        float x = Mathf.Abs(tempDirection.x);
        float y = Mathf.Abs(tempDirection.y);
        float z = Mathf.Abs(tempDirection.z);
        if(x>y && x>z)
        {
            if (tempDirection.x > 0) this._direction = new Vector(1, 0, 0);
            else if (tempDirection.x < 0) this._direction = new Vector(-1, 0, 0);
        }
        else if(y>x && y>z)
        {
            if (tempDirection.y > 0) this._direction = new Vector(0, 1, 0);
            else if (tempDirection.y < 0) this._direction = new Vector(0, -1, 0);
        }
        else if(z>x && z>y)
        {
            if (tempDirection.z > 0) this._direction = new Vector(0, 0, 1);
            else if (tempDirection.z < 0) this._direction = new Vector(0, 0, -1);
        }

        return this._direction;
    }

    public virtual void CheckGesture()
    {
        this._lastFrame = this._leap_controller.Frame(0);
        this.Hands = this._lastFrame.Hands;
        this._gestures = this._lastFrame.Gestures();
        
        foreach(Hand hand in Hands)
        {
            this._fingers = hand.Fingers;

            foreach(Gesture gesture in _gestures)
            {
                if ( (gesture.Type == Gesture.GestureType.TYPE_KEY_TAP) && WhichSide(hand))
                {
                    _keytab_gesture = new KeyTapGesture(gesture);

                    this.GetDirection();
                    this.AnyHand();
                    this.GetPointable();
                    this.GetPosition();
                    this._isChecked = true;

                    break;
                }
            }

            if(this._isChecked)
            { break; }

        }
       
        if(this._isChecked)
        {
            DoAction();
        }
    }

    public virtual void UnCheck()
    {
        this._isChecked = false;
    }

    protected virtual void DoAction()
    {
        print("Please code this method");
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

    public bool AnyHand()
    {
        if(_keytab_gesture.IsValid)
        {
            if(_keytab_gesture.Hands.Rightmost.IsRight)
            {
                this._isRight = true;
            }
            else if(_keytab_gesture.Hands.Leftmost.IsLeft)
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

    protected bool SetMaxDownVelocity(float velocity)
    {
        this._maxDownVelocity = velocity;
        return true;
    }

    protected bool SetMaxDistance(float distance)
    {
        this._maxDistance = distance;
        return true;
    }

    protected HandList GetHandList()
    {
        if(_keytab_gesture != null)
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
        if(_keytab_gesture != null)
        {
            return _fingers;
        }
        else
        {
            return new FingerList();
        }
        
    }

    protected Pointable GetPointable()
    {
        if(_keytab_gesture != null)
        {
            _pointable = _keytab_gesture.Pointable;
            return _pointable;
        }
        else
        {
            return null;
        }
        
    }

    protected Vector GetPosition()
    {
        if(_keytab_gesture != null)
        {
            _position = _keytab_gesture.Position;
            return _position;
        }
        else
        {
            return null;
        }
        
    }

}


