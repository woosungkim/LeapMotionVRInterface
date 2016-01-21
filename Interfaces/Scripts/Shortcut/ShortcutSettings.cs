using UnityEngine;
using System.Collections;

public class ShortcutSettings : MonoBehaviour {
	
	public string ShortcutName = "";

	[Range(0,1)]
	public float XPosition = 0.5f, YPosition = 0.5f;

	public ShortcutType Type = ShortcutType.Arc;

	// type arc
	public float InnerRadius = 0.3f;
	public float Thickness = 0.4f;
	public float EachItemDegree = 30.0f;

	// type stick
	public StickShortcutDirection Direction = StickShortcutDirection.Horizontal;
	public float ItemWidth = 0.3f;
	public float ItemHeight = 0.3f;

	//
	private ItemSettings _itemSettings;
	public ItemSettings ItemSettings { get { return _itemSettings; } set { _itemSettings = value; }	}

}
