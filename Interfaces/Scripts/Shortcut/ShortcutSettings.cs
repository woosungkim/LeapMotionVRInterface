using UnityEngine;
using System.Collections;

public class ShortcutSettings : MonoBehaviour {

	public bool AutoStart = false;

	[Range(0.3f, 1.0f)]
	public float DistanceFromMainCamera = 0.5f;

	[Range(0.0f,1.0f)]
	public float XPosition = 0.0f, YPosition = 0.0f;

	public ShortcutType Type = ShortcutType.Arc;

	// type arc
	public float InnerRadius = 0.3f;
	public float Thickness = 0.4f;
	public float EachItemDegree = 30.0f;

	// type stick
	public StickShortcutDirection Direction = StickShortcutDirection.Horizontal;
	public float ItemWidth = 0.3f;
	public float ItemHeight = 0.3f;

	// color
	public Color BackgroundColor = new Color (0.1f, 0.1f, 0.1f, 0.5f);
	public Color FocusingColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	public Color SelectingColor = new Color (0.8f, 0.8f, 0.8f, 0.5f);

	// item Settings
	public string TextFont = "Tahoma";
	public Color TextColor = Color.black;
	public int TextSize = 20;
	
	public string CancelItemLabel = "Cancel";



	// interaction Settings
	public float AppearAnimSpeed = 4.0f;
	public float SelectSpeed = 2.0f;
	public float FocusStart = 0.1f;


}
