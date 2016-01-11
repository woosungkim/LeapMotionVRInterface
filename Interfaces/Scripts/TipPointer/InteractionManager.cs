using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class InteractionManager {

	static Dictionary<PointerType, Vector3> _pointerPosDict = new Dictionary<PointerType, Vector3>();
	static Dictionary<string, Vector3> _itemPosDict = new Dictionary<string, Vector3> ();

	public static bool HasPointer(PointerType type) 
	{
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


	public static bool HasItemPath(string path) 
	{
		if (_itemPosDict.ContainsKey (path))
			return true;
		return false;
	}

	public static void SetItemPos(string path, Vector3 pos)
	{
		if (_itemPosDict.ContainsKey (path)) {
			_itemPosDict [path] = pos;
		} else {
			_itemPosDict.Add (path, pos);
		}
	}

	public static Vector3 GetItemPos(string path)
	{
		if (_itemPosDict.ContainsKey (path)) {
			return _itemPosDict[path];
		} else {
			return Vector3.zero;
		}
	}

}
