using UnityEngine;
using System.Collections;

public class SelectActionBase : MonoBehaviour {

	public bool isChecked = false;

	public string name;

	public void Start()
	{
	}

	public void OnSelectDown()
	{
		Debug.Log ("clicked item name : " + name);

	}

	void OnTriggerEnter(Collider obj)
	{

	}

	void OnTriggerExit(Collider obj)
	{

	}
}
