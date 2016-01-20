using UnityEngine;
using System.Collections;

public class StaticTest2 : MonoBehaviour {

    public int a = 0;
    
	// Use this for initialization
	void Start () {
       

	}
	
	// Update is called once per frame
	void Update () {
        Static.Change(this);
        print(a);
	}
}
