using UnityEngine;
using System.Collections;

public class ShortcutSettings : MonoBehaviour {
	
	public string ShortcutName = "";

	[Range(0,1)]
	public float XPosition = 0.5f, YPosition = 0.5f;

	public ShortcutType Type = ShortcutType.Arc;
	
	public float ScaleCoeff = 1.0f;

}
