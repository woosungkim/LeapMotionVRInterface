using UnityEngine;
using System.Collections;

public class tempGesture : FlipHand_Gesture {
    
    GameObject ob;

    public override void DoAction()
    {
        ob = GameObject.Find("Cube");
        ob.GetComponent<Renderer>().material.color = Color.black;
    }
}
