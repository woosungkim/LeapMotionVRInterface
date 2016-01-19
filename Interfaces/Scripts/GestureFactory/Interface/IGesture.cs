using System;
using System.Collections.Generic;
using System.Text;
using Leap;

//모든 제스처 관련 클래스들이 상속 받아야할 인터페이스 클래스이다.
public interface IGesture
{
    /*
    // Function for setting basic option of using gesture.
    bool SetConfig();
    */
    // Function of Check gesture captured every frame.
    void CheckGesture();

    // Uncheck checked gesture flag
    void UnCheck();

    // Function of getting hand which side user used.
    bool AnyHand();

    // Set flag of VR mode.
    void SetMount();

    GestureType gt
    { get; set; }
   
    Controller _leap_controller
    {
        get;
        set;
    }

    HandList Hands
    {
        get;
        set;
    }

    Frame _lastFrame
    {
        get;
        set;
    }

    int _userSide
    {
        get;
        set;
    }

    bool _isChecked
    {
        get;
        set;
    }

    bool _isRight
    {
        get;
        set;
    }

    bool _isHeadMount
    {
        get;
        set;
    }

    bool _isPlaying
    {
        get;
        set;
    }

}

