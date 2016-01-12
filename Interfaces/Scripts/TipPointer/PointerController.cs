using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Leap;

public class PointerController : MonoBehaviour {

	public PointerSetting _PointerSetting;
	public Transform _Camera;

	private Controller _controller;

	private Dictionary<PointerType, GameObject> _pointerDict =
			new Dictionary<PointerType, GameObject>();

	// Use this for initialization
	void Start () {
		gameObject.transform.SetParent (_Camera.transform, false);

		_controller = new Controller ();

		GameObject pointersObj = new GameObject("Pointers");
		pointersObj.transform.SetParent (gameObject.transform, false);

		// 사용하기로 설정한 포인터타입에만 포인터를 생성한다.
		foreach (PointerType type in _PointerSetting.PointerUsed) {
			// 포인터의 중복설정을 확인한다.
			if ( InteractionManager.HasPointer (type)) {
				continue;
			}

			// 포인터 객체를 만들고 사전에 추가 후 빌드한다.
			GameObject pointerObj = new GameObject("Pointer_"+type);
			pointerObj.transform.SetParent (pointersObj.transform, false);

			_pointerDict.Add (type, pointerObj);
			InteractionManager.SetPointerPos(type, pointerObj.transform.position);


			Pointer pointer = pointerObj.AddComponent<Pointer>();
			pointer.Build(type, _Camera);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = _controller.Frame (0);
		HandList hands = frame.Hands;
		
		foreach (Hand hand in hands) {
			FingerList fingers = hand.Fingers;

			foreach (Finger finger in fingers) {
				PointerType type = Converter.ConvertType(hand, finger.Type);

				if (type != null) {
					if (_pointerDict.ContainsKey(type)) {
						GameObject pointerObj = _pointerDict[type];

						Transform pointerTransform = pointerObj.transform;
						pointerTransform.position = Camera.main.ViewportToWorldPoint(Converter.ConvertPosInFrustum(finger.TipPosition.ToUnity()));
						pointerTransform.localRotation = Quaternion.identity;

                        //print(ConvertPosInFrustum(finger.TipPosition.ToUnity()));

						Vector3 camWorld = _Camera.TransformPoint (Vector3.zero);
						Vector3 camLocal = pointerTransform.InverseTransformPoint (camWorld);

						pointerTransform.localRotation = Quaternion.FromToRotation (Vector3.down, camLocal);

					}
				}

			}
		}
	

	}




}
