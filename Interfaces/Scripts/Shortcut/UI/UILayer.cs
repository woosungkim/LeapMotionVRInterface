using UnityEngine;
using System.Collections;

public class UILayer : MonoBehaviour {

	private float _scale = 0.0f;
	private float _appearRate = 0.01f;
	private bool _appearAnimFlag = false;

	private bool _isCurrentLayer = false;
	public bool IsCurrentLayer {
		get {
			return _isCurrentLayer;
		}
	}
	
	private bool _disappearAnimFlag = false;




	internal void Build(ShortcutSetting setting) {



	}
	

	public void AppearLayer() {
		gameObject.SetActive (true);
		_appearAnimFlag = true;
		_scale = 0.0f;

		_isCurrentLayer = true;
	}

	public void DisappearLayer() {
		_disappearAnimFlag = true;
		_scale = 1.0f;

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
		_scale += _appearRate;
		gameObject.transform.localScale = Vector3.one * _scale;
		
		if (_scale >= 1.0f) {
			_appearAnimFlag = false;
			
		}
		
	}

	void DisappearAnimation() {
		_scale -= _appearRate;
		gameObject.transform.localScale = Vector3.one * _scale;

		if (_scale <= 0.0f) {
			_disappearAnimFlag = false;
			gameObject.SetActive(false);
		}
	}
}
