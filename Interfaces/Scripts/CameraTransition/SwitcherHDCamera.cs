using UnityEngine;
using System.Collections;

public class SwitcherHDCamera : MonoBehaviour {

	private static SwitcherHDCamera _instance;
    
	enum CameraState : int { AR, VR };

	public GameObject _target = null;
	private GameObject plane;
    private CameraState state;
	private Vector3 vrPos, arPos;
    private Vector3 tempPos;
    private bool isPlaying = false;

	// Use this for initialization
	void Start () {
		if (_target == null) {
			Debug.Log ("no target");
		}

        state = CameraState.VR;
        arPos = new Vector3(_target.transform.position.x, _target.transform.position.y , _target.transform.transform.position.z + 0.5f);
        vrPos = new Vector3(_target.transform.position.x, _target.transform.position.y - 0.8f, _target.transform.transform.position.z + 0.5f);
        plane = GameObject.Find ("Plane");
        plane.transform.position = vrPos;
        plane.GetComponent<Renderer>().enabled = true;
	}

	void Awake() {
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (_target != null) {
			// track camera position & rotation
            transform.position =  Vector3.Lerp( _target.transform.position - (_target.transform.forward * 0.1f),transform.position, 5f * Time.deltaTime);
            transform.rotation = _target.transform.rotation;
            if(state == CameraState.AR)
            {
                tempPos = plane.transform.position;
            }
            else
            {
                tempPos = new Vector3(plane.transform.position.x, plane.transform.position.y + 0.8f, plane.transform.position.z);
            }

            arPos = tempPos;
            vrPos = new Vector3(tempPos.x, tempPos.y - 0.8f, tempPos.z);
            
           
            
		}
	}

    public static SwitcherHDCamera GetInstance()
    {
        return _instance;
    }
    
	public void switchCamera() {
		Debug.Log ("switch camera");
		StartCoroutine (switchCameraRutine( state ));

	}
    
	public void switchCameraToVR() {
        if(!isPlaying && this.state!=CameraState.VR)
        {
            isPlaying = true;
            StartCoroutine(switchCameraRutine(CameraState.AR));
        }
		    
	}

	public void switchCameraToAR() {
        if(!isPlaying && this.state!=CameraState.AR)
        {
            isPlaying = true;
            StartCoroutine(switchCameraRutine(CameraState.VR));
        }
		    
	}

	IEnumerator switchCameraRutine( CameraState from ) {
		float time = 0.0f;
		Vector3 startPos, endPos;

        startPos = plane.transform.position;
		endPos = (from == CameraState.AR) ? vrPos : arPos;

		while (time < 1.0f) {
            plane.transform.position = Vector3.Lerp(startPos, endPos, time);

			time = time + 0.1f;
			yield return new WaitForSeconds(0.02f);
		}

		if (from == CameraState.VR) {
			Debug.Log("change camera to AR");
            isPlaying = false;
			state = CameraState.AR;
			
		} else if (from == CameraState.AR) {
			Debug.Log("change camera to VR");
            isPlaying = false;
			state = CameraState.VR;
		}

		yield return 0;
	}
}
