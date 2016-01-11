using UnityEngine;
using System.Collections;

public class ItemLayer : MonoBehaviour {

	public string _LayerName = "";

	internal void Build(ShortcutSetting setting, string path) {
		ShortcutItem[] items = Getter.GetChildItemsFromGameObject (gameObject);

		for (int i=0; i<items.Length; i++) {
				items[i].transform.localRotation = Quaternion.Euler (0, setting.EachItemDegree*i, 0);
				
				items[i].Build(setting, path+_LayerName);

		}

	}
}
