using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Leap;

public class PointerController : MonoBehaviour {




	public PointerSettings _PointerSettings;

	private Camera _Camera;

	private Controller _controller;

	private Dictionary<PointerType, GameObject> _pointerDict =
		new Dictionary<PointerType, GameObject>();

	private GameObject pointersObj;
	private GameObject leftHandObj;
	private GameObject rightHandObj;

	private bool _isRightsAppearing = false;
	public bool IsRightsAppearing { get { return _isRightsAppearing; } set { _isRightsAppearing = value; } }
	private bool _isLeftsAppearing = false;
	public bool IsLeftsAppearing { get { return _isLeftsAppearing; } set { _isLeftsAppearing = value; } }

	// Use this for initialization
	void Start () {
		_Camera = Camera.main;

		if (_PointerSettings.AutoStart) {
			_isLeftsAppearing = true;
			_isRightsAppearing = true;
		} else {
			_isLeftsAppearing = false;
			_isRightsAppearing = false;
		}

		gameObject.transform.SetParent (_Camera.transform, false);

		_controller = new Controller ();

		pointersObj = new GameObject("Pointers");
		pointersObj.transform.SetParent (gameObject.transform, false);
		leftHandObj = new GameObject ("Left Hand");
		leftHandObj.transform.SetParent (pointersObj.transform, false);
		rightHandObj = new GameObject ("Right Hand");
		rightHandObj.transform.SetParent (pointersObj.transform, false);


		// 포인터의 중복설정을 확인한다.
		for (int i=0; i<_PointerSettings.PointerUsed.Length-1; i++) {
			for (int j=i+1; j<_PointerSettings.PointerUsed.Length;j++) {
				if (_PointerSettings.PointerUsed[i] == _PointerSettings.PointerUsed[j]) {
					print ("same pointers are detected");
					return;
				}
			}
		}


		// 사용하기로 설정한 포인터타입에만 포인터를 생성한다.
		foreach (PointerType type in _PointerSettings.PointerUsed) {

			// 포인터 객체를 만들고 사전에 추가 후 빌드한다.
			GameObject pointerObj = new GameObject ("Pointer_" + type);

			if (type == PointerType.LeftThumb ||
				type == PointerType.LeftIndex ||
				type == PointerType.LeftMiddle ||
				type == PointerType.LeftRing ||
				type == PointerType.LeftPinky) {
				pointerObj.transform.SetParent (leftHandObj.transform, false);
			} else {
				pointerObj.transform.SetParent (rightHandObj.transform, false);
			}


			_pointerDict.Add (type, pointerObj);
			InteractionManager.SetPointerPos (type, Vector3.one*9999.0f);
			ObjectInteractionManager.SetPointerWorldPos(type, Vector3.one*99999.0f);

			Pointer pointer = pointerObj.AddComponent<Pointer> ();
			pointer.Build (_PointerSettings, type);
		}

	}

	// Update is called once per frame
	void Update () {
		UpdateLeapPointers();
	}

