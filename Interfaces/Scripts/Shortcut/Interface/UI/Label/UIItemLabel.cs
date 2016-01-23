using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIItemLabel : MonoBehaviour, IUILabel {

	private bool alignLeft;	
	
	private GameObject canvasGroupObj;
	private GameObject canvasObj;
	private GameObject textObj;
	
	private Text _text;
	private string _fontName;
	private int _size;
	private Color _color;
	private TextAlignType _textAlignment;

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
		
		canvasObj.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
		/*********************************************************/
		
		textObj = new GameObject ("Text");
		textObj.transform.SetParent (canvasObj.transform, false);
		
		_text = textObj.AddComponent<Text> ();

		/*********************************************************/
		
		//_text.material = ShortcutUtil.GetTextMaterial (_text.material);
		_text.material = Materials.GetTextLayer (_text.material, 1);
		/*********************************************************/
		
	}
	
	public void SetAttributes(string text, string font, int size, Color color, float width) {
		textObj.GetComponent<Text>().text = text;
		
		_fontName = font;
		_text.font = Resources.Load<Font> ("Fonts/"+_fontName);
		
		_size = size;
		_text.fontSize = _size;
		
		_color = color;
		_text.color = _color;

		_text.rectTransform.sizeDelta = new Vector2 ((100.0f+width)*0.8f, 100.0f);
	}
	
	public void SetTextAlignment(TextAlignType alignType) {
		_textAlignment = alignType;

		if (alignType == TextAlignType.Left) {
			_text.alignment = TextAnchor.MiddleLeft;
		} else if (alignType == TextAlignType.Center) {
			_text.alignment = TextAnchor.MiddleCenter;
		} else if (alignType == TextAlignType.Right) {
			_text.alignment = TextAnchor.MiddleRight;
		}
	}

	public void RotateText(Vector3 rot) {
		_text.rectTransform.Rotate (rot);
	}

}
