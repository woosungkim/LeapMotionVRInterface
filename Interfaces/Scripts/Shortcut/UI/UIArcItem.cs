using UnityEngine;
using System.Collections;
using System;

public class UIArcItem : MonoBehaviour {

	private float innerRadius;
	private float thickness;

	private float startAngle;
	private float endAngle;
	private int meshStep;
	
	private UILabel uiLabel;

	internal void Build(float innerRadius, float thickness, float startAngle, float endAngle )
	{
		this.innerRadius = innerRadius;
		this.thickness = thickness;
		this.startAngle = startAngle;
		this.endAngle = endAngle;
		meshStep = (int)Math.Round(Math.Max(2, (endAngle-startAngle)/Math.PI*60));

		/*********************************************************/

		//GameObject item = new GameObject ("Item");
		//item.transform.SetParent (gameObject.transform, false);
		/*********************************************************/

		GameObject background = new GameObject("Background");
		background.transform.SetParent (gameObject.transform, false);
		background.AddComponent<MeshRenderer> ();

		MeshFilter bgFilter = background.AddComponent<MeshFilter>();
		
		MeshBuilder bgMeshBuilder = new MeshBuilder ();

		BuildMesh (bgMeshBuilder);

		bgMeshBuilder.Commit (true, true);
		bgMeshBuilder.CommitColors (new Color (0.1f, 0.1f, 0.1f, 0.5f));

		bgFilter.sharedMesh = bgMeshBuilder.Mesh;
		
		background.GetComponent<MeshRenderer> ().sharedMaterial = 
			Materials.GetLayer (Materials.Layer.Background, 1);

		/*********************************************************/

		GameObject labelObj = new GameObject("Label");
		labelObj.transform.SetParent(gameObject.transform, false);
		labelObj.transform.localPosition = new Vector3(0, 0, innerRadius);
		labelObj.transform.localRotation = Quaternion.FromToRotation(Vector3.back, Vector3.right);
		labelObj.transform.localScale = new Vector3(1, 1, 1);

		uiLabel = labelObj.AddComponent<UILabel>();
		uiLabel.Label = "item "+innerRadius;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BuildMesh(MeshBuilder meshBuilder) {
		MeshNative.BuildArcMesh (meshBuilder, 
		                        innerRadius, 
		                        innerRadius + thickness, 
		                        startAngle, 
		                        endAngle, 
		                        meshStep);
	}
}
