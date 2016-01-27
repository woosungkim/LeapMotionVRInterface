using UnityEngine;
using System.Collections;
using Leap;

public class GrabbingHand_Gesture : MonoBehaviour, ISingleStepCheckGesture {

    protected bool IsGrab;
    [HideInInspector]
    public GrabbingHand _grabbinghand_gesture;
    public MountType MountType;
    public UseArea UseArea;
    public UsingHand UsingHand;

    public UsingHand _usingHand
    { get; set; }

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

    public virtual void Start()
    {
        this.SetGestureCondition();
    }

    public virtual void Update()
    {
        if (!this._isChecked)
        {
            CheckGesture();
        }
        UnCheck();
    }
    
    public virtual void CheckGesture()
    {
        _lastFrame = _leap_controller.Frame();
        Hands = _lastFrame.Hands;
        
        if(!this._isChecked && IsEnableGestureHand())
        {
          
            foreach (Hand hand in Hands)
            {
              
                if(WhichSide.capturedSide(hand, _useArea, _mountType) && IsGrabbingHand(hand))
                {
                    this._isChecked = true;
                    break;
                }
            }
        }
        
 
        if (this._isChecked)
        {
            DoAction();
        }
    }
    
    public virtual void UnCheck()
    {
        _isChecked = false;
    }

    public bool IsEnableGestureHand()
    {
        return WhichSide.IsEnableGestureHand(this);
    }

    protected void SetGestureCondition()
    {
        _gestureType = GestureType.grabbinghand;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this, MountType, UseArea, UsingHand);
    }

    public virtual void DoAction()
    {
        print("Please code this method");
    }


    public bool IsGrabbingHand(Hand hand)
    {
        if (hand.GrabStrength == 1)
        {
            IsGrab = true;
        }
        else
        {
            IsGrab = false;
        }

        return IsGrab;
    }
}
