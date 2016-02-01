using UnityEngine;
using System.Collections;
using Leap;

public class UserGesture : MonoBehaviour, IUserGesture {

    [HideInInspector]
    public Hand tempHand;

    protected Vector StartPosition;
    protected Vector EndPosition;
    protected FingerList Fingers;
    protected bool IsGrab = false;
    protected bool IsUpward = false;//true면 손바닥이 위방향, false면 아래방향.
    protected Frame tFrame;
    protected Hand hand;

    public MountType MountType;
    public UsingHand UsingHand;
    public MountType _mountType
    { get; set; }

    public GestureType _gestureType
    { get; set; }

    public UseArea _useArea
    { get; set; }

    public UsingHand _usingHand
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
        this._gestureType = GestureType.usercustom;
        _leap_controller = new Controller();
        this._useArea = UseArea.All;
        this._mountType = MountType;
        this._usingHand = UsingHand;

        return true;
    }

    public void CheckGesture()
    {
        _lastFrame = _leap_controller.Frame(0);
        tFrame = _leap_controller.Frame(5);
        Hands = _lastFrame.Hands;

        foreach(Hand hand in Hands)
        {
            tempHand = hand;
            Fingers = hand.Fingers;
            if (WhichSide.IsEnableGestureHand(this) && WhichSide.IsEnableGestureHand(this) && WhichSide.capturedSide(hand, _useArea, _mountType))
            {
                this._isChecked = GestureCondition();
                if (this._isChecked)
                    break;
            }
        }
       
       

        if(_isChecked)
        {
            DoAction();
        }
    }

    public virtual bool GestureCondition()
    {

        return _isChecked;
    }

    public virtual void DoAction()
    {
        print("Please make your Gesture Handler.");
    }

    public virtual void UnCheck()
    {
        _isChecked = !_isChecked;
    }

}
