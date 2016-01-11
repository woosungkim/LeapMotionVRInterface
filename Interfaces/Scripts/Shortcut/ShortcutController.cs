using UnityEngine;
using System.Collections;

public class ShortcutController : MonoBehaviour {

	public ShortcutSetting _ShortcutSetting;
	public ItemHierarchy _ItemHierarchy;
	public Transform _Camera;
	
	// Use this for initialization
	void Start () {
		gameObject.transform.SetParent (_Camera.transform, false);

		_ItemHierarchy.Build (_ShortcutSetting, _Camera);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
