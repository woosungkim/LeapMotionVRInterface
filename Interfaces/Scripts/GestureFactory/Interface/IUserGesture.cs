using UnityEngine;
using System.Collections;

public interface IUserGesture : IGesture {

    bool SetConfig();
    void SetMount();
    void IsGrabbingHand();
    void PalmDirection();
}
