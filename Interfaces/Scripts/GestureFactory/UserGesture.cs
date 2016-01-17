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

        return true;
    }

    public void CheckGesture()
    {
        _lastFrame = _leap_controller.Frame(0);
        tFrame = _leap_controller.Frame(5);
        Hands = _lastFrame.Hands;
        Fingers = _lastFrame.Fingers;

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
        //print("pitch = " + pitch);
        //print("yaw = " + yaw);
        //print("roll = " + roll);
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
   
}
