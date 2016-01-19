using UnityEngine;
using System.Collections;

public static class ShortcutUtil{

	private static int _autoId = 0;
	private static GameObject _curLayer;
	private static readonly Material _vertColorTexTwoSided =
		new Material(Shader.Find("VertColorTexTwoSided"));
	private static Material mat = (Material)Object.Instantiate (_vertColorTexTwoSided);

	public static int ItemAutoId {
		get {
			_autoId++;
			return _autoId;
		}
	}

	public static GameObject CurrentLayerObj {
		get {
			return _curLayer;
		}
		set {
			_curLayer = value;
		}
	}

	public static Material GetMaterial() {

		return mat;
	}


}
