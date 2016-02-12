using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class InteractionManager {

	private static float TriggerDistance = 0.2f;
	
	static Dictionary<PointerType, Vector3> _pointerPosDict = new Dictionary<PointerType, Vector3>();
	static Dictionary<int, Vector3> _itemPosDict = new Dictionary<int, Vector3> ();
    static Dictionary<int, float> _interactionStartDict = new Dictionary<int, float> ();
	static Dictionary<int, float> _progressDict = new Dictionary<int, float> ();


	/*******************************************************************/
	public static bool HasPointer(PointerType type) {
		if (_pointerPosDict.ContainsKey (type))
			return true;
		return false;
	}
	public static void SetPointerPos(PointerType type, Vector3 pos) {
		if (_pointerPosDict.ContainsKey (type)) {
			_pointerPosDict [type] = pos;
		} else {
			_pointerPosDict.Add (type, pos);
		}
	}
	public static Vector3 GetPointerPos(PointerType type) {
		if (_pointerPosDict.ContainsKey (type)) {
			return _pointerPosDict[type];
		} else {
			return Vector3.zero;
		}
	}

	/*******************************************************************/
	public static bool HasItemId(int id) {
		if (_itemPosDict.ContainsKey (id))
			return true;
		return false;
	}
	public static void SetItemPos(int id, Vector3 pos) {
		if (_itemPosDict.ContainsKey (id)) {
			_itemPosDict [id] = pos;
		} else {
			_itemPosDict.Add (id, pos);
		}
	}
	public static Vector3 GetItemPos(int id) {
		if (_itemPosDict.ContainsKey (id)) {
			return _itemPosDict[id];
		} else {
			return Vector3.zero;
		}
	}

    /*******************************************************************/

    public static bool HasInterStartId(int id) {
        if (_interactionStartDict.ContainsKey (id)) 
			return true;
		return false;
    }
	public static void SetInterStart(int id, float val) { 
		if (_interactionStartDict.ContainsKey (id)) {
			_interactionStartDict [id] = val;
		} else {
			_interactionStartDict.Add(id, val);
		}
	}
	public static float GetInterStart(int id) {
		if (_interactionStartDict.ContainsKey (id)) {
			return _interactionStartDict [id];
		} else {
			return -1.0f;
		}
	}
	
    /*******************************************************************/
	public static bool HasProgId(int id) {
		if (_progressDict.ContainsKey (id))
			return true;
		return false;
	}
	public static void SetItemProg(int id, float prog) {
		if (_progressDict.ContainsKey (id)) {
			_progressDict [id] = prog;
		} else {
			_progressDict.Add (id, prog);
		}
	}
	public static float GetItemProg(int id) {
		if (_progressDict.ContainsKey (id)) {
			return _progressDict[id];
		} else {
			return -1.0f;
		}
	}

	/*******************************************************************/
	public static float findNearestPointerDistance(int id) {
		float nearestDis = 999.0f;

		foreach (PointerType type in _pointerPosDict.Keys) {
			float intervalStart =  _interactionStartDict[id];
			float intervalEnd = (_interactionStartDict[id]*3.0f);
			float interval = intervalEnd - intervalStart;

            float dis = Vector2.Distance((Vector2)_itemPosDict[id], (Vector2)_pointerPosDict[type]);
           
			if (intervalEnd > dis) {
				dis -= intervalStart;
				dis /= interval;
                nearestDis = Math.Min (nearestDis, dis);
				//Mathf.Lerp (0, 1, (1-nearestDis));
            }
			//Debug.Log ("Item : " + (Vector2)pos);
		}
		nearestDis = Mathf.Lerp (0, 1, nearestDis);

		return nearestDis;
	}

	public static float findNearestItemDistance(Vector3 pos) {
		float nearestDis = 999.0f;

		foreach (int id in _itemPosDict.Keys) {
			float intervalStart =  _interactionStartDict[id];
			float intervalEnd = (_interactionStartDict[id]*3.0f);
			float interval = intervalEnd - intervalStart;

			float dis = Vector2.Distance((Vector2)pos, (Vector2)_itemPosDict[id]);

			if (intervalEnd > dis) {
				//float normDis   = Mathf.Lerp (intervalStart, intervalEnd, dis);
				dis -= intervalStart;
				dis /= interval;

				nearestDis = (Math.Min (nearestDis, dis)); 
			}
		}

		return nearestDis;
	}



	public static float GetItemHighlightProgress(int id) {
		float nearestDis = findNearestPointerDistance (id);

		return (_interactionStartDict[id] / nearestDis);

	}

	public static float GetPointerHighlightProgress(PointerType type) {

		float nearestDis = findNearestItemDistance (_pointerPosDict [type]);

		return nearestDis;

	}


}


/*
 * float nearestDis = 999.0f;

		foreach (PointerType type in _pointerPosDict.Keys) {
	        float intervalStart =  _interactionStartDict[id];
			float intervalEnd = (_interactionStartDict[id]*3.0f);
			float interval = intervalEnd - intervalStart;

            float dis = Vector2.Distance((Vector2)_itemPosDict[id], (Vector2)_pointerPosDict[type]);
           
			if (intervalEnd > dis) {
				dis -= intervalStart;
				dis /= interval;
                nearestDis = Math.Min (nearestDis, dis);
				//Mathf.Lerp (0, 1, (1-nearestDis));
            }
			//Debug.Log ("Item : " + (Vector2)pos);
		}
		nearestDis = Mathf.Lerp (0, 1, nearestDis);

*/