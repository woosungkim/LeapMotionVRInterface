using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class ObjectInteractionManager {

	static Dictionary<int, Vector3> _objectPosDict = new Dictionary<int, Vector3> ();
	static Dictionary<PointerType, Vector3> _pointerWorldPosDict = new Dictionary<PointerType, Vector3>();

	/*******************************************************************/
	
	public static void SetObjectPos(int id, Vector3 pos)
	{
		if (_objectPosDict.ContainsKey(id))	{
			_objectPosDict[id] = pos;
		} else {
			_objectPosDict.Add(id, pos);
		}
	}
	public static Vector3 GetObjectPos(int id)
	{
		if (_objectPosDict.ContainsKey(id))	{
			return _objectPosDict[id];
		} else {
			return Vector3.zero;
		}
	}

	/*******************************************************************/

	public static void SetPointerWorldPos(PointerType type, Vector3 pos) {
		if (_pointerWorldPosDict.ContainsKey (type)) {
			_pointerWorldPosDict [type] = pos;
		} else {
			_pointerWorldPosDict.Add(type, pos);
		}
	}

	public static Vector3 GetPointerWorldPos(PointerType type) {
		if (_pointerWorldPosDict.ContainsKey (type)) {
			return _pointerWorldPosDict [type];
		} else {
			return Vector3.zero;
		}
	}

	public static float findNearestPointerDistance(int id) {
		float nearestDis = 99999.0f;

		foreach (PointerType type in _pointerWorldPosDict.Keys) {
			float dis = Vector3.Distance(_objectPosDict[id], _pointerWorldPosDict[type]);

			if (nearestDis > dis) {
				nearestDis = dis;
			}
		}
		return nearestDis;
	}

}
