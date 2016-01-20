using UnityEngine;
using System.Collections;
using Leap;

public class MyGestureU : UserGesture {

    public int _direction = 0;
    public float _length = 0;
    Switcher _switcher = null;

    public override bool SetConfig()
    {
        _switcher = Switcher.GetInstance();
        return base.SetConfig();

    }

    protected override void DoAction()
    {
        //base.DoAction();
        _switcher.switchCameraToAR();
        print("AR");
    }

    protected override bool GestureCondition()
    {
        //return base.GestureCondition();

        foreach(Hand hand in Hands)
        {
            if(!_isPlaying)//손 인식
            {
                StartPosition = hand.PalmPosition;
                _isPlaying = !_isPlaying;
                _length = 0;
                print("0");
            }
            else//인식 중
            {
                EndPosition = hand.PalmPosition;
                float temp = EndPosition.z - StartPosition.z;
                if(_direction == 0)//처음 제스처를 받을 때.
                {
                    if(temp > 0)
                    {
                        _direction = 1;
                    }else if(temp < 0)
                    {
                        _direction = -1;
                    }
                    StartPosition = EndPosition;
                    _length += temp;
                    print("1");
                }
                else
                {
                    float tempDirec = EndPosition.z - StartPosition.z;
                    if(tempDirec * _direction >= 0)//방향이 이어질 때
                    {
                        _length += tempDirec;
                        StartPosition = EndPosition;
                        print("2");
                    }
                    else//방향이 이어지지 않을 때 -> 갑자기 방향이 바뀌었거나 종료된거라고 생각.
                    {
                        if(_length > 50)
                        {
                            _isChecked = true;
                            _direction = 0;
                            _length = 0;
                            _isPlaying = !_isPlaying;
                            print("4");
                            break;

                        }

                        _direction = 0;
                        _length = 0;
                        _isPlaying = !_isPlaying;
                        print("3");
                        break;
                    }
                }
            }
        }

        return _isChecked;
    }
    
}