	private void UpdateLeapPointers() {
		Frame frame = _controller.Frame (0);
		HandList hands = frame.Hands;

		foreach (PointerType type in _PointerSettings.PointerUsed) {
			InteractionManager.SetPointerPos (type, Vector3.one*9999.0f);
			ObjectInteractionManager.SetPointerWorldPos(type, Vector3.one*99999.0f);
		}

		// 
		bool leftHandOn = false;
		bool rightHandOn = false;

		foreach (Hand hand in hands) {
			if (hand.IsLeft && _isLeftsAppearing) {
				leftHandOn = true;
				leftHandObj.SetActive(true);
			}
			if (hand.IsRight && _isRightsAppearing) {
				rightHandOn = true;
				rightHandObj.SetActive(true);
			}

			if (hand.IsLeft && !_isLeftsAppearing) {
				continue;
			}
			if (hand.IsRight && !_isRightsAppearing) {
				continue;
			}

			FingerList fingers = hand.Fingers;

			foreach (Finger finger in fingers) {
				PointerType type = Converter.ConvertType(hand, finger.Type);

				if (type != null) {
					if (_pointerDict.ContainsKey(type)) {
						GameObject pointerObj = _pointerDict[type];

						Transform pointerTransform = pointerObj.transform;

						if (_PointerSettings.UseHandModel) {
							if (type == PointerType.RightThumb) {
								GameObject tipObj = GameObject.Find (FingerTipPath.RightThumbPath);
								InteractionManager.SetPointerPos (type,  _Camera.WorldToViewportPoint(tipObj.transform.position));
								pointerTransform.position = tipObj.transform.position;
							} else if (type == PointerType.RightIndex) {
								GameObject tipObj = GameObject.Find (FingerTipPath.RightIndexPath);
								InteractionManager.SetPointerPos (type,  _Camera.WorldToViewportPoint(tipObj.transform.position));
								pointerTransform.position = tipObj.transform.position;
							} else if (type == PointerType.RightMiddle) {
								GameObject tipObj = GameObject.Find (FingerTipPath.RightMiddlePath);
								InteractionManager.SetPointerPos (type,  _Camera.WorldToViewportPoint(tipObj.transform.position));
								pointerTransform.position = tipObj.transform.position;
							} else if (type == PointerType.RightRing) {
								GameObject tipObj = GameObject.Find (FingerTipPath.RightRingPath);
								InteractionManager.SetPointerPos (type,  _Camera.WorldToViewportPoint(tipObj.transform.position));
								pointerTransform.position = tipObj.transform.position;
							} else if (type == PointerType.RightPinky) {
								GameObject tipObj = GameObject.Find (FingerTipPath.RightPinkyPath);
								InteractionManager.SetPointerPos (type,  _Camera.WorldToViewportPoint(tipObj.transform.position));
								pointerTransform.position = tipObj.transform.position;
							} else if (type == PointerType.LeftThumb) {
								GameObject tipObj = GameObject.Find (FingerTipPath.LeftThumbPath);
								InteractionManager.SetPointerPos (type,  _Camera.WorldToViewportPoint(tipObj.transform.position));
								pointerTransform.position = tipObj.transform.position;
							} else if (type == PointerType.LeftIndex) {
								GameObject tipObj = GameObject.Find (FingerTipPath.LeftIndexPath);
								InteractionManager.SetPointerPos (type,  _Camera.WorldToViewportPoint(tipObj.transform.position));
								pointerTransform.position = tipObj.transform.position;
							} else if (type == PointerType.LeftMiddle) {
								GameObject tipObj = GameObject.Find (FingerTipPath.LeftMiddlePath);
								InteractionManager.SetPointerPos (type,  _Camera.WorldToViewportPoint(tipObj.transform.position));
								pointerTransform.position = tipObj.transform.position;
							} else if (type == PointerType.LeftRing) {
								GameObject tipObj = GameObject.Find (FingerTipPath.LeftRingPath);
								InteractionManager.SetPointerPos (type,  _Camera.WorldToViewportPoint(tipObj.transform.position));
								pointerTransform.position = tipObj.transform.position;
							} else if (type == PointerType.LeftPinky) {
								GameObject tipObj = GameObject.Find (FingerTipPath.LeftPinkyPath);
								InteractionManager.SetPointerPos (type,  _Camera.WorldToViewportPoint(tipObj.transform.position));
								pointerTransform.position = tipObj.transform.position;
							}
							ObjectInteractionManager.SetPointerWorldPos(type, pointerTransform.position);

						} else {
							if (_PointerSettings.MountType == MountType.TableMount) {
								InteractionManager.SetPointerPos (type, Converter.ConvertPosInFrustum(finger.TipPosition.ToUnity()));
								pointerTransform.position = _Camera.ViewportToWorldPoint(Converter.ConvertPosInFrustum(finger.TipPosition.ToUnity()));
								ObjectInteractionManager.SetPointerWorldPos(type, pointerTransform.position);
							} else if (_PointerSettings.MountType == MountType.HeadMount) {
								InteractionManager.SetPointerPos (type, Converter.ConvertPosInFrustumVR(finger.TipPosition.ToUnity()));
								pointerTransform.position = _Camera.ViewportToWorldPoint(Converter.ConvertPosInFrustumVR(finger.TipPosition.ToUnity()));
								ObjectInteractionManager.SetPointerWorldPos(type, pointerTransform.position);
							}
						}

						//print (Converter.ConvertPosInFrustum(finger.TipPosition.ToUnity()));

						pointerTransform.localRotation = Quaternion.identity;


						Vector3 camWorld = _Camera.transform.TransformPoint (Vector3.zero);
						Vector3 camLocal = pointerTransform.InverseTransformPoint (camWorld);

						pointerTransform.localRotation = Quaternion.FromToRotation (Vector3.down, camLocal);

					}
				}
			}
		}


		if (!leftHandOn) {
			leftHandObj.SetActive(false);
		}
		if (!rightHandOn) {
			rightHandObj.SetActive(false);
		}

	}

	public void AppearRights() {
		_isRightsAppearing = true;
	}
	public void AppearLefts() {
		_isLeftsAppearing = true;
	}

	public void DisappearRights() {
		_isRightsAppearing = false;
	}
	public void DisappearLefts() {
		_isLeftsAppearing = false;
	}

}
