using UnityEngine;
using System.Collections;
using System;

using Leap;

public class UIPointer : MonoBehaviour {
	
	private PointerType _type;

	private Color _color;
	private float _radiusNormal;
	private float _radiusHighlighted;
	private float _thickness;


	MeshBuilder _meshBuilder;

	GameObject _pointerObj;

	// Use this for initialization
	internal void Build(PointerSettings pSettings, PointerType type) {
		_type = type;

		_color = pSettings.Color;
		_radiusNormal = pSettings.RadiusNormal;
		_radiusHighlighted = pSettings.RadiusHighlighted;
		_thickness = pSettings.Thickness;

		_pointerObj = new GameObject ("Pointer");
		_pointerObj.transform.SetParent (gameObject.transform, false);

		
		MeshRenderer meshRenderer = _pointerObj.AddComponent <MeshRenderer> ();
		meshRenderer.sharedMaterial = ShortcutUtil.GetMaterial ();

		
		_pointerObj.AddComponent<MeshFilter> ();
		
		_meshBuilder = new MeshBuilder ();
		_pointerObj.GetComponent<MeshFilter> ().sharedMesh = _meshBuilder.Mesh;
	}
	
	// Update is called once per frame
	void Update () {
	
		float maxProg = InteractionManager.GetPointerHighlightProgress(_type); 
		float thick = Mathf.Lerp (_thickness, _thickness, maxProg);
		float scale = Mathf.Lerp(_radiusNormal, _radiusHighlighted, maxProg);
		
		BuildMesh (thick);
		
		_pointerObj.transform.localScale = Vector3.one*scale;
		
		/**********************************************************************/

	}

	private void BuildMesh(float thickness) {
		
		MeshNative.BuildArcMesh (_meshBuilder, (1 - thickness) / 2.0f, 0.5f, 0, (float)Math.PI * 2, 30);

		_meshBuilder.Commit ();
		_meshBuilder.CommitColors (_color);
	}


}
