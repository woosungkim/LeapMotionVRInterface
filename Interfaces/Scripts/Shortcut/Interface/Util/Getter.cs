using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Getter {


	public static ShortcutItemLayer GetChildLayerFromGameObject(GameObject parentObj)
	{
		Transform transform = parentObj.transform;
		if (transform.childCount == 0) { 
			return null;
		}

		ShortcutItemLayer layer = transform.GetChild (0).GetComponent<ShortcutItemLayer>();
		
		return layer;
	}


	public static ShortcutItem[] GetChildItemsFromGameObject(GameObject parentObj)
	{
		Transform transform = parentObj.transform;
		List<ShortcutItem> items = new List<ShortcutItem> ();
		
		for (int i=0; i<transform.childCount; i++) {
			ShortcutItem item = transform.GetChild (i).GetComponent<ShortcutItem>();
			
			items.Add (item);
		}
		
		return items.ToArray ();
	}
}
