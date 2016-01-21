using UnityEngine;
using System.Collections;

public class ShortCutOn : Circle_Gesture {

    GameObject ob;
    ShortcutController scc;
 

    public override void DoAction()
    {
        ob = GameObject.Find("ShortCut");
        print("ShortCutON");
        ob.GetComponent<ShortcutController>().Appear();
            
        

    }
}
