using UnityEngine;
using System.Collections;
using System;

public class UIArcItem : MonoBehaviour {
	
	private float _innerRadius;
	private float _outerRadius;
	
	private float _startAngle;
	private float _endAngle;
	private int _meshStep;
	
	private int _id;
	
	private Color backgroundColor = new Color (0.1f, 0.1f, 0.1f, 0.5f);
	private Color focusingColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	private Color seiectingColor = new Color (0.8f, 0.8f, 0.8f, 0.5f);
	
	MeshFilter _filter;
	MeshBuilder _meshBuilder;
	
	internal void Build(ShortcutSetting setting, int id)
	{
		_id = id;
		
		float toDegree = 180 / (float)Mathf.PI;
		float eachItemAngle = setting.EachItemDegree / toDegree;

		_startAngle = -(eachItemAngle/2);
		_endAngle = eachItemAngle/2;
		_meshStep = (int)Math.Round(Math.Max(2, (_endAngle-_startAngle)/Math.PI*60));
		
		/*********************************************************/

		
		gameObject.AddComponent<MeshRenderer> ();
		
		_filter = gameObject.AddComponent<MeshFilter>();
		
		_meshBuilder = new MeshBuilder ();
		
		gameObject.GetComponent<MeshRenderer> ().sharedMaterial = 
			Materials.GetLayer (Materials.Layer.Background, 1);
		
		/*********************************************************/
		
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
	
	void BuildMesh(MeshBuilder meshBuilder) {
		MeshNative.BuildArcMesh (meshBuilder, 
		                         _innerRadius, 
		                         _outerRadius, 
		                         _startAngle, 
		                         _endAngle, 
		                         _meshStep);
		
	}
}
