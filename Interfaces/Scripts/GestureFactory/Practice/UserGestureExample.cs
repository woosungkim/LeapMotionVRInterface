using UnityEngine;
using System.Collections;
using Leap;

public class UserGestureExample : UserGesture {
   
    private int _direction = 0;
    private float _length = 0;

    public override bool GestureCondition()
    {
        foreach (Hand hand in Hands)
        {
            if (!_isPlaying) // Gesture Recognization start.
            {
                StartPosition = hand.PalmPosition;
                _isPlaying = !_isPlaying;
                _length = 0;
            }
            else // Recognizing...
            {
                EndPosition = hand.PalmPosition;
                float temp = EndPosition.z - StartPosition.z;
                if (_direction == 0)// At the first time gesture recognize.
                {
                    if (temp > 0)
                    {
                        _direction = 1;
                    }
                    else if (temp < 0)
                    {
                        _direction = -1;
                    }
                    StartPosition = EndPosition;
                    _length += temp;
                }
                else
                {
                    float tempDirec = EndPosition.z - StartPosition.z;
                    if (tempDirec * _direction >= 0)// update gesture phase. When traveling direction and 
                    {                               // the current direction is the same.
                        _length += tempDirec;
                        StartPosition = EndPosition;
                    }
                    else//Stop Gesture recognize. Traveling direction and the current direction is not same.
                    {
                        if (_length > 50)
                        {
                            _isChecked = true;
                            _direction = 0;
                            _length = 0;
                            _isPlaying = !_isPlaying;
                            break;

                        }

                        _direction = 0;
                        _length = 0;
                        _isPlaying = !_isPlaying;
                        break;
                    }
                }
            }
        }

        return _isChecked;
    }

    GameObject obj;

    public void ChangeColor(Color color)
    {
        obj = GameObject.Find("Cube"); // Find object to change color,
        obj.GetComponent<Renderer>().material.color = color; // Change the color.
    }

    public override void DoAction()
    {
        ChangeColor(Color.red); // Call the handler method.
    }
}
