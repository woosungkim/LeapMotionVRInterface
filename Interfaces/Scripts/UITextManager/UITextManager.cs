using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITextManager : MonoBehaviour {
	[Range(0.0f, 1.0f)]
	public float xPos = 0.5f, yPos = 0.5f;

	public string textString = "화면에 표시 될 텍스트";
	public string fontName = "Tahoma";
	public int fontSize = 20;
	public Color textColor = Color.black;

	public float textWidth = 100.0f, textHeight = 100.0f;

	/*************************************************************/

	private Text text;

	private GameObject canvasGroupObj;
	private GameObject canvasObj;
	private GameObject textObj;


	private bool isFirst = true;

	public void DrawText() {
		if (isFirst) {
			isFirst = false;

			canvasGroupObj = new GameObject ("CanvasGroup");
			canvasGroupObj.transform.SetParent (gameObject.transform, false);
			canvasGroupObj.AddComponent<CanvasGroup> ();
			//canvasGroupObj.transform.localRotation = 
			//canvasGroupObj.transform.localScale = Vector3.one * 0.002f;

			/**/

			canvasObj = new GameObject ("Canvas");
			canvasObj.transform.SetParent (canvasGroupObj.transform, false);

			Canvas canvas = canvasObj.AddComponent<Canvas> ();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;

			/**/

			textObj = new GameObject ("Text");
			textObj.transform.SetParent (canvasObj.transform, false);


			text = textObj.AddComponent<Text> ();

			text.material = Materials.GetTextLayer (text.material, 1);

			SetTextAttribute ();
		} 
		else {
			canvasGroupObj.SetActive (true);
		}

	}

	public void SetTextAttribute() {
		textObj.GetComponent<Text> ().text = textString;

		text.font = Resources.Load<Font> ("Fonts/" + fontName);

		text.fontSize = fontSize;

		text.color = textColor;

		text.rectTransform.sizeDelta = new Vector2 (textWidth, textHeight);

		text.rectTransform.localPosition = 
			new Vector3 ((-Screen.width/2)+(Screen.width*xPos),
				(-Screen.height/2)+(Screen.height*yPos), 
				0.0f);

		//_text.rectTransform.sizeDelta = new Vector2 ((100.0f+width)*0.8f, 100.0f);
	}

	public void EraseText() {
		canvasGroupObj.SetActive (false);
	}


	public void UpdateTextPos() {
		text.rectTransform.localPosition = 
			new Vector3 ((-Screen.width/2)+(Screen.width*xPos),
				(-Screen.height/2)+(Screen.height*yPos), 
				0.0f);
	}
}
