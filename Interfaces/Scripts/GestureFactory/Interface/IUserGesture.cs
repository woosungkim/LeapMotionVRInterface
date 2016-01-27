using UnityEngine;
using System.Collections;

public interface IUserGesture : IGesture {

    //User custom method that set gesture & controller options.
    bool SetConfig();
   
}
