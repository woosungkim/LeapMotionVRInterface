using UnityEngine;
using System.Collections;

public class ShortCutOn : GrabbingHand_Gesture {

    GameObject ob;
    ShortcutController scc;
    GameObject ob2;

    public override void DoAction()
    {
        ob = GameObject.Find("ShortCut");
        ob2 = GameObject.Find("ShortCut (1)");
        print("ShortCutON");
        ob.GetComponent<ShortcutController>().Appear();
        ob2.GetComponent<ShortcutController>().Appear();
        

    }
}
