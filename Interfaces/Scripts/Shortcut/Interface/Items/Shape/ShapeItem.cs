using UnityEngine;
using System.Collections;

public abstract class ShapeItem : MonoBehaviour, IShapeItem {

	protected int _id;
    protected string _label;
    protected ItemType _itemType;
    protected EventScript _action;
	protected ActionExecType _execType;

    protected GameObject _itemObject;
    protected ShortcutItemLayer _curLayer;
    protected bool _isCancelItem = false;

	protected bool _isNearestItem = false;

    protected ISelectableItem _selectableItem;

    protected ShortcutSettings _sSettings;
    protected GameObject _parentObj;

    protected Color _backgroundColor;
    protected Color _focusingColor;
    protected Color _selectingColor;

	protected TextAlignType _textAlignment;

	public int Id { get { return _id; } set { _id = value; } }
	public string Label {  get { return _label; } set { _label = value; } }
	public ItemType Type { get { return _itemType; } set { _itemType = value; } }
	public EventScript Action { get { return _action; } set { _action = value; } }
	public ActionExecType ExecType { get { return _execType; } set { _execType = value; } }

	public GameObject ItemObject { get { return _itemObject; } set { _itemObject = value; } }
	public ShortcutItemLayer Layer { get { return _curLayer; } set { _curLayer = value; } }
	public bool IsCancelItem { get {return _isCancelItem; } set { _isCancelItem = value; } }

	public bool IsNearestItem { get { return _isNearestItem; } set { _isNearestItem = value; } }

	public TextAlignType TextAlignment { get { return _textAlignment; } set { _textAlignment = value; } }
	//
	
	public virtual void Build (ShortcutSettings sSettings, GameObject parentObj) {
		// variables setting

		_sSettings = sSettings;
		_parentObj = parentObj;
		
		_backgroundColor = _sSettings.BackgroundColor;
		_focusingColor = _sSettings.FocusingColor;
		_selectingColor = _sSettings.SelectingColor;

		// build item as per itemtype
		BuildItemAsPerType ();
		
	}

	private void BuildItemAsPerType() {

		switch (_itemType) {
		case (ItemType.Parent) :
			ParentItem parentItem = gameObject.AddComponent<ParentItem>();
			parentItem.Layer = _curLayer;
			parentItem.ItemObject = _itemObject;
			
			parentItem.Build (_sSettings, _parentObj);
			
			_selectableItem = parentItem;
			
			break;
		case (ItemType.NormalButton) :
			NormalButtonItem normalItem = gameObject.AddComponent<NormalButtonItem>();
			normalItem.Layer = _curLayer;
			normalItem.Action = _action;
			normalItem.IsCancelItem = _isCancelItem;
			
			normalItem.Build (_sSettings, _parentObj);
			
			_selectableItem = normalItem;
			
			break;
		default :
			
			break;
		}
		
	}
	
	public abstract void Rendering();
}
