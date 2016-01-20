using UnityEngine;
using System.Collections;

public class ItemSettings : MonoBehaviour {

	public string TextFont = "Tahoma";
	public Color TextColor = Color.black;
	public int TextSize = 20;

	public string CancelItemLabel = "Cancel";
	
	public Color BackgroundColor = new Color (0.1f, 0.1f, 0.1f, 0.5f);
	public Color FocusingColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	public Color SelectingColor = new Color (0.8f, 0.8f, 0.8f, 0.5f);
	
	public float SelectSpeed = 0.02f;
	public float FocusStart = 0.9f;

	public float InnerRadius = 0.3f;
	public float Thickness = 0.4f;
	public float EachItemDegree = 30.0f;

	public float ItemWidth = 0.3f;
	public float ItemHeight = 0.3f;
}
