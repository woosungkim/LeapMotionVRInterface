using UnityEngine;
using Leap;

public static class Converter {



	public static PointerType ConvertType(Hand hand, Finger.FingerType fingerType)
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


	public static Vector3 ConvertPosInFrustum(Vector3 fromPos) {
		
		Vector3 toPos = new Vector3(((fromPos.x+150.0f)/300.0f), 
		                            (fromPos.y/300.0f), 
		                            ((fromPos.z+150.0f)/300.0f));
		
		return toPos;
	}

	public static Vector3 ConvertPosInFrustumVR(Vector3 fromPos) {
		
		Vector3 toPos = new Vector3( 1-((fromPos.x+150.0f)/300.0f), 
		                            ((fromPos.z+150.0f)/300.0f),
		                            (fromPos.y/300.0f));
		
		return toPos;
	}

}