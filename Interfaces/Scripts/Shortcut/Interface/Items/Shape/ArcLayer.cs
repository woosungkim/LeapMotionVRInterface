using UnityEngine;
using System.Collections;

public class ArcLayer : ItemLayer {

	protected override void BuildItems() {
		base.BuildItems ();

		for (int i=0; i<items.Length; i++) {
			GameObject _uiItemObj = new GameObject("UIItem_"+items[i]._Label);
			_uiItemObj.transform.SetParent(_uiLayerObj.transform, false);
			
			_uiItemObj.transform.localRotation = Quaternion.Euler (0, _sSettings.EachItemDegree*i, 0);

			items[i].Layer = gameObject.GetComponent<ShortcutItemLayer>();
			items[i].Build(_sSettings, _uiItemObj);
		}
		
		if (_curLevel > 1) { // in case parent item, draw cancel button
			GameObject uiCancelItemObj = new GameObject("UIItem_Cancel");
			uiCancelItemObj.transform.SetParent (_uiLayerObj.transform, false);
			uiCancelItemObj.transform.localRotation = Quaternion.Euler (0, _sSettings.EachItemDegree*items.Length, 0);
			
			ShortcutItem cancelItem = uiCancelItemObj.AddComponent<ShortcutItem>();

			cancelItem.Layer = gameObject.GetComponent<ShortcutItemLayer>();
			cancelItem._Label = _sSettings.CancelItemLabel;
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
