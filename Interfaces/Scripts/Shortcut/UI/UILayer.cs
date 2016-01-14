using UnityEngine;
using System.Collections;

public class UILayer : MonoBehaviour {

	private float _scale = 0.0f;
	private float _appearRate = 0.01f;
	private bool _appearAnimFlag = false;

	internal void Build(ShortcutSetting setting) {



	}


	void OnEnable() {
		_appearAnimFlag = true;
		_scale = 0.0f;
	}
	
	void Update() {
		if (_appearAnimFlag) {
			AppearAnimation ();
		}
		
				
	}
	
	void AppearAnimation() {
		_scale += _appearRate;
		gameObject.transform.localScale = Vector3.one * _scale;
		
		if (_scale >= 1.0f) {
			_appearAnimFlag = false;
			
		}
		
	}
}
