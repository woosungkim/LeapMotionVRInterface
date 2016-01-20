using UnityEngine;
using System.Collections;

public static class Static {

	public static void Change<T>(T ob)
    {
        string t = ob.GetType().ToString();
        if(t.Equals("StaticTest2"))
        {
            StaticTest2 temp = ob as StaticTest2;
            temp.a = 2;
        }
    }
}
