using UnityEngine;
using System.Collections;

public static class SelectableObjectUtil { 

	private static int _autoItemId = 0;


	public static int AutoItemId {
		get {
			_autoItemId++;
			return _autoItemId;
		}
	}

}