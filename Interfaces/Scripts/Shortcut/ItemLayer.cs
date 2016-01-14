using UnityEngine;
using System.Collections;

public class ItemLayer : MonoBehaviour {

	public string _LayerName = "";


	private float _scale = 0.0f;
	private float _appearRate = 0.01f;
	private bool _appearAnimFlag = false;

    private GameObject _uiLayerObj;
	private UILayer _uiLayer;

	internal void Build(ShortcutSetting setting) {
		
		ShortcutItem[] items = Getter.GetChildItemsFromGameObject (gameObject);

		_uiLayerObj = new GameObject ("UILayer_" + _LayerName);
		_uiLayerObj.transform.SetParent (gameObject.transform, false);
		_uiLayer = _uiLayerObj.AddComponent<UILayer> ();

		for (int i=0; i<items.Length; i++) {
			GameObject _uiItemObj = new GameObject("UIItem_"+items[i]._Label);
			_uiItemObj.transform.SetParent(_uiLayerObj.transform, false);

			_uiItemObj.transform.localRotation = Quaternion.Euler (0, setting.EachItemDegree*i, 0);

			items[i].Build(setting, _uiItemObj);

		}

		/************/



	}

	void OnEnable() {
		_appearAnimFlag = true;
		_scale = 0.0f;
	}

	void Update() {
		//if (_appearAnimFlag) {
		//	AppearAnimation ();
		//}


		if (Input.GetKey (KeyCode.LeftArrow)) {
			print ("Left Arrow");
			_uiLayerObj.SetActive(false);
		}
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			print ("Right Arrow");
			_uiLayerObj.SetActive(true);
		}


	}

	void AppearAnimation() {
		_scale += _appearRate;
		_uiLayerObj.transform.localScale = Vector3.one * _scale;

		if (_scale >= 1.0f) {
			_appearAnimFlag = false;

		}

	}
}
