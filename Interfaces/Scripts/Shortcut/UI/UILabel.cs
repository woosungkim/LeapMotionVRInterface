using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILabel : MonoBehaviour {

	private bool alignLeft;	

	private GameObject canvasGroupObj;
	private GameObject canvasObj;
	private GameObject textObj;

	public void Awake() {
		canvasGroupObj = new GameObject ("CanvasGroup");
		canvasGroupObj.transform.SetParent(gameObject.transform, false);
		canvasGroupObj.AddComponent<CanvasGroup>();
		canvasGroupObj.transform.localRotation = 
			Quaternion.FromToRotation(Vector3.forward, Vector3.down);

		canvasGroupObj.transform.localScale = Vector3.one * 0.002f;

		/*******************************************************/
		
		canvasObj = new GameObject ("Canvas");
		canvasObj.transform.SetParent (canvasGroupObj.transform, false);

		Canvas canvas = canvasObj.AddComponent<Canvas> ();
		canvas.renderMode = RenderMode.WorldSpace;

		canvasObj.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
		/*********************************************************/

		textObj = new GameObject ("Text");
		textObj.transform.SetParent (canvasObj.transform, false);
		
		Text text = textObj.AddComponent<Text> ();


		text.GetComponent<Text> ().alignment = TextAnchor.MiddleLeft;

		/*********************************************************/
		
		text.material = Materials.GetTextLayer (text.material, 1);
		/*********************************************************/
		
		string fontName = "Tahoma";
		text.font = Resources.Load<Font> ("Fonts/"+fontName);
		text.color = Color.black;
		text.text = "dd";


	}

	public string Label {
		get { 
			return textObj.GetComponent<Text> ().text;
		}
		set {
			textObj.GetComponent<Text>().text = value;
		}
	}



}
