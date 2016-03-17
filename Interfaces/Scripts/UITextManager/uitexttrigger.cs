using UnityEngine;
using System.Collections;

public class uitexttrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			gameObject.GetComponent<UITextManager> ().EraseText ();
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			gameObject.GetComponent<UITextManager> ().DrawText ();
		}
	}
}
