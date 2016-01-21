using UnityEngine;
using System.Collections;

public class Practice0119 : Swipe_Gesture {

    public override void DoAction()
    {
        GameObject cube = GameObject.Find("Cube");
        cube.GetComponent<Renderer>().material.color = Color.red;
    }
}
