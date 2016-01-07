using System;
using System.Collections.Generic;
using System.Text;
using Leap;

public interface GestureInterface
{
    bool SetConfig();
    bool CheckGesture();
    void UnCheck();
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
}

