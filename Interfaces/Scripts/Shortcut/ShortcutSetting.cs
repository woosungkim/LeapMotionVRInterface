using UnityEngine;
using System.Collections;

public class ShortcutSetting : MonoBehaviour {

	public enum ShortcutType
	{
		Arc,
		Stick
	}

	public string ShortcutName = "";

	[Range(0,1)]
	public float X = 0.5f, Y = 0.5f, Z = 0.5f;

	public ShortcutType Type = ShortcutType.Arc;

	public float InnerRadius = 0.1f;
	public float Thickness = 0.3f;

	public float EachItemDegree = 20.0f;

}
