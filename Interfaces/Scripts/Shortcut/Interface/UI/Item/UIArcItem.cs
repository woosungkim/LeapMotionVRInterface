using UnityEngine;
using System.Collections;
using System;

public class UIArcItem : MonoBehaviour, IUIArcItem {

    ShortcutSettings _sSettings;
	ItemSettings _iSettings;
	
	private float _innerRadius;
	private float _outerRadius;
	
	private float _startAngle;
	private float _endAngle;
	private int _meshStep;

	
	MeshFilter _filter;
	MeshBuilder _meshBuilder;
	
	public void Build(ShortcutSettings sSettings)
	{
		// variables setting
        _sSettings = sSettings;
		_iSettings = sSettings.ItemSettings;

		float toDegree = 180 / (float)Mathf.PI;
		float eachItemAngle = _sSettings.EachItemDegree / toDegree;

		_startAngle = -(eachItemAngle/2);
		_endAngle = eachItemAngle/2;
		_meshStep = (int)Math.Round(Math.Max(2, (_endAngle-_startAngle)/Math.PI*60));


		// rendering
		gameObject.AddComponent<MeshRenderer> ();
		
		_filter = gameObject.AddComponent<MeshFilter>();
		
		_meshBuilder = new MeshBuilder ();
		
		gameObject.GetComponent<MeshRenderer> ().sharedMaterial = 
			ShortcutUtil.GetMaterial ();
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	public void UpdateMesh(float innerRadius, float outerRadius, Color color)
	{
		_innerRadius = innerRadius;
		_outerRadius = outerRadius;

		// update mesh
		BuildMesh (_meshBuilder);

		_meshBuilder.Commit (true, true);
		_meshBuilder.CommitColors (color);
		
		_filter.sharedMesh = _meshBuilder.Mesh;

	}
	
	private void BuildMesh(MeshBuilder meshBuilder) {
		MeshNative.BuildArcMesh (meshBuilder, 
		                         _innerRadius, 
		                         _outerRadius, 
		                         _startAngle, 
		                         _endAngle, 
		                         _meshStep);
		
	}
}
