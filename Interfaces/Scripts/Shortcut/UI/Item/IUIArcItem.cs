using UnityEngine;
using System.Collections;

public interface IUIArcItem : IUIItem {
	
	void Build(ShortcutSettings sSettings);

	//

	void UpdateMesh(float innerRadius, float outerRadius, Color color);

}
