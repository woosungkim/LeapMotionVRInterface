using UnityEngine;
using System.Collections;

public class ShortcutController : MonoBehaviour {

	public ShortcutSettings _ShortcutSettings;
	public ItemSettings _ItemSettings;
	public ItemHierarchy _ItemHierarchy;
	public Transform _Camera;

	private bool isFirst = true;

	void Awake () {
		if (!CheckInspector ()) {
			print ("Check inspector factors");
			return;
		}
		_ShortcutSettings.ItemSettings = _ItemSettings;

		PutInsideOfMainCamera ();
	}


	/* Check all Inspector factors are valid. */
	private bool CheckInspector() {
		if (_ShortcutSettings == null ||
		    _ItemSettings == null ||
		    _ItemHierarchy == null ||
		    _Camera == null) {
			return false; 
		}
		return true;
	}


	/* Put this shortcut inside of main camera. */
	private void PutInsideOfMainCamera() {
		gameObject.transform.SetParent (_Camera.transform, false);

		Vector3 pos = new Vector3 (_ShortcutSettings.XPosition, _ShortcutSettings.YPosition, ComputeZPos (_ShortcutSettings.XPosition, _ShortcutSettings.YPosition));
		gameObject.transform.position = Camera.main.ViewportToWorldPoint (pos);

	}

	private float ComputeZPos(float x, float y) {
		float d = 1.0f;

		return Mathf.Sqrt ((d * d) - ((x-0.5f) * (x-0.5f)) - ((y-0.5f) * (y-0.5f)));
	}


	/* Appear this shortcut. */
	public void Appear() {

			if (isFirst) {
				// item hierarchy build
				_ItemHierarchy.Build (_ShortcutSettings, gameObject);
				isFirst = false;
			} 
			else {
				_ItemHierarchy.Appear ();
			}


	}
	
	/* DisAppear this shortcut. */
	public void Disappear() {

		_ItemHierarchy.Disappear ();
	}

}
