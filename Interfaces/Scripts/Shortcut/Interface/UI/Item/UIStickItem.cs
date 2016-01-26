using UnityEngine;
using System.Collections;
using System;

public class UIStickItem : MonoBehaviour {
	
	ShortcutSettings _sSettings;

	// for rectangle mesh
	private float _width;
	private float _height;
	private float _wStart = 0.0f;
	private float _hStart = 0.0f;
	private float _amount = 1.0f;

	

	MeshFilter _filter;
	MeshBuilder _meshBuilder;
	
	internal void Build(ShortcutSettings sSettings)
	{
		// variables setting
		_sSettings = sSettings;

		gameObject.AddComponent<MeshRenderer> ();
		
		_filter = gameObject.AddComponent<MeshFilter>();
		
		_meshBuilder = new MeshBuilder ();
		
		gameObject.GetComponent<MeshRenderer> ().sharedMaterial = 
			ShortcutUtil.GetMaterial ();
		

		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	public void UpdateMesh(float width, float height, float wStart, float hStart, Color color)
	{		

		_width = width;
		_height = height;
		_wStart = wStart;
		_hStart = hStart;

		// update mesh
		BuildMesh (_meshBuilder);
		
		_meshBuilder.Commit (true, true);
		_meshBuilder.CommitColors (color);
		
		_filter.sharedMesh = _meshBuilder.Mesh;
		
	}
	
	void BuildMesh(MeshBuilder meshBuilder) {
		MeshNative.BuildRectangleMesh (meshBuilder, 
		                        		_width,
		                        		_height,
		                               	_wStart,
		                               	_hStart,
		                                _amount);
		
	}
}
