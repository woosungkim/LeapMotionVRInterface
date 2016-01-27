using System;
using System.Collections.Generic;
using System.Text;
using Leap;

//모든 제스처 관련 클래스들이 상속 받아야할 인터페이스 클래스이다.
public interface IGesture
{
    // Function of Check gesture captured every frame.
    void CheckGesture();

    // Uncheck checked gesture flag
    void UnCheck();

    // Handler method. User must override this method. 
    void DoAction();
    
    // Application's mount type.
    // This value affect to leap motion axis.
    MountType _mountType
    { get; set; }

    // The value of using gesture type.
    GestureType _gestureType
    { get; set; }
   
    // Instance of Leap::Cotroller object.
    Controller _leap_controller
    { get; set; }

    // Hand list in gesture captured.
    HandList Hands
    { get; set; }

    // Frame object using check gesture.
    Frame _lastFrame
    { get; set; }
    
    // The area that user want to check gesture. 
    UseArea _useArea
    { get; set; }

    // A Flag of gesture check state.
    bool _isChecked
    { get; set; }

    // The value that user want to check.
    UsingHand _usingHand
    { get; set; }

}

