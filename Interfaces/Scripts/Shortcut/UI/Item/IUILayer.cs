using UnityEngine;
using System.Collections;

public interface IUILayer : IUIItem {

	bool IsCurrentLayer { get; }

	void Build(ShortcutSettings sSettings);

	//

	void AppearLayer(int direction);
	void DisappearLayer(int direction);
}
