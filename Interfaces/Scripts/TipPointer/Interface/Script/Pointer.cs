using UnityEngine;
using System.Collections;


public class Pointer : MonoBehaviour {

	private PointerType _type;
	private Transform _cameraTransform;


	private GameObject _pointerRendererObj;

	private UIPointer _uiPointer;


    public void Build(PointerSettings pSettings, PointerType type) {
		_type = type;

		_pointerRendererObj = new GameObject ("Renderer");
		_pointerRendererObj.transform.SetParent (gameObject.transform, false);

		_uiPointer = _pointerRendererObj.AddComponent<UIPointer> ();
		_uiPointer.Build (pSettings, _type);
	}


		/**********************************************************************/


	// Update is called once per frame
	void Update() {
		// Save current object position to interaction hashtable

	}


}
