using UnityEngine;
using System.Collections;

public class tempGesture2 : FlipHand_Gesture
{

    GameObject ob;

    public override void DoAction()
    {
        print(1);
        ob = GameObject.Find("Cube");
        ob.GetComponent<Renderer>().material.color = Color.red;
    }
}
