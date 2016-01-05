using UnityEngine;
using System.Collections;

public class SelectionActionBase : MonoBehaviour {

	public string name;
	
	public void doAction()
	{
		Debug.Log ("clicked item name : " + name);

	}
}
