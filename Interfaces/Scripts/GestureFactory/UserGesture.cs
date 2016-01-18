using UnityEngine;
using System.Collections;
using Leap;

public class UserGesture : MonoBehaviour, IGesture {

    protected Vector StartPosition;
    protected Vector EndPosition;
    protected FingerList Fingers;
    protected bool IsGrab = false;
    protected bool IsUpward = false;//true면 손바닥이 위방향, false면 아래방향.
    protected Frame tFrame;

    public Controller _leap_controller
    { get; set; }

    public HandList Hands
    { get; set; }

    public Frame _lastFrame
    { get; set; }

    public bool _isChecked
    { get; set; }

    public bool _isRight
    { get; set; }

    public bool _isPlaying
    { get; set; }

    public int _userSide
    { get; set; }

    public bool _isVR
    { get; set; }

    void Start()
    {
        SetConfig();
    }
   
    void Update()
    {
        if(!this._isChecked)
        {
           
            CheckGesture();
        }
        UnCheck();
    }
    
	// Update is called once per frame
    public virtual bool SetConfig()
    {
        _leap_controller = new Controller();
        this._isVR = false;
        this._userSide = 0;

        return true;
    }

    public void CheckGesture()
    {
        _lastFrame = _leap_controller.Frame(0);
        tFrame = _leap_controller.Frame(5);
        Hands = _lastFrame.Hands;
        Fingers = _lastFrame.Fingers;
        Hand hand = Hands.Frontmost;

        WhichSide(hand);
        AnyHand();
        IsGrabbingHand();
        PalmDirection();
        this._isChecked = GestureCondition();

        if(_isChecked)
        {
            DoAction();
        }
    }

    protected void IsGrabbingHand()
    {
        if (Hands.Frontmost.GrabStrength == 1)
        {
            IsGrab = true;
        }
        else
        {
            IsGrab = false;
        }
    }

    protected virtual void PalmDirection()
    {
        Hand tempHand = Hands.Frontmost;
       
        float pitch = tempHand.Direction.Pitch;
        float yaw = tempHand.Direction.Yaw;
        float roll = tempHand.PalmNormal.Roll;

        if( roll > -0.5f && roll < 0.5f )
        {
            IsUpward = false;
        }
        else if( roll > 2.5f && roll < 3.5)
        {
            IsUpward = true;
        }
       
    }

    protected virtual bool GestureCondition()
    {

        return _isChecked;
    }

    protected virtual void DoAction()
    {
        print("만들어라 이 자식아");
    }

    public virtual void UnCheck()
    {
        _isChecked = !_isChecked;
    }

    public virtual bool AnyHand()
    {
        if(this.Hands.IsEmpty)
        {
            return false;
        }
        return false;
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
}
