using UnityEngine;
using System.Collections;

public class ArcItem : ShapeItem {
	
	private GameObject rendererObj;
	private GameObject backgroundObj;
	private GameObject focusingObj;
	private GameObject selectingObj;
	private GameObject labelObj;
	private GameObject centerObj;
	
	private UIArcItem _uiArcItemBg;
	private UIArcItem _uiArcItemFs; 
	private UIArcItem _uiArcItemSt;
	

	private float _innerRadius;
	private float _outerRadius;
	private float _thickness;


	private float _selectProg = 0.0f;
	private float _selectSpeed = 0.02f;
	private bool _isSelected = false;

	public override void Build (ShortcutSettings sSettings, GameObject parentObj)
	{
		base.Build (sSettings, parentObj);

		_selectSpeed = _iSettings.SelectSpeed;

		_innerRadius = _sSettings.InnerRadius;
		_outerRadius = _sSettings.InnerRadius + _sSettings.Thickness;
		_thickness = _sSettings.Thickness;

		// rendering
		Rendering ();

		// interaction setting
		InteractionManager.SetItemPos (_id, Camera.main.WorldToViewportPoint (centerObj.transform.position));
	}
	

	// Update is called once per frame
	void Update () {
		if (_curLayer != null) {
			if (InteractionManager.HasItemId (_id)) {
				if (_curLayer.UILayer.IsCurrentLayer) {
					// register this item pos to interaction manager
					InteractionManager.SetItemPos (_id, Camera.main.WorldToViewportPoint (centerObj.transform.position));
					
					
					/***** focus, select ui update *****/
					float progress = InteractionManager.GetItemHighlightProgress (_id);
					
					float focusStart = _iSettings.FocusStart; // trigger focus percent = 80%
					if (progress > focusStart) { // is focusing
						float focusProg = Mathf.Lerp (0, 1, progress - focusStart);
						
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
									_selectableItem.SelectAction();
								}
							}
							// select ui update
							_uiArcItemBg.UpdateMesh (0.0f, 0.0f, _backgroundColor);
							_uiArcItemFs.UpdateMesh (_innerRadius + (_thickness * _selectProg), _innerRadius + (_thickness * focusProg), _focusingColor);
							_uiArcItemSt.UpdateMesh (_innerRadius, _innerRadius + (_thickness * _selectProg), _selectingColor);
							
						} else {
							_selectProg = 0.0f;
							_isSelected = false;
							// focus ui update
							_uiArcItemBg.UpdateMesh (_innerRadius + (_thickness * focusProg), _outerRadius, _backgroundColor);
							_uiArcItemFs.UpdateMesh (_innerRadius, _innerRadius + (_thickness * focusProg), _focusingColor);
							_uiArcItemSt.UpdateMesh (0.0f, 0.0f, _selectingColor);
						}
						
					} else { // is non focusing
						// general ui update
						_uiArcItemBg.UpdateMesh (_innerRadius, _outerRadius, _backgroundColor);
						_uiArcItemFs.UpdateMesh (0.0f, 0.0f, _focusingColor);
						_uiArcItemSt.UpdateMesh (0.0f, 0.0f, _selectingColor);
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


	public override void Rendering() {
		/***** rendering *****/
		// make renderer gameobject
		rendererObj = new GameObject ("Renderer");
		rendererObj.transform.SetParent (_parentObj.transform, false);
		
		// center pivot position setting
		centerObj = new GameObject("Center");
		centerObj.transform.SetParent (rendererObj.transform, false);
		centerObj.transform.localPosition = new Vector3 (0, 0, _sSettings.InnerRadius + (_sSettings.Thickness / 2));
		
		// build arc item background ui
		backgroundObj = new GameObject ("Background");
		backgroundObj.transform.SetParent (rendererObj.transform, false);
		focusingObj = new GameObject ("Focusing");
		focusingObj.transform.SetParent (rendererObj.transform, false);
		selectingObj = new GameObject ("Selecting");
		selectingObj.transform.SetParent (rendererObj.transform, false);
	
		_uiArcItemBg = backgroundObj.AddComponent<UIArcItem> ();
		_uiArcItemBg.Build (_sSettings);
		_uiArcItemBg.UpdateMesh(_innerRadius, _outerRadius, _backgroundColor);
		
		_uiArcItemFs = focusingObj.AddComponent<UIArcItem> ();
		_uiArcItemFs.Build (_sSettings);
		
		_uiArcItemSt = selectingObj.AddComponent<UIArcItem> ();
		_uiArcItemSt.Build (_sSettings);


		// build item label ui
		labelObj = new GameObject ("Label");
		labelObj.transform.SetParent(rendererObj.transform, false);
		labelObj.transform.localPosition = new Vector3(0, 0, _sSettings.InnerRadius);
		labelObj.transform.localRotation = Quaternion.FromToRotation(Vector3.back, Vector3.right);
		labelObj.transform.localScale = new Vector3(1, 1, 1);
		
		UIItemLabel _uiItemLabel = labelObj.AddComponent<UIItemLabel>();
		_uiItemLabel.SetAttributes (_label, _iSettings.TextFont, _iSettings.TextSize, _iSettings.TextColor);
		
		// each type's ui
		switch (_itemType) {
		case (ItemType.Parent) :
			GameObject labelHasChildObj = new GameObject ("HasChildArrow");
			labelHasChildObj.transform.SetParent (rendererObj.transform, false);
			labelHasChildObj.transform.localPosition = new Vector3(0, 0, _sSettings.InnerRadius+_sSettings.Thickness);
			labelHasChildObj.transform.localRotation = Quaternion.FromToRotation(Vector3.back, Vector3.right);
			labelHasChildObj.transform.localScale = new Vector3(1, 1, 1);
			
			UIHasChild uiLabelHasChild = labelHasChildObj.AddComponent<UIHasChild>();
			uiLabelHasChild.SetAttributes (">", _iSettings.TextFont, _iSettings.TextSize, _iSettings.TextColor);

			break;
		case (ItemType.NormalButton) :
			
			break;
		default :
			break;
		}
		
	}
}
