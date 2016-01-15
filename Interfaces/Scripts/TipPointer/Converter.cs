using UnityEngine;
using Leap;

public static class Converter {



	public static PointerType ConvertType(Hand hand, Finger.FingerType fingerType)
	{
		if (hand.IsRight) {
			switch (fingerType) {
			case Finger.FingerType.TYPE_THUMB: return PointerType.RightThumb;
			case Finger.FingerType.TYPE_INDEX: return PointerType.RightIndex;
			case Finger.FingerType.TYPE_MIDDLE: return PointerType.RightMiddle;
			case Finger.FingerType.TYPE_RING: return PointerType.RightRing;
			case Finger.FingerType.TYPE_PINKY: return PointerType.RightPinky;
			}
		}
		
		return PointerType.Null;
		
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