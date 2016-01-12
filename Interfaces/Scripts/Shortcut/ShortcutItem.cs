using UnityEngine;
using System.Collections;
using System;

public class ShortcutItem : MonoBehaviour {
	
	public ItemType _ItemType;
	public string _Label = "";


	private int _id;


	private string _path;
	
	private UIArcItem _uiArcItem;
	private UILabel _uiLabel;

	private GameObject backgroundObj;
	private GameObject labelObj;
	
	internal void Build(ShortcutSetting setting) {
		_id = ShortcutUtil.ItemAutoId;

		float toDegree = 180 / (float)Mathf.PI;
		float eachItemAngle = setting.EachItemDegree / toDegree;
		
		float startAngle = -(eachItemAngle / 2);
		float endAngle = eachItemAngle / 2;

		// build arc item background ui
		backgroundObj = new GameObject ("Background");
		backgroundObj.transform.SetParent (gameObject.transform, false);
		_uiArcItem = backgroundObj.AddComponent<UIArcItem> ();
		_uiArcItem.Build (setting);

		// build item label ui
		labelObj = new GameObject ("Label");
		labelObj.transform.SetParent(gameObject.transform, false);
		labelObj.transform.localPosition = new Vector3(0, 0, setting.InnerRadius);
		labelObj.transform.localRotation = Quaternion.FromToRotation(Vector3.back, Vector3.right);
		labelObj.transform.localScale = new Vector3(1, 1, 1);

		_uiLabel = labelObj.AddComponent<UILabel>();
		_uiLabel.Label = " "+_Label;
	

		// interaction setting
		InteractionManager.SetItemPos (_id, labelObj.transform.position);
	}


	void Update() {

		if (InteractionManager.HasItemId (_id)) {
			InteractionManager.SetItemPos (_id, labelObj.transform.position);
			print(InteractionManager.GetItemPos (_id));
		}

	}
}
