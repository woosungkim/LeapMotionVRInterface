using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class InteractionManager {

	static Dictionary<PointerType, Vector3> _pointerPosDict = new Dictionary<PointerType, Vector3>();


	public static bool HasPointer(PointerType type) {
		if (_pointerPosDict.ContainsKey (type))
			return true;
		return false;
	}

	public static void SetPointerPos(PointerType type, Vector3 pos)
	{
		if (_pointerPosDict.ContainsKey (type)) {
			_pointerPosDict [type] = pos;
		} else {
			_pointerPosDict.Add (type, pos);
		}
	}

	public static Vector3 GetPointerPos(PointerType type)
	{
		if (_pointerPosDict.ContainsKey (type)) {
			return _pointerPosDict[type];
		} else {
			return Vector3.zero;
		}
	}

}
