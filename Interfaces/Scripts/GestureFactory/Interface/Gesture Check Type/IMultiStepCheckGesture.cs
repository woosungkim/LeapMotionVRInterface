using UnityEngine;
using System.Collections;
using Leap;

public interface IMultiStepCheckGesture : IGesture
{

    bool _isPlaying
    { get; set; }

    Gesture.GestureState _state
    { get; set; }

}
