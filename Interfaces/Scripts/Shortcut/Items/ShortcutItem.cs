using UnityEngine;
using System.Collections;

public class ShortcutItem : MonoBehaviour {
	
	public string _Label;
	public ItemType _ItemType;
	public EventScript _Action;
	
	private ShortcutItemLayer _curLayer;
	private bool _isCancelItem = false;

	public ShortcutItemLayer Layer { get { return _curLayer; } set { _curLayer = value;	} }
	public bool IsCancelItem { get { return _isCancelItem; } set { _isCancelItem = value; } }
	
	public void Build(ShortcutSettings sSettings, GameObject parentObj) {

		switch (sSettings.Type) {
		case (ShortcutType.Arc) :
			ArcItem _aItem = parentObj.AddComponent<ArcItem>();
			_aItem.transform.SetParent(parentObj.transform, false);
	
			_aItem.Label = _Label;
			_aItem.Type = _ItemType;
			_aItem.Action = _Action;
			_aItem.Layer = _curLayer;
			if (_isCancelItem) {
				_aItem.IsCancelItem = true;
			}
			_aItem.ItemObject = gameObject;

			_aItem.Build(sSettings, parentObj);
			break;
		case (ShortcutType.Stick) :
			StickItem _sItem = parentObj.AddComponent<StickItem>();
			_sItem.transform.SetParent (parentObj.transform, false);

			_sItem.Label = _Label;
			_sItem.Type = _ItemType;
			_sItem.Action = _Action;
			_sItem.Layer = _curLayer;

			if (_isCancelItem) {
				_sItem.IsCancelItem = true;
			}
			_sItem.ItemObject = gameObject;
			
			_sItem.Build(sSettings, parentObj);
			break;
		default :

			break;
		}


	}
	
}
