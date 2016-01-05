using UnityEngine;
using System.Collections;
using Interface.Shortcut;

public class ShortcutBase : MonoBehaviour {

	private ShortcutController scController;

	// size 등 inspector에 나타날 세부값 변수들
	public SelectionActionBase[] actions;

	// Use this for initialization
	public void Start () {
		scController = new ShortcutController ();

		for (int i = 0; i < actions.Length; i++) {
			if ( actions[i] == null )
			{
				Debug.Log ("item " + i + " is null");
				return;
			}

			scController.addItem (actions[i].name); // 컨트롤러에 아이템 추가


		}
		//setItems ();
	}
	
	// Update is called once per frame
	public void FixedUpdate () {

	}

	public void setItems() {
		scController = new ShortcutController ();

		/* add items */
	}

}
