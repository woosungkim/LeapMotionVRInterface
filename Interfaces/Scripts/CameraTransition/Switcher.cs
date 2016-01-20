using UnityEngine;
using System.Collections;

public class Switcher : MonoBehaviour {

	private static Switcher _instance;

	enum CameraState : int { AR, VR };

	public GameObject _target = null;
	private GameObject quadForeground;
    private CameraState state;
	private Vector3 vrPos, arPos;

	// Use this for initialization
	void Start () {
		if (_target == null) {
			Debug.Log ("no target");
		}

        state = CameraState.VR;
        vrPos = new Vector3(_target.transform.position.x, _target.transform.position.y , _target.transform.transform.position.z + 0.137f);
        arPos = new Vector3(_target.transform.position.x, _target.transform.position.y - 1.0f, _target.transform.transform.position.z + 0.137f);
        //vrPos = new Vector3(0.0f, 1.0f, 0.137f);
        //arPos = new Vector3(0.0f, 0.0f, 0.137f);
        quadForeground = GameObject.Find ("QuadForeground");
        quadForeground.transform.position = vrPos;
	}

	void Awake() {
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (_target != null) {
			// track camera position & rotation
			transform.position = _target.transform.position;
			transform.rotation = _target.transform.rotation;
          
                vrPos = new Vector3(transform.position.x, transform.position.y, transform.transform.position.z + 0.137f);
                arPos = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.transform.position.z + 0.137f);
            
           
            
		}
	}

    public static Switcher GetInstance()
    {
        return _instance;
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
		float time = 0.0f;
		Vector3 startPos, endPos;

		startPos = quadForeground.transform.position;
		endPos = (from == CameraState.AR) ? vrPos : arPos;

		while (time < 1.0f) { 
			quadForeground.transform.position = Vector3.Lerp (startPos, endPos, time);

			time = time + 0.1f;
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
