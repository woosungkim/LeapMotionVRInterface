using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemHierarchy : MonoBehaviour {

	internal void Build(ShortcutSetting setting) {

		ShortcutItemLayer layer = Getter.GetChildLayerFromGameObject (gameObject);

		layer.Build (setting, gameObject);

	}

	void Update()
	{


	}






}
