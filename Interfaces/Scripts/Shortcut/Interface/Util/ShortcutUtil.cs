using UnityEngine;
using System.Collections;

public static class ShortcutUtil{

	private static int _autoItemId = 0;
	private static GameObject _curLayer;
	private static readonly Material _vertColorTexTwoSided =
		new Material(Shader.Find("VertColorTexTwoSided"));

	public static int ItemAutoId {
		get {
			_autoItemId++;
			return _autoItemId;
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
		Material mat = (Material)Object.Instantiate (_vertColorTexTwoSided);
		return mat;
	}

	public static Material GetTextMaterial(Material textMat) {
		Material mat = (Material)Object.Instantiate (textMat);
		return mat;
	}


}
