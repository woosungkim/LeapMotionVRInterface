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
    int AnyHand();
    
    Controller _leap_controller
    {
        get;
        set;
    }

    Frame lastFrame
    {
        get;
        set;
    }

    bool isChecked
    {
        get;
        set;
    }

    bool isRight
    {
        get;
        set;
    }

    bool isPlaying
    {
        get;
        set;
    }
}

