using UnityEngine;
using System.Collections;

public class StaticTest3 : MonoBehaviour {

    public int a = 0;
    StaticTest3 _instance;

    void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //StaticTest.changeValue(_instance);
        print(a);
    }
}
