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

	
	private float _innerRadius;
	private float _outerRadius;
	private float _thickness;


	private int _id;

	private ShortcutSetting _setting;

	private GameObject _parentObj;

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

	
	private float _selectProg = 0.0f;
	private float _selectSpeed = 0.01f;
	private bool _isSelected = false;

	private GameObject _curlayer;
	public GameObject Layer {
		get {
			return _curlayer;
		}
		set {
			_curlayer = value;
		}
	}


	internal void Build(ShortcutSetting setting, GameObject parentObj) {
		/***** variables setting *****/
		_id = ShortcutUtil.ItemAutoId;

		_setting = setting;
		_parentObj = parentObj;

		_innerRadius = _setting.InnerRadius;
		_outerRadius = _setting.InnerRadius + _setting.Thickness;
		_thickness = _setting.Thickness;

		// rendering
		Rendering ();

		// interaction setting
		InteractionManager.SetItemPos (_id, Camera.main.WorldToViewportPoint(centerObj.transform.position));

	}


	void Update() {
		if (_curlayer != null) {
			if (InteractionManager.HasItemId (_id)) {
				if (_curlayer.GetComponent<UILayer> ().IsCurrentLayer) {
					// register this item pos to interaction manager
					InteractionManager.SetItemPos (_id, Camera.main.WorldToViewportPoint (centerObj.transform.position));
					
					
					/***** focus, select ui update *****/
					float prog = InteractionManager.GetItemHighlightProgress (_id);
					
					float focusStart = 0.8f; // trigger focus percent = 80%
					if (prog > focusStart) { // is focusing
						float focusProg = Mathf.Lerp (0, 1, prog - focusStart);
						
						// focus, select event process
						if (focusProg == 1) { // all focus, is selecting
							if (_selectProg < 1.0f) {
								_selectProg += _selectSpeed;
							}
							
							// item click evnet
							if (_selectProg >= 1.0f) {
								_selectProg = 1.0f;
								if (!_isSelected) { // select action is triggered just once
									_isSelected = true;
									SelectAction();
								}
								
							}
							// select ui update
							_uiArcItemBg.UpdateMesh (0.0f, 0.0f, backgroundColor);
							_uiArcItemFs.UpdateMesh (_innerRadius + (_thickness * _selectProg), _innerRadius + (_thickness * focusProg), focusingColor);
							_uiArcItemSt.UpdateMesh (_innerRadius, _innerRadius + (_thickness * _selectProg), selectingColor);
							
						} else {
							_selectProg = 0.0f;
							_isSelected = false;
							// focus ui update
							_uiArcItemBg.UpdateMesh (_innerRadius + (_thickness * focusProg), _outerRadius, backgroundColor);
							_uiArcItemFs.UpdateMesh (_innerRadius, _innerRadius + (_thickness * focusProg), focusingColor);
							_uiArcItemSt.UpdateMesh (0.0f, 0.0f, selectingColor);
						}
						
					} else { // is non focusing
						// general ui update
						_uiArcItemBg.UpdateMesh (_innerRadius, _outerRadius, backgroundColor);
						_uiArcItemFs.UpdateMesh (0.0f, 0.0f, focusingColor);
						_uiArcItemSt.UpdateMesh (0.0f, 0.0f, selectingColor);
					}
					
					/****************************************/
				
				}
				else {
					// register this item pos to interaction manager
					InteractionManager.SetItemPos (_id, Vector3.one*999);

				}

			}
		}
	}

	private void SelectAction() {
		switch (_ItemType) {
		case (ItemType.Parent) :
			ShortcutItemLayer layer = Getter.GetChildLayerFromGameObject (gameObject);
			layer.Build (_setting, gameObject);
			_curlayer.GetComponent<UILayer>().DisappearLayer();
			break;
		case (ItemType.NormalButton) :
			if (action != null) {
				action.ClickAction ();
			}
			break;
		default :
			break;
		}

	}

	private void Rendering() {
		/***** rendering *****/
		// make renderer gameobject
		rendererObj = new GameObject ("Renderer");
		rendererObj.transform.SetParent (_parentObj.transform, false);

		// center pivot position setting
		centerObj = new GameObject("Center");
		centerObj.transform.SetParent (rendererObj.transform, false);
		centerObj.transform.localPosition = new Vector3 (0, 0, _setting.InnerRadius + (_setting.Thickness / 2));
		
		// build arc item background ui
		backgroundObj = new GameObject ("Background");
		backgroundObj.transform.SetParent (rendererObj.transform, false);
		_uiArcItemBg = backgroundObj.AddComponent<UIArcItem> ();
		_uiArcItemBg.Build (_setting);
		_uiArcItemBg.UpdateMesh(_innerRadius, _outerRadius, backgroundColor);
		
		focusingObj = new GameObject ("Focusing");
		focusingObj.transform.SetParent (rendererObj.transform, false);
		_uiArcItemFs = focusingObj.AddComponent<UIArcItem> ();
		_uiArcItemFs.Build (_setting);
		
		selectingObj = new GameObject ("Selecting");
		selectingObj.transform.SetParent (rendererObj.transform, false);
		_uiArcItemSt = selectingObj.AddComponent<UIArcItem> ();
		_uiArcItemSt.Build (_setting);
		
		// build item label ui
		labelObj = new GameObject ("Label");
		labelObj.transform.SetParent(rendererObj.transform, false);
		labelObj.transform.localPosition = new Vector3(0, 0, _setting.InnerRadius);
		labelObj.transform.localRotation = Quaternion.FromToRotation(Vector3.back, Vector3.right);
		labelObj.transform.localScale = new Vector3(1, 1, 1);
		
		_uiLabel = labelObj.AddComponent<UILabel>();
		_uiLabel.Label = " "+_Label;
		_uiLabel.Font = _setting.FontName;
		_uiLabel.Color = Color.black;


	}
}
