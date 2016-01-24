using UnityEngine;
using System.Collections;

public class WSClick : EventScript {
	
	public override void ClickAction ()
	{
		base.ClickAction ();
		
		GameObject cube = GameObject.Find ("Cube");
		
		cube.GetComponent<Renderer> ().material.color = Color.red;
	
	}
}
