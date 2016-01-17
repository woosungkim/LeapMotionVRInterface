using UnityEngine;
using System.Collections;

public class ShortcutItem : MonoBehaviour {
	
	public string _Label;
	public ItemType _ItemType;
	public EventScript _Action;

	private Item _item;
	private ShortcutItemLayer _curLayer;
	private bool _isCancelItem = false;
	
	public void Build(ShortcutSettings sSettings, GameObject parentObj) {

		switch (sSettings.Type) {
		case (ShortcutType.Arc) :
			_item = gameObject.AddComponent<ArcItem>();
			_item.SetUserInputs(_Label, _ItemType, _Action);
			_item.Build(sSettings, parentObj);
			break;
		case (ShortcutType.Stick) :

			break;
		default :

			break;
		}
	}


	/* _curItem getter setter */
	public ShortcutItemLayer Layer {
		get {
			return _curLayer;
		}
		set {
			_curLayer = value;
		}
	}

	/* _isCancelItem getter setter */
	public bool IsCancelItem {
		get {
			return _isCancelItem;
		}
		set {
			_isCancelItem = value;
		}
	}
}
