using UnityEngine;
using System.Collections;

public class ShortcutItem : MonoBehaviour {
	
	public string _Label = "Item";
	public ItemType _ItemType = ItemType.NormalButton;
	public TextAlignType _TextAlignment = TextAlignType.Center;
	public EventScript _Action = null;
	public ActionExecType _ExecType = ActionExecType.Once;
	
	private int _id;
	private ShapeItem _item;

	private ShortcutItemLayer _curLayer;
	private bool _isCancelItem = false;

	public int Id { get { return _id; } set { _id = value; } }
	public ShapeItem Item { get { return _item; } set { _item = value; } }
	public ShortcutItemLayer Layer { get { return _curLayer; } set { _curLayer = value;	} }
	public bool IsCancelItem { get { return _isCancelItem; } set { _isCancelItem = value; } }


	public void Build(ShortcutSettings sSettings, GameObject parentObj) {
		_id = ShortcutUtil.ItemAutoId;

		switch (sSettings.Type) {
		case (ShortcutType.Arc) :
			ArcItem _aItem = parentObj.AddComponent<ArcItem>();
			_item = _aItem;

			break;
		case (ShortcutType.Stick) :
			StickItem _sItem = parentObj.AddComponent<StickItem>();
			_item = _sItem;

			break;
		default :
			break;
		}

		_item.transform.SetParent (parentObj.transform, false);

		SetItemDatas ();

		_item.Build(sSettings, parentObj);
	}


	private void SetItemDatas() {
		_item.Id = _id;
		_item.Label = _Label;
		_item.Type = _ItemType;
		_item.Action = _Action;
		_item.ExecType = _ExecType;
		_item.Layer = _curLayer;
		_item.TextAlignment = _TextAlignment;
		if (_isCancelItem) {
			_item.IsCancelItem = true;
		}
		_item.ItemObject = gameObject;
	}
	
}
