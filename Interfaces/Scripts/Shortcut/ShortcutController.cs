using UnityEngine;
using System.Collections;

public class ShortcutController : MonoBehaviour {

	public ShortcutSetting _ShortcutSetting;
	public ItemHierarchy _ItemHierarchy;
	public Transform _Camera;
	
	// Use this for initialization
	void Start () {
		// set shortcut inside of camera
		gameObject.transform.SetParent (_Camera.transform, false);
	
		Vector3 pos = new Vector3 (_ShortcutSetting.X, _ShortcutSetting.Y, _ShortcutSetting.Z);
		gameObject.transform.position = Camera.main.ViewportToWorldPoint (pos);
		gameObject.transform.rotation = Quaternion.LookRotation(_Camera.transform.up);


		// item hierarchy build
		_ItemHierarchy.Build (_ShortcutSetting);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
