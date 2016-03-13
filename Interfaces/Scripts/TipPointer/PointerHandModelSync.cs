using UnityEngine;
using System.Collections;

public class PointerHandModelSync : MonoBehaviour {

	private Camera _Camera;

	// Use this for initialization
	void Start () {
		_Camera = Camera.main;

		gameObject.transform.SetParent (_Camera.transform, false);

		gameObject.transform.eulerAngles = new Vector3 (270.0f, 180.0f, 0.0f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
