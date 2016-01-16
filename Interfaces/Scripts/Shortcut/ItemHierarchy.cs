using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemHierarchy : MonoBehaviour {

	internal void Build(ShortcutSettings sSettings, ItemSettings iSettings ) {

		ShortcutItemLayer layer = Getter.GetChildLayerFromGameObject (gameObject);

		layer.Build (sSettings, iSettings, gameObject);


	}

	void Update()
	{


	}






}
