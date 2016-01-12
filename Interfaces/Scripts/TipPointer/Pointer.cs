using UnityEngine;
using System.Collections;


public class Pointer : MonoBehaviour {

	private PointerType _type;
	private Transform _cameraTransform;


	private GameObject _pointerRendererObj;

	private UIPointer _uiPointer;


    public void Build(PointerType type, Transform cameraTransform) {
		_type = type;
		_cameraTransform = cameraTransform;

		_pointerRendererObj = new GameObject ("PointerRenderer");
		_pointerRendererObj.transform.SetParent (gameObject.transform, false);

		_uiPointer = _pointerRendererObj.AddComponent<UIPointer> ();
		_uiPointer.Build (_type);
	}


		/**********************************************************************/


	// Update is called once per frame
	void Update() {
		// Save current object position to interaction hashtable
		InteractionManager.SetPointerPos (_type, gameObject.transform.position);



	}


}
