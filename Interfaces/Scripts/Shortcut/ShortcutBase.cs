using UnityEngine;
using System.Collections;
using Interface.Shortcut;

public class ShortcutBase : MonoBehaviour {

	private ShortcutController scController;

	// 리소스 폴더의 아이템 이름
	public string shortcutItemResource;
	// size 등 inspector에 나타날 세부값 변수들
	public SelectionActionBase[] actions;

	// Use this for initialization
	public void Start () {
		for (int i = 0; i< actions.Length; i++) { // inspector null check
			if ( actions[i] == null )
			{
				Debug.Log ("item " + i + " is null");
				return;
			}
		}

		scController = new ShortcutController (actions.Length);

		for (int i = 0; i < actions.Length; i++) {
			scController.addItem (i, actions[i].name); // 컨트롤러에 아이템 추가
		}
		 
	}
	
	// Update is called once per frame
	public void Update () {
		scController.drawShortcut ();
	}

}
