using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemHierarchy : MonoBehaviour {

	internal void Build(ShortcutSetting setting) {

		ItemLayer layer = Getter.GetChildLayerFromGameObject (gameObject);

		layer.Build (setting);



	}

	void Update()
	{


	}






}
