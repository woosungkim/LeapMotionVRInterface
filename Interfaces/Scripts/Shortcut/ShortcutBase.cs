using UnityEngine;
using System.Collections;
using Interface.Shortcut;

public class ShortcutBase : MonoBehaviour {

	private ShortcutController scController;

	[Range(0, 1)]
	public float xPosition, yPosition, zPosition;

	// 리소스 폴더의 아이템 이름
	public string shortcutItemResourceName;
	// size 등 inspector에 나타날 세부값 변수들
	public SelectActionBase[] actions;
		
	// x, y, z 위치.. (0~1로 nomalization)


	// Use this for initialization
	public void Start () {
		for (int i = 0; i< actions.Length; i++) { // inspector null check
			if ( actions[i] == null )
			{
				Debug.Log ("item " + i + " is null");
				return;
			}
		}

		if (shortcutItemResourceName == null) { // inspector null check
			Debug.Log ("Shortcut Item Resource is null");
			return;
		}


		scController = new ShortcutController (shortcutItemResourceName, actions.Length);

		for (int i = 0; i < actions.Length; i++) {
			scController.addItem (i, actions[i]); // 컨트롤러에 아이템 추가
			
		}

		scController.bindItems ();
		 
	}
	
	// Update is called once per frame
	public void Update () {
		//scController.drawShortcut ();

	}

}
