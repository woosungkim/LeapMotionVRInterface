using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILabel : MonoBehaviour {

	private bool alignLeft;	

	private GameObject canvasGroupObj;
	private GameObject canvasObj;
	private GameObject textObj;

	private Text _text;
	private string _fontName;
	private Color _color;

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
		
		_text = textObj.AddComponent<Text> ();


		_text.GetComponent<Text> ().alignment = TextAnchor.MiddleLeft;

		/*********************************************************/
		
		_text.material = Materials.GetTextLayer (_text.material, 1);
		/*********************************************************/

	}

	public string Label {
		get { 
			return textObj.GetComponent<Text> ().text;
		}
		set {
			textObj.GetComponent<Text>().text = value;
		}
	}

	public string Font {
		get {
			return _fontName;
		}
		set {
			_fontName = value;
			_text.font = Resources.Load<Font> ("Fonts/"+_fontName);
		}

	}

	public Color Color {
		get {
			return _color;
		}
		set {
			_color = value;
			_text.color = _color;
		}

	}



}
