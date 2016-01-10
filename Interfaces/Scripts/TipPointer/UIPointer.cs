using UnityEngine;
using System.Collections;
using System;

using Leap;

public class UIPointer : MonoBehaviour {

	public Color ColorNormal = new Color(1, 1, 1, 0.6f);
	public Color ColorHighlighted = new Color(1, 1, 1, 1);
	public float RadiusNormal = 0.12f;
	public float RadiusHighlighted = 0.06f;
	public float ThicknessNormal = 0.1f;
	public float ThicknessHighlighted = 0.4f;
	public float CursorForwardDistance = 0;

	MeshBuilder _meshBuilder;

	GameObject _pointerObj;

	// Use this for initialization
	internal void Build() {

		_pointerObj = new GameObject ("Pointer");
		_pointerObj.transform.SetParent (gameObject.transform, false);

		
		MeshRenderer meshRenderer = _pointerObj.AddComponent <MeshRenderer> ();
		meshRenderer.sharedMaterial = Materials.GetCursorLayer ();
		
		_pointerObj.AddComponent<MeshFilter> ();
		
		_meshBuilder = new MeshBuilder ();
		_pointerObj.GetComponent<MeshFilter> ().sharedMesh = _meshBuilder.Mesh;
	}
	
	// Update is called once per frame
	void Update () {
		float maxProg = 1.0f; 
		float thick = Mathf.Lerp (ThicknessNormal, ThicknessHighlighted, maxProg);
		float scale = Mathf.Lerp(RadiusNormal, RadiusHighlighted, maxProg);
		
		BuildMesh (thick);
		
		_pointerObj.transform.localScale = Vector3.one*scale;
		
		/**********************************************************************/



	}

	private void BuildMesh(float thickness) {
		
		MeshUtil.BuildRingMesh (_meshBuilder, (1 - thickness) / 2f, 0.5f, 0, (float)Math.PI * 2, 24);
		//MeshUtil.BuildCircleMesh(ringMeshBuilder, 0.5f, 24);
		_meshBuilder.Commit ();
	}


}
