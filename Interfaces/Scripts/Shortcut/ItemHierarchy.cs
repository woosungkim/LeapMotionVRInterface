using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemHierarchy : MonoBehaviour {
	
	private ShortcutItemLayer layer;
	private bool isAppearing = false;

	private GameObject _curLayerObj;

	public GameObject CurLayerObj { get { return _curLayerObj; } set { _curLayerObj = value; } }

	internal void Build(ShortcutSettings sSettings, GameObject parentObj) {
	
		layer = Getter.GetChildLayerFromGameObject (gameObject);
		if (layer == null) {
			print ("layer find error");
			return;
		}
		isAppearing = true;

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
			_curLayerObj.GetComponent<UILayer>().DisappearLayer(0-1);

			isAppearing = false;
		}
	}



}
