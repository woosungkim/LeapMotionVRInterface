using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemHierarchy : MonoBehaviour {

	private ShortcutItemLayer layer;
	private bool isAppearing = true;

	private GameObject _cutLayerObj;

	internal void Build(ShortcutSettings sSettings, GameObject parentObj) {
	
		layer = Getter.GetChildLayerFromGameObject (gameObject);

		layer.Build (sSettings, gameObject);
	}


	/* Appear this shortcut. */
	public void Appear() {
		if (!isAppearing) {
			layer.UILayer.AppearLayer (layer.Level - 0);

			isAppearing = true;
		}
	}


	/* Disappear this shortcut. */
	public void Disappear() {
		if (isAppearing) {
			GameObject curLayerObj = ShortcutUtil.CurrentLayerObj;
			curLayerObj.GetComponent<UILayer>().DisappearLayer(0-1);

			isAppearing = false;
		}
	}



}
