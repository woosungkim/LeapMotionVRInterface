using UnityEngine;
using System.Collections;

public static class ShortcutUtil{

	private static int _autoId = 0;

	public static int ItemAutoId {
		get {
			_autoId++;
			return _autoId;
		}
	}
}
