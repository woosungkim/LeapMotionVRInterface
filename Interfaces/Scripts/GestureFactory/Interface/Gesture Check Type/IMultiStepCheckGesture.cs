using UnityEngine;
using System.Collections;
using Leap;

// The interface class of define multiple check step gesture.
public interface IMultiStepCheckGesture : IGesture
{

    // The flag for indicating that gesture check is in progress.
    bool _isPlaying
    { get; set; }

    // The value for check gesture in multiple state.
    Gesture.GestureState _state
    { get; set; }


}
