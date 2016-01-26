using UnityEngine;
using System.Collections;

public class ShortcutItemLayer : MonoBehaviour {

	public string _LayerName = "";

	private ItemLayer _itemLayer;
	private int _level = 1;
	private ShortcutItemLayer _prevLayer = null;

	internal void Build(ShortcutSettings sSettings, GameObject parentObj) {

		switch (sSettings.Type) {
		case (ShortcutType.Arc) :
			_itemLayer = gameObject.AddComponent<ArcLayer>();
			
			_itemLayer.LayerName = _LayerName;
			_itemLayer.Level = _level;
			_itemLayer.PrevLayer = _prevLayer;

			_itemLayer.Build (sSettings, parentObj);

			break;
		case (ShortcutType.Stick) :
			_itemLayer = gameObject.AddComponent<StickLayer>();

			_itemLayer.LayerName = _LayerName;
			_itemLayer.Level = _level;
			_itemLayer.PrevLayer = _prevLayer;

			_itemLayer.Build (sSettings, parentObj);

			break;
		default :
			
			break;
		}

	}

	public int Level { get { return _level; } set { _level = value; } }
	public UILayer UILayer { get { return _itemLayer.UILayer; } set {	_itemLayer.UILayer = value; } }
	public ShortcutItemLayer PrevLayer { get { return _prevLayer; }	set { _prevLayer = value; }	}
	
}
