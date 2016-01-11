using UnityEngine;
using System.Collections;
using System;

public class ArcShortcut : MonoBehaviour {
	
	public float innerRadius = 0.1f;
	public float thickness = 0.3f;

	public int numberOfItems = 3;
	public float fullDegree = 180.0f;

	private float baseAngle;
	private float startAngle;
	private float endAngle;
	private int meshStep;

    private UIArcItem uiArcItem;
	private UILabel uiLabel;

	// Use this for initialization
	void Start () {
		float eachItemDegree = fullDegree / numberOfItems;
		float toDegree = 180 / (float)Mathf.PI;
		float eachItemAngle = eachItemDegree / toDegree;
		float fullAngle = fullDegree / toDegree;

		startAngle = -(eachItemAngle/2);
		endAngle = eachItemAngle/2;

		meshStep = (int)Math.Round(Math.Max(2, (endAngle-startAngle)/Math.PI*60));

		/*********************************************************/

		for (int i=0; i<numberOfItems; i++) {
            GameObject itemObj = new GameObject("Item "+i);
            itemObj.transform.SetParent (gameObject.transform, false);
			itemObj.transform.localRotation = Quaternion.Euler(0, eachItemDegree*i, 0);
            uiArcItem = itemObj.AddComponent<UIArcItem>();
            //uiArcItem.Build(innerRadius, thickness, startAngle, endAngle);

		}
		  
	}
	
	// Update is called once per frame
	void Update () {
		//vMainAlpha = UiItemSelectRenderer.GetArcAlpha(vMenuState);
		
		//Color colBg = vSettings.BackgroundColor;
		//colBg.a *= vMainAlpha;
		Color bgColor = new Color (0.1f, 0.1f, 0.1f, 0.5f);
		//bgMeshBuilder.CommitColors (bgColor);

		// setting에 있는 ui setting 값으로 setting
	}
	
}
