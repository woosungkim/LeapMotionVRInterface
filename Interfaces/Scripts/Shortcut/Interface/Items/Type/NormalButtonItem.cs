using UnityEngine;
using System.Collections;

public class NormalButtonItem : MonoBehaviour, INormalButtonItem {

	private ShortcutItemLayer _curLayer;
	private EventScript _action;

	private bool _isCancelItem = false;
	private string _cancelItemLabel = "";

	/*********************************************************************/

	public ShortcutItemLayer Layer { get { return _curLayer; } set { _curLayer = value;	} }
	public EventScript Action { get { return _action; } set { _action = value; } }
	public bool IsCancelItem { get { return _isCancelItem; } set { _isCancelItem = value; } }
	public string CancelItemLabel { get { return _cancelItemLabel; } set { _cancelItemLabel = value; } }

	/*********************************************************************/

	public void Build(ShortcutSettings sSettings, GameObject parentObj) {

	}

	public void SelectAction() {
		if (_isCancelItem) { // action for cancel item
			ShortcutItemLayer prevLayer = _curLayer.PrevLayer;
			prevLayer.UILayer.AppearLayer (prevLayer.Level - _curLayer.Level);
			_curLayer.UILayer.DisappearLayer (prevLayer.Level - _curLayer.Level);

		}
		else { 
			if (_action != null) {
				_action.ClickAction ();
			}
			else {
				Debug.Log ("action script is empty");
			}
		}
		
	}




}
