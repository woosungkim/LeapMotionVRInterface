using UnityEngine;
using System.Collections;

public class ShortcutItemLayer : MonoBehaviour {

	public string _LayerName = "";

	private bool _appearAnimFlag = false;

	private ItemSettings _iSettings;
	
    private GameObject _uiLayerObj;
	private UILayer _uiLayer;
	private int _curLevel = 1;
	private ShortcutItemLayer _prevLayer = null;
	private int _prevLevel = 0;


	internal void Build(ShortcutSettings sSettings, GameObject parentObj) {

		_iSettings = sSettings.ItemSettings;
		
		ShortcutItem[] items = Getter.GetChildItemsFromGameObject (gameObject);

		_uiLayerObj = new GameObject ("UILayer_" + _LayerName);
		_uiLayerObj.transform.SetParent (parentObj.transform, false);
		_uiLayer = _uiLayerObj.AddComponent<UILayer> ();
		_uiLayer.Build (sSettings);
		_uiLayer.AppearLayer(_curLevel-_prevLevel);

		for (int i=0; i<items.Length; i++) {
			GameObject _uiItemObj = new GameObject("UIItem_"+items[i]._Label);
			_uiItemObj.transform.SetParent(_uiLayerObj.transform, false);

			_uiItemObj.transform.localRotation = Quaternion.Euler (0, _iSettings.EachItemDegree*i, 0);

			items[i].Layer = gameObject.GetComponent<ShortcutItemLayer>();
			items[i].Build(sSettings, _uiItemObj);

		}

		if (_curLevel > 1) { // in case parent item, draw cancel button
			GameObject uiCancelItemObj = new GameObject("UIItem_Cancel");
			uiCancelItemObj.transform.SetParent (_uiLayerObj.transform, false);
			uiCancelItemObj.transform.localRotation = Quaternion.Euler (0, _iSettings.EachItemDegree*items.Length, 0);
			
			ShortcutItem cancelItem = uiCancelItemObj.AddComponent<ShortcutItem>();
			cancelItem.Layer = gameObject.GetComponent<ShortcutItemLayer>();
			cancelItem._Label = _iSettings.CancelItemLabel;
			cancelItem._ItemType = ItemType.NormalButton;
			cancelItem.IsCancelItem = true;

			cancelItem.Build (sSettings, uiCancelItemObj);                                    
		}

	}
	

	public int Level {
		get {
			return _curLevel;
		}
		set {
			_curLevel = value;
		}
	}

	public UILayer UILayer {
		get {
			return _uiLayer;
		}
		set {
			_uiLayer = value;
		}
	}

	public ShortcutItemLayer PrevLayer {
		get {
			return _prevLayer;
		}
		set {
			_prevLayer = value;
			_prevLevel = _prevLayer.Level;
		}

	}
	
}
