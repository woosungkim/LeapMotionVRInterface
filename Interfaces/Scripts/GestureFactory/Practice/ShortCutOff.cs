using UnityEngine;
using System.Collections;

public class ShortCutOff : GrabbingHand_Gesture {

    GameObject ob;
    ShortcutController scc;
    GameObject ob2;

    public override void DoAction()
    {
        ob = GameObject.Find("ShortCut");
        ob2 = GameObject.Find("ShortCut (1)");
        print("ShortCut OFF");
        ob.GetComponent<ShortcutController>().Disappear();
        ob2.GetComponent<ShortcutController>().Disappear();


    }
}
