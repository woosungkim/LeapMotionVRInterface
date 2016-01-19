using UnityEngine;
using System.Collections;

public class ParentItem : MonoBehaviour, IParentItem {

	private ShortcutSettings _sSettings;
	private ShortcutItemLayer _curLayer;

	private GameObject _itemObject;

	private ShortcutItemLayer _nextLayer = null;

	/*********************************************************************/

	public ShortcutItemLayer Layer { get { return _curLayer; } set { _curLayer = value;	} }
	public GameObject ItemObject { get { return _itemObject; } set { _itemObject = value; } }

	/*********************************************************************/

	public void Build(ShortcutSettings sSettings, GameObject parentObj) {
		_sSettings = sSettings;
	}
	
	
	public void SelectAction() {

		if (_nextLayer == null) {
			_nextLayer = Getter.GetChildLayerFromGameObject (_itemObject);

			if (_nextLayer == null) { // there is no layer below this item
				print ("layer find error");
				return;
			}

			_nextLayer.Level = _curLayer.Level+1;
			_nextLayer.PrevLayer = _curLayer;

			_nextLayer.Build (_sSettings, _itemObject);

		}
		else {
			_nextLayer.UILayer.AppearLayer(_nextLayer.Level - _curLayer.Level);
		}
		_curLayer.UILayer.DisappearLayer(_nextLayer.Level - _curLayer.Level);

	}

}
