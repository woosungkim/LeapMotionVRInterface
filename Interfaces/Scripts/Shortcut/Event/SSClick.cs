using UnityEngine;
using System.Collections;

public class SSClick : EventScript {
	
	public override void ClickAction ()
	{
		base.ClickAction ();
		
		GameObject cube = GameObject.Find ("Cube");
		
		cube.GetComponent<Renderer> ().material.color = Color.green;
	}
}
