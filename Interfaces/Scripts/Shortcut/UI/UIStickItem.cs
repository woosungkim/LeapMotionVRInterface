using UnityEngine;
using System.Collections;
using System;

public class UIStickItem : MonoBehaviour {
	
	ItemSettings _iSettings;

	// for rectangle mesh
	private float _width;
	private float _height;
	private float _amount;

	

	MeshFilter _filter;
	MeshBuilder _meshBuilder;
	
	internal void Build(ItemSettings iSettings)
	{
		_iSettings = iSettings;
				
		_width = 3.0f;
		_height = 3.0f;
		_amount = 3.0f;
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
		                                _amount);
		
	}
}
