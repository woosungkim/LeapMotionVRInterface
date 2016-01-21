using UnityEngine;
using System.Collections;

public abstract class ShapeItem : MonoBehaviour, IShapeItem {

	protected int _id;
    protected string _label;
    protected ItemType _itemType;
    protected EventScript _action;

    protected GameObject _itemObject;
    protected ShortcutItemLayer _curLayer;
    protected bool _isCancelItem = false;


    protected ISelectableItem _selectableItem;

    protected ShortcutSettings _sSettings;
    protected ItemSettings _iSettings;
    protected GameObject _parentObj;

    protected Color _backgroundColor;
    protected Color _focusingColor;
    protected Color _selectingColor;


	public string Label {  get { return _label; } set { _label = value; } }
	public ItemType Type { get { return _itemType; } set { _itemType = value; } }
	public EventScript Action { get { return _action; } set { _action = value; } }
	
	public GameObject ItemObject { get { return _itemObject; } set { _itemObject = value; } }
	public ShortcutItemLayer Layer { get { return _curLayer; } set { _curLayer = value; } }
	public bool IsCancelItem { get {return _isCancelItem; } set { _isCancelItem = value; } }
	
	//
	
	public virtual void Build (ShortcutSettings sSettings, GameObject parentObj) {
		// variables setting
		_id = ShortcutUtil.ItemAutoId;

		_sSettings = sSettings;
		_iSettings = _sSettings.ItemSettings;
		_parentObj = parentObj;
		
		_backgroundColor = _iSettings.BackgroundColor;
		_focusingColor = _iSettings.FocusingColor;
		_selectingColor = _iSettings.SelectingColor;

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
