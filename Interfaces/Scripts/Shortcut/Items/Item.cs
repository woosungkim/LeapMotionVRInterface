using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour, IItem {

	public abstract void SetUserInputs(string label, ItemType itemType, EventScript action);

	public abstract void Build (ShortcutSettings sSettings, GameObject parentObj);

	public abstract void Update();

	public abstract void Rendering();
	
}
