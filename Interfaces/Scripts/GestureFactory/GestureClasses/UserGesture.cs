using UnityEngine;
using System.Collections;
using Leap;

public class UserGesture : MonoBehaviour, IUserGesture {

    protected Vector StartPosition;
    protected Vector EndPosition;
    protected FingerList Fingers;
    protected bool IsGrab = false;
    protected bool IsUpward = false;//true면 손바닥이 위방향, false면 아래방향.
    protected Frame tFrame;

    public MountType _mountType
    { get; set; }

    public GestureType _gestureType
    { get; set; }

    public UseArea _useArea
    { get; set; }

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

    public bool _isHeadMount
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
        this.SetMount();
        this._useArea = UseArea.All;

        return true;
    }

    public void CheckGesture()
    {
        _lastFrame = _leap_controller.Frame(0);
        tFrame = _leap_controller.Frame(5);
        Hands = _lastFrame.Hands;
        Fingers = _lastFrame.Fingers;
        Hand hand = Hands.Frontmost;

       
        AnyHand();
        IsGrabbingHand();
        PalmDirection();
        this._isChecked = GestureCondition();

        if(_isChecked)
        {
            DoAction();
        }
    }

    public void IsGrabbingHand()
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

    public virtual void PalmDirection()
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
        print("Please make your Gesture Handler.");
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

  
    public void SetMount()
    {
        if (_mountType == MountType.HeadMount)
        {
            _isHeadMount = true;
        }
        else
        {
            _isHeadMount = false;
        }
    }
}
