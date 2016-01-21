using UnityEngine;
using System.Collections;

public interface IParentItem : ISelectableItem {

	ShortcutItemLayer Layer { get; set; }
	GameObject ItemObject { get; set; }


	void Build (ShortcutSettings sSettings, GameObject parentObj);


	void SelectAction ();
}
