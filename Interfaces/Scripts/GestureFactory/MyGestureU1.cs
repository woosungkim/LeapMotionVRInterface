using UnityEngine;
using System.Collections;
using Leap;

public class MyGestureU1 : UserGesture {

    GameObject obj;

    public void ChangeColor(Color color)
    {
        obj = GameObject.Find("Cube");
        obj.GetComponent<Renderer>().material.color = color;

    }

    protected override void DoAction()
    {
        //base.DoAction();
        if (IsUpward)
        {
            ChangeColor(Color.blue);
        }

        if (IsGrab)
        {
            ChangeColor(Color.red);
        }

    }

    protected override bool GestureCondition()
    {
        bool result = false;// Result of gesture recognition.

        foreach (Hand hand in Hands)// All of hands captured.
        {
            if (IsUpward)// 
            {
                result = true;
                break;
            }

            if (IsGrab)
            {
                result = true;
                break;
            }
        }

        return result;
    }
}
