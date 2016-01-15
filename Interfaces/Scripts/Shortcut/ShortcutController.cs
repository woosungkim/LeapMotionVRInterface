using UnityEngine;
using System.Collections;

public class ShortcutController : MonoBehaviour {

	public ShortcutSettings _ShortcutSettings;
	public ShortcutItemSettings _ShortcutItemSettings;
	public ItemHierarchy _ItemHierarchy;
	public Transform _Camera;
	
	// Use this for initialization
	void Start () {
		// set shortcut inside of camera
		gameObject.transform.SetParent (_Camera.transform, false);
	
		Vector3 pos = new Vector3 (_ShortcutSettings.XPosition, _ShortcutSettings.YPosition, 1.0f);
		gameObject.transform.position = Camera.main.ViewportToWorldPoint (pos);
		gameObject.transform.rotation = Quaternion.LookRotation(_Camera.transform.up);

		gameObject.transform.localScale = Vector3.one * _ShortcutSettings.ScaleCoeff;

		// item hierarchy build
		_ItemHierarchy.Build (_ShortcutSettings, _ShortcutItemSettings);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Appear() {

	}

	public void Disappear() {

	}
}
