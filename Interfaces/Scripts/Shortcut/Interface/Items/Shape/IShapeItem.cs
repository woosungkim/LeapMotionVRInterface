
using UnityEngine;

public interface IShapeItem : IShortcutItem {
	
	string Label { get; set; }
	ItemType Type { get; set; }
	EventScript Action { get; set; }
	
	GameObject ItemObject { get; set; }
	ShortcutItemLayer Layer { get; set; }
	bool IsCancelItem { get; set; }
	
	//

	void Build (ShortcutSettings sSettings, GameObject parentObj);

	void Rendering();
	
	
}