using System;
using System.Collections.Generic;
using System.Text;
using Leap;

//모든 제스처 관련 클래스들이 상속 받아야할 인터페이스 클래스이다.
public interface IGesture
{
    bool SetConfig();
    void CheckGesture();
    void UnCheck();
    bool AnyHand();
    bool WhichSide(Hand hand);
    void OnVR();

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

    bool _isVR
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

