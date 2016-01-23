using UnityEngine;
using System.Collections;

public class StickLayer : ItemLayer {
	
	protected override void BuildItems() {
		base.BuildItems ();
		
		for (int i=0; i<items.Length; i++) {
			GameObject _uiItemObj = new GameObject("UIItem_"+items[i]._Label);
			_uiItemObj.transform.SetParent(_uiLayerObj.transform, false);

			if (_sSettings.Direction == StickShortcutDirection.Horizontal) {
				_uiItemObj.transform.localPosition = new Vector3(_sSettings.ItemWidth*i, 0, 0);
			}
			else if (_sSettings.Direction == StickShortcutDirection.Vertical) {
				_uiItemObj.transform.localPosition = new Vector3(0, -_sSettings.ItemWidth*i, 0);
			}
			items[i].Layer = gameObject.GetComponent<ShortcutItemLayer>();
			items[i].Build(_sSettings, _uiItemObj);
		}


		if (_curLevel > 1) { // in case parent item, draw cancel button
			GameObject uiCancelItemObj = new GameObject("UIItem_Cancel");
			uiCancelItemObj.transform.SetParent (_uiLayerObj.transform, false);

			if (_sSettings.Direction == StickShortcutDirection.Horizontal) {
				uiCancelItemObj.transform.localPosition = new Vector3(_sSettings.ItemWidth*items.Length, 0, 0);
			}
			else if (_sSettings.Direction == StickShortcutDirection.Vertical) {
				uiCancelItemObj.transform.localPosition = new Vector3(0, -_sSettings.ItemWidth*items.Length, 0);
			}

			ShortcutItem cancelItem = uiCancelItemObj.AddComponent<ShortcutItem>();
			cancelItem.Layer = gameObject.GetComponent<ShortcutItemLayer>();
			cancelItem._Label = _iSettings.CancelItemLabel;
			cancelItem._ItemType = ItemType.NormalButton;
			cancelItem.IsCancelItem = true;
			
			cancelItem.Build (_sSettings, uiCancelItemObj);         

			ShortcutItem[] newItems = new ShortcutItem[items.Length+1];
			
			for (int i=0; i<items.Length; i++) {
				newItems[i] = items[i];
			}
			newItems[items.Length] = cancelItem;
			
			items = newItems;
		}

	}
	
}
