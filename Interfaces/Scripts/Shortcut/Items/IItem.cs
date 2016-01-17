
using UnityEngine;

public interface IItem {

	void SetUserInputs(string label, ItemType itemType, EventScript action);

	void Build (ShortcutSettings sSettings, GameObject parentObj);
	
	void Update();
	
	void Rendering();
}