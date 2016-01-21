using UnityEngine;
using System.Collections;

public class UILayer : MonoBehaviour, IUILayer {

	private float _scale = 2.0f;
	private float _appearRate = 0.04f;

	private float _outScale = 2.0f;
	private float _normalScale = 1.0f;
	private float _inScale = 0.0f;

	private bool _appearAnimFlag = false;
	private bool _disappearAnimFlag = false;

	private int _aDirection = 0; // appear direction
	private int _dDirection = 0; // disappear direction

	private int activeCnt = 0;

	private bool _isCurrentLayer = false;
	public bool IsCurrentLayer { get { return _isCurrentLayer; } }
	

	public void Build(ShortcutSettings sSettings) {
		gameObject.transform.localScale = Vector3.one * _scale;

		if (sSettings.Type == ShortcutType.Arc) { // 회전 보정
			gameObject.transform.localRotation = Quaternion.FromToRotation (Vector3.down, Vector3.forward);
		} 

	}
	

	public void AppearLayer(int direction) {

		gameObject.GetComponentInParent<ItemHierarchy> ().CurLayerObj = gameObject;

		gameObject.SetActive (true);
		_aDirection = direction;
		if (direction > 0) {
			_scale = _outScale;
		} 
        else {
			_scale = _inScale;
		} 

		_isCurrentLayer = true;
        _appearAnimFlag = true;
	}

	public void DisappearLayer(int direction) {
		
		_dDirection = direction;
		_scale = _normalScale;

		_isCurrentLayer = false;
        _disappearAnimFlag = true;
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
		if (_aDirection > 0) {
			if (_scale == _outScale) {
				gameObject.SetActive (false);
				gameObject.SetActive (true);
			}
			_scale -= _appearRate;
			gameObject.transform.localScale = Vector3.one * _scale;
			
			if (_scale <= _normalScale) {
				_appearAnimFlag = false;
			}
		} 
		else {
			if (_scale == _inScale) {
				gameObject.SetActive (false);
				gameObject.SetActive (true);
			}

			_scale += _appearRate;

			gameObject.transform.localScale = Vector3.one * _scale;
			if (_scale >= _normalScale) {
				_appearAnimFlag = false;
			}
		}
	}

	void DisappearAnimation() {
		if (_dDirection > 0) {
			_scale -= _appearRate;
			gameObject.transform.localScale = Vector3.one * _scale;

			if (_scale <= _inScale) {
				_disappearAnimFlag = false;
				gameObject.SetActive(false);
			}
		}
		else {
			_scale += _appearRate;
			gameObject.transform.localScale = Vector3.one * _scale;
			
			if (_scale >= _outScale) {
				_disappearAnimFlag = false;
				gameObject.SetActive(false);
			}
		}
	}

}
