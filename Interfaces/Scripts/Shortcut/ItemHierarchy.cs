using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemHierarchy : MonoBehaviour {

	internal void Build(ShortcutSettings sSettings, ShortcutItemSettings iSettings ) {

		ShortcutItemLayer layer = Getter.GetChildLayerFromGameObject (gameObject);

		layer.Build (sSettings, iSettings, gameObject);

	}

	void Update()
	{


	}






}
