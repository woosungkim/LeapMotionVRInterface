using UnityEngine;


public interface IShortcutItem {
	
	void Build (ShortcutSettings sSettings, GameObject parentObj);
	
	void Rendering();
}
