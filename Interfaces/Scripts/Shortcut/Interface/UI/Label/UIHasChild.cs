
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHasChild : MonoBehaviour, IUILabel {

	private GameObject canvasGroupObj;
	private GameObject canvasObj;
	private GameObject textObj;
	
	private Text _text;
	private string _fontName;
	private int _size;
	private Color _color;

	public string Label { 
		get { return textObj.GetComponent<Text> ().text; }
		set { textObj.GetComponent<Text>().text = value; }
	}
	
	public string Font { 
		get { return _fontName;	}
		set { _fontName = value;
			_text.font = Resources.Load<Font> ("Fonts/"+_fontName); }
	}
	
	public int Size {
		get { return _size;	}	
		set { _size = value;
			_text.fontSize = _size; }
	}
	
	public Color Color { 
		get { return _color; }
		set { _color = value;
			_text.color = _color; }
	}
	
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
		
		canvasObj.GetComponent<RectTransform>().pivot = new Vector2(0.6f, 0.5f);
		/*********************************************************/
		
		textObj = new GameObject ("Text");
		textObj.transform.SetParent (canvasObj.transform, false);
		
		_text = textObj.AddComponent<Text> ();
		_text.text = ">";
		
		_text.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
		
		/*********************************************************/
		
		//_text.material = ShortcutUtil.GetMaterial ();
		_text.material = Materials.GetTextLayer (_text.material, 1);
		/*********************************************************/
		
	}
	
	public void SetAttributes(string text, string font, int size, Color color) {
		textObj.GetComponent<Text>().text = text;
		
		_fontName = font;
		_text.font = Resources.Load<Font> ("Fonts/"+_fontName);
		
		_size = size;
		_text.fontSize = _size;
		
		_color = color;
		_text.color = _color;
	}
}
