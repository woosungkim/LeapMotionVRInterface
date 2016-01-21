using UnityEngine;
using System.Collections;

public class ShortCutOff : Circle_Gesture {

    GameObject ob;
    ShortcutController scc;


    public override void DoAction()
    {
        ob = GameObject.Find("ShortCut");
        print("ShortCut Off");
        ob.GetComponent<ShortcutController>().Disappear();



    }
}
