using UnityEngine;
using System.Collections;

public class UILayer : MonoBehaviour {

	private float _scale = 0.0f;
	private float _appearRate = 0.02f;

	private float _appearStart = 2.0f;
	private float _normalScale = 1.0f;
	private float _disappearEnd = 0.0f;

	private bool _appearAnimFlag = false;

	private bool _isCurrentLayer = false;
	public bool IsCurrentLayer {
		get {
			return _isCurrentLayer;
		}
	}
	
	private bool _disappearAnimFlag = false;


	internal void Build(ShortcutSettings sSettings, ShortcutItemSettings iSettings) {



	}
	

	public void AppearLayer() {
		gameObject.SetActive (true);
		_appearAnimFlag = true;
		_scale = _appearStart;

		_isCurrentLayer = true;
	}

	public void DisappearLayer() {
		_disappearAnimFlag = true;
		_scale = _normalScale;

		_isCurrentLayer = false;
	}

	void Update() {
		if (_appearAnimFlag) {
			AppearAnimation ();
		}

		if (_disappearAnimFlag) {
			DisappearAnimation();
		}
	}

	
	void AppearAnimation() {
		_scale -= _appearRate;
		gameObject.transform.localScale = Vector3.one * _scale;
		
		if (_scale <= _normalScale) {
			_appearAnimFlag = false;
			
		}
		
	}

	void DisappearAnimation() {
		_scale -= _appearRate;
		gameObject.transform.localScale = Vector3.one * _scale;

		if (_scale <= _disappearEnd) {
			_disappearAnimFlag = false;
			gameObject.SetActive(false);
		}
	}
}
