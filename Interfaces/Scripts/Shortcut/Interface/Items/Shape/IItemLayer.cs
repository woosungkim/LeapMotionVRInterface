using UnityEngine;
using System.Collections;

public interface IItemLayer {
	
	int Level { get; set; }
	UILayer UILayer { get; set; }
	ShortcutItemLayer PrevLayer { get; set; }

	void Build(ShortcutSettings sSettings, GameObject parentObj);

}
