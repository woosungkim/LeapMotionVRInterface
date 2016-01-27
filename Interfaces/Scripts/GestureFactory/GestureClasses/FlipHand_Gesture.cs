using UnityEngine;
using System.Collections;
using Leap;

public class FlipHand_Gesture : MonoBehaviour, ISingleStepCheckGesture {

    [HideInInspector]
    public FlipHand_Gesture _fliphand_gesture;
    [HideInInspector]
    public PalmDirection _palmDirection;
    public MountType MountType;
    public UseArea UseArea;
    public UsingHand UsingHand;
    public PalmDirection PalmDirection;

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

        if (!this._isChecked && IsEnableGestureHand())
        {

            foreach (Hand hand in Hands)
            {

                if (WhichSide.capturedSide(hand, _useArea, _mountType) && IsCorrectHandDirection(hand))
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
        _gestureType = GestureType.fliphand;
        _leap_controller = ControllerSetter.SetConfig(_gestureType);
        GestureSetting.SetGestureCondition(this, MountType, UseArea, UsingHand, PalmDirection);
    }

    public virtual void DoAction()
    {
        print("Please code this method");
    }


    public virtual bool IsCorrectHandDirection(Hand hand)
    {
        Hand tHand = hand;
        float roll = tHand.PalmNormal.Roll;
        
        if(_palmDirection == PalmDirection.DownWard)
        {
            if (roll > -0.5f && roll < 0.5f)
            {
                return true;
            }
            else if (roll > 2.5f && roll < 3.5)
            {
                return false;
            }
        }
        else
        {
            if (roll > -0.5f && roll < 0.5f)
            {
                return false;
            }
            else if (roll > 2.5f && roll < 3.5)
            {
                return true;
            }
        }

        return false;
    }
}
