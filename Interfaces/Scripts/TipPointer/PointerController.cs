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
				PointerType type = ConvertType(hand, finger.Type);

				if (type != null) {
					if (_pointerDict.ContainsKey(type)) {
						GameObject pointerObj = _pointerDict[type];

						Transform pointerTransform = pointerObj.transform;
						pointerTransform.localPosition = Camera.main.ViewportToWorldPoint(ConvertPosInFrustum(finger.TipPosition.ToUnity()));
						pointerTransform.localRotation = Quaternion.identity;

						Vector3 camWorld = _Camera.TransformPoint (Vector3.zero);
						Vector3 camLocal = pointerTransform.InverseTransformPoint (camWorld);
						pointerTransform.localRotation = Quaternion.FromToRotation (Vector3.down, camLocal);

						if (type == PointerType.RIGHT_INDEX) {
							print(InteractionManager.GetPointerPos(type));

						}

					}
				}

			}
		}
	

	}

	private PointerType ConvertType(Hand hand, Finger.FingerType fingerType)
	{
		if (hand.IsRight) {
			switch (fingerType) {
			case Finger.FingerType.TYPE_THUMB: return PointerType.RIGHT_THUMB;
			case Finger.FingerType.TYPE_INDEX: return PointerType.RIGHT_INDEX;
			case Finger.FingerType.TYPE_MIDDLE: return PointerType.RIGHT_MIDDLE;
			case Finger.FingerType.TYPE_RING: return PointerType.RIGHT_RING;
			case Finger.FingerType.TYPE_PINKY: return PointerType.RIGHT_PINKY;
			}
		}

		return PointerType.NULL;

	}

	private Vector3 ConvertPosInFrustum(Vector3 fromPos) {
		
		Vector3 toPos = new Vector3(((fromPos.x+150.0f)/300.0f), 
		                            (fromPos.y/300.0f), 
		                            ((fromPos.z+150.0f)/300.0f));
		
		return toPos;
	}

}
