using UnityEngine;
using System.Collections;
using System;

public class UIArcItem : MonoBehaviour {

	private float innerRadius;
	private float thickness;

	private float startAngle;
	private float endAngle;
	private int meshStep;

	internal void Build(ShortcutSetting setting)
	{
		float toDegree = 180 / (float)Mathf.PI;
		float eachItemAngle = setting.EachItemDegree / toDegree;


		this.innerRadius = setting.InnerRadius;
		this.thickness = setting.Thickness;
		this.startAngle = -(eachItemAngle/2);
		this.endAngle = eachItemAngle/2;
		meshStep = (int)Math.Round(Math.Max(2, (endAngle-startAngle)/Math.PI*60));

		/*********************************************************/

		//GameObject item = new GameObject ("Item");
		//item.transform.SetParent (gameObject.transform, false);
		/*********************************************************/

		gameObject.AddComponent<MeshRenderer> ();

		MeshFilter bgFilter = gameObject.AddComponent<MeshFilter>();
		
		MeshBuilder bgMeshBuilder = new MeshBuilder ();

		BuildMesh (bgMeshBuilder);

		bgMeshBuilder.Commit (true, true);
		bgMeshBuilder.CommitColors (new Color (0.1f, 0.1f, 0.1f, 0.5f));

		bgFilter.sharedMesh = bgMeshBuilder.Mesh;
		
		gameObject.GetComponent<MeshRenderer> ().sharedMaterial = 
			Materials.GetLayer (Materials.Layer.Background, 1);

		/*********************************************************/

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
