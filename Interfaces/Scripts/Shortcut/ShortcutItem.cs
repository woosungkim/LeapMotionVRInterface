using UnityEngine;
using System.Collections;
using System;

public class ShortcutItem : MonoBehaviour {
	
	public ItemType _ItemType;
	public string _Label = "";
	public EventScript action;

	private Color backgroundColor = new Color (0.1f, 0.1f, 0.1f, 0.5f);
	private Color focusingColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	private Color selectingColor = new Color (0.8f, 0.8f, 0.8f, 0.5f);


	private int _id;

	private ShortcutSetting _setting;

	private GameObject rendererObj;
	private GameObject backgroundObj;
	private GameObject focusingObj;
	private GameObject selectingObj;
	private GameObject labelObj;
	private GameObject centerObj;

	private UIArcItem _uiArcItemBg;
	private UIArcItem _uiArcItemFs; 
	private UIArcItem _uiArcItemSt;
	private UILabel _uiLabel;


	private float _innerRadius;
	private float _outerRadius;
	private float _thickness;

	private float _selectProg = 0.0f;

	internal void Build(ShortcutSetting setting, GameObject uiItem) {
		_id = ShortcutUtil.ItemAutoId;
		_setting = setting;

		_innerRadius = setting.InnerRadius;
		_outerRadius = setting.InnerRadius + setting.Thickness;
		_thickness = setting.Thickness;

		float toDegree = 180 / (float)Mathf.PI;
		float eachItemAngle = setting.EachItemDegree / toDegree;
		
		float startAngle = -(eachItemAngle / 2);
		float endAngle = eachItemAngle / 2;

		// make renderer gameobject
		rendererObj = new GameObject ("Renderer");
		rendererObj.transform.SetParent (uiItem.transform, false);

		// build arc item background ui
		backgroundObj = new GameObject ("Background");
		backgroundObj.transform.SetParent (rendererObj.transform, false);
		_uiArcItemBg = backgroundObj.AddComponent<UIArcItem> ();
		_uiArcItemBg.Build (setting, _id);
		_uiArcItemBg.UpdateMesh(_innerRadius, _outerRadius, backgroundColor);

		focusingObj = new GameObject ("Focusing");
		focusingObj.transform.SetParent (rendererObj.transform, false);
		_uiArcItemFs = focusingObj.AddComponent<UIArcItem> ();
		_uiArcItemFs.Build (setting, _id);

		selectingObj = new GameObject ("Selecting");
		selectingObj.transform.SetParent (rendererObj.transform, false);
		_uiArcItemSt = selectingObj.AddComponent<UIArcItem> ();
		_uiArcItemSt.Build (setting, _id);

		// build item label ui
		labelObj = new GameObject ("Label");
		labelObj.transform.SetParent(rendererObj.transform, false);
		labelObj.transform.localPosition = new Vector3(0, 0, setting.InnerRadius);
		labelObj.transform.localRotation = Quaternion.FromToRotation(Vector3.back, Vector3.right);
		labelObj.transform.localScale = new Vector3(1, 1, 1);

		centerObj = new GameObject("Center");
		centerObj.transform.SetParent (rendererObj.transform, false);
		centerObj.transform.localPosition = new Vector3 (0, 0, setting.InnerRadius + (setting.Thickness / 2));


		_uiLabel = labelObj.AddComponent<UILabel>();
		_uiLabel.Label = " "+_Label;
		_uiLabel.Font = setting.FontName;
		_uiLabel.Color = Color.black;

		// interaction setting
		InteractionManager.SetItemPos (_id, Camera.main.WorldToViewportPoint(centerObj.transform.position));

	}


	void Update() {

		if (InteractionManager.HasItemId (_id)) {
			InteractionManager.SetItemPos (_id, Camera.main.WorldToViewportPoint(centerObj.transform.position));
			//print (Camera.main.WorldToViewportPoint(labelObj.transform.position));
			float prog = InteractionManager.GetItemHighlightProgress(_id);

			float focusStart = 0.8f;
			if (prog > focusStart) {
				float focusProg = Mathf.Lerp (0, 1, prog-focusStart);

				// focus event
				if (focusProg == 1)	{
					if (_selectProg < 1.0f) {
						_selectProg += 0.01f;
					}
					_uiArcItemBg.UpdateMesh (0.0f, 0.0f, backgroundColor);
					_uiArcItemFs.UpdateMesh (_innerRadius+(_thickness*_selectProg), _innerRadius+(_thickness*focusProg), focusingColor);
					_uiArcItemSt.UpdateMesh (_innerRadius, _innerRadius+(_thickness*_selectProg), selectingColor);

					// item click evnet
					if (_selectProg >= 1.0f) {
						if (action != null) {
							action.ClickAction();
						}
					}
				}
				else {
					_selectProg = 0.0f;

					_uiArcItemBg.UpdateMesh(_innerRadius+(_thickness*focusProg), _outerRadius, backgroundColor);
					_uiArcItemFs.UpdateMesh (_innerRadius, _innerRadius+(_thickness*focusProg), focusingColor);
					_uiArcItemSt.UpdateMesh (0.0f, 0.0f, selectingColor);
				}

			}
			else {
				_uiArcItemBg.UpdateMesh(_innerRadius, _outerRadius, backgroundColor);
				_uiArcItemFs.UpdateMesh (0.0f, 0.0f, focusingColor);
				_uiArcItemSt.UpdateMesh (0.0f, 0.0f, selectingColor);
			}
			//float highPercent = Mathf.Lerp (0, 1, progress);


		}

	}
}
