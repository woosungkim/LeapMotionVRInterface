using UnityEngine;
using System.Collections;

public interface INormalButtonItem : ISelectableItem {

	ShortcutItemLayer Layer { get; set; }
	EventScript Action { get; set; }
	bool IsCancelItem { get; set; }

	
	void Build (ShortcutSettings sSettings, GameObject parentObj);
	
	void SelectAction ();
}
