using UnityEngine;
using System.Collections;

public class CameraTransitions : MonoBehaviour {

	public static CameraTransitions _instance;

	enum CameraState : int { AR, VR };

	public GameObject _targetHead = null;
	private GameObject quadForeground;
	private CameraState state;
	private Vector3 vrPos, arPos;

	// Use this for initialization
	void Start () {
		if (_targetHead == null) {
			Debug.Log ("no target");
			return;
		}

		quadForeground = GameObject.Find ("QuadForeground");

		transform.position = _targetHead.transform.position;
		transform.rotation = _targetHead.transform.rotation;

		state = CameraState.VR;
		vrPos = new Vector3 (0.0f, 1.0f, 0.137f);
		arPos = new Vector3 (0.0f, 0.0f, 0.137f);
	}

	void Awake() {
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (_targetHead != null) {
			// track camera position & rotation
			//transform.position = _targetHead.transform.position;
			transform.rotation = _targetHead.transform.rotation;
		}
	}

	public void switchCamera() {
		Debug.Log ("switch camera");
		StartCoroutine (switchCameraRutine( state ));

	}

	public void switchCameraToVR() {
		StartCoroutine (switchCameraRutine (CameraState.AR));
	}

	public void switchCameraToAR() {
		StartCoroutine (switchCameraRutine (CameraState.VR));
	}

	IEnumerator switchCameraRutine( CameraState from ) {
		float t = 0.0f;
		Vector3 fromPos, toPos;

		fromPos = quadForeground.transform.localPosition;
		toPos = (from == CameraState.AR) ? vrPos : arPos;



		while (t < 1.0f) { 
			quadForeground.transform.localPosition = Vector3.Lerp (fromPos, toPos, t);

			t = t + 0.1f;
			yield return new WaitForSeconds(0.02f);
		}

		if (from == CameraState.VR) {
			Debug.Log("change camera to AR");			
			state = CameraState.AR;
			
		} else if (from == CameraState.AR) {
			Debug.Log("change camera to VR");
			state = CameraState.VR;
		}

		yield return 0;
	}
}
