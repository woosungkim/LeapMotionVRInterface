using UnityEngine;
using System.Collections;

public class ShortcutItemSettings : MonoBehaviour {

	public string TextFont = "Tahoma";
	public Color TextColor = Color.black;
	public int TextSize = 12;

	public Color BackgroundColor = new Color (0.1f, 0.1f, 0.1f, 0.5f);
	public Color FocusingColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	public Color SelectingColor = new Color (0.8f, 0.8f, 0.8f, 0.5f);

	public float InnerRadius = 0.2f;
	public float Thickness = 0.2f;
	
	public float EachItemDegree = 30.0f;

}
