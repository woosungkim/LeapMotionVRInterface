using UnityEngine;
using System.Collections;

public abstract class SelectableItem : MonoBehaviour {

	public abstract ShortcutItemLayer Layer { get; set; }
	public abstract bool IsCancelItem { get; set; }


	public abstract void Build (ShortcutSettings sSettings, GameObject parentObj);

	public abstract void SelectAction ();
}
