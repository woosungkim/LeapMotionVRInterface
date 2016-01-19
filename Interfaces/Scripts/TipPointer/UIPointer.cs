using UnityEngine;
using System.Collections;
using System;

using Leap;

public class UIPointer : MonoBehaviour {
	
	private PointerType _type;

	private Color _colorNormal;
	private Color _colorHighlighted;
	private float _radiusNormal;
	private float _radiusHighlighted;
	private float _thicknessNormal;
	private float _thicknessHighlighted;


	MeshBuilder _meshBuilder;

	GameObject _pointerObj;

	// Use this for initialization
	internal void Build(PointerSettings pSettings, PointerType type) {
		_type = type;

		_colorNormal = pSettings.ColorNormal;
		_colorHighlighted = pSettings.ColorHighlighted;
		_radiusNormal = pSettings.RadiusNormal;
		_radiusHighlighted = pSettings.RadiusHighlighted;
		_thicknessNormal = pSettings.ThicknessNormal;
		_thicknessHighlighted = pSettings.ThicknessHighlighted;

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
	
		float maxProg = InteractionManager.GetPointerHighlightProgress(_type); 
		float thick = Mathf.Lerp (_thicknessNormal, _thicknessHighlighted, maxProg);
		float scale = Mathf.Lerp(_radiusNormal, _radiusHighlighted, maxProg);
		
		BuildMesh (thick);
		
		_pointerObj.transform.localScale = Vector3.one*scale;
		
		/**********************************************************************/

	}

	private void BuildMesh(float thickness) {
		
		MeshNative.BuildArcMesh (_meshBuilder, (1 - thickness) / 2.0f, 0.5f, 0, (float)Math.PI * 2, 30);

		_meshBuilder.Commit ();
	}


}
