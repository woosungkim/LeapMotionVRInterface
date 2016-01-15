using UnityEngine;
using System.Collections;

public class ShortcutItemLayer : MonoBehaviour {

	public string _LayerName = "";


	private float _scale = 0.0f;
	private float _appearRate = 0.01f;
	private bool _appearAnimFlag = false;


    private GameObject _uiLayerObj;
	private UILayer _uiLayer;

	internal void Build(ShortcutSetting setting, GameObject parentObj) {
		
		ShortcutItem[] items = Getter.GetChildItemsFromGameObject (gameObject);

		_uiLayerObj = new GameObject ("UILayer_" + _LayerName);
		_uiLayerObj.transform.SetParent (parentObj.transform, false);
		_uiLayer = _uiLayerObj.AddComponent<UILayer> ();
		_uiLayer.AppearLayer();

		for (int i=0; i<items.Length; i++) {
			GameObject _uiItemObj = new GameObject("UIItem_"+items[i]._Label);
			_uiItemObj.transform.SetParent(_uiLayerObj.transform, false);

			_uiItemObj.transform.localRotation = Quaternion.Euler (0, setting.EachItemDegree*i, 0);

			items[i].Layer = _uiLayerObj;
			items[i].Build(setting, _uiItemObj);


		}

		/************/



	}


	void Update() {
		//if (_appearAnimFlag) {
		//	AppearAnimation ();
		//}


		if (Input.GetKey (KeyCode.LeftArrow)) {
			print ("Left Arrow");
			_uiLayer.DisappearLayer();
		}
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			print ("Right Arrow");
			_uiLayer.AppearLayer();
		}


	}
	
}
